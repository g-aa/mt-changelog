using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Mt.ChangeLog.DataContext;
using Mt.ChangeLog.Entities.Tables;
using Mt.ChangeLog.Logic.Mappers;
using Mt.ChangeLog.TransferObjects.Other;
using Mt.ChangeLog.TransferObjects.Platform;
using Mt.Entities.Abstractions.Extensions;
using Mt.Utilities.Exceptions;

namespace Mt.ChangeLog.Logic.Features.Platform;

/// <summary>
/// Запрос на обновление сущности <see cref="PlatformModel"/>.
/// </summary>
public static class Update
{
    /// <inheritdoc />
    public sealed record Command(Guid PlatformId, PlatformModel Model) : IRequest<MessageModel>
    {
    }

    /// <inheritdoc />
    public sealed class Validator : AbstractValidator<Command>
    {
        /// <summary>
        /// Инициализация экземпляра <see cref="Validator"/>.
        /// </summary>
        /// <param name="validator">Platform model validator.</param>
        public Validator(IValidator<PlatformModel> validator)
        {
            this.RuleFor(e => e.PlatformId)
                .Must((command, id) => id == command.Model.Id)
                .WithMessage("Значение параметра '{PropertyName}' не равен значению идентификатора в модели из тела запроса.");

            this.RuleFor(e => e.Model).SetValidator(validator);
        }
    }

    /// <inheritdoc />
    public sealed class Handler : IRequestHandler<Command, MessageModel>
    {
        private readonly ILogger<Handler> logger;

        private readonly MtContext context;

        /// <summary>
        /// Инициализация нового экземпляра класса <see cref="Handler"/>.
        /// </summary>
        /// <param name="logger">Журнал логирования.</param>
        /// <param name="context">Контекст данных.</param>
        public Handler(ILogger<Handler> logger, MtContext context)
        {
            this.logger = logger;
            this.context = context;
        }

        /// <inheritdoc />
        public Task<MessageModel> Handle(Command request, CancellationToken cancellationToken)
        {
            var model = request.Model;
            this.logger.LogDebug("Получен запрос на обновление данных платформы '{Title}' в системе.", model.Title);

            var dbPlatform = this.context.Platforms
                .Include(e => e.Projects)
                .Include(e => e.AnalogModules)
                .Search(model.Id);

            if (dbPlatform.Default)
            {
                throw new MtException(ErrorCode.EntityCannotBeModified, $"Сущность по умолчанию '{dbPlatform}' не может быть обновлена.");
            }

            var dbAnalogModules = this.context.AnalogModules
                .SearchManyOrDefault(model.AnalogModules.Select(e => e.Id));
            dbPlatform.GetBuilder()
                .SetAttributes(model)
                .SetAnalogModules(dbAnalogModules)
                .Build();

            return this.SaveChangesAsync(dbPlatform, cancellationToken);
        }

        /// <summary>
        /// Сохранить изменения сущности.
        /// </summary>
        /// <param name="entity">Сущность.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Результат выполнения.</returns>
        private async Task<MessageModel> SaveChangesAsync(PlatformEntity entity, CancellationToken cancellationToken)
        {
            this.context.Platforms.Update(entity);
            await this.context.SaveChangesAsync(cancellationToken);

            this.logger.LogInformation("Платформа '{Title}' успешно обновлен в системе.", entity.Title);
            return new MessageModel
            {
                Message = $"'{entity}' обновлена в системе.",
            };
        }
    }
}