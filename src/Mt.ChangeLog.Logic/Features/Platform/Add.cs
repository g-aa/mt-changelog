using FluentValidation;
using MediatR;
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
/// Запрос на добавления сущности <see cref="PlatformModel"/>.
/// </summary>
public static class Add
{
    /// <inheritdoc />
    public sealed record Command(PlatformModel Model) : IRequest<MessageModel>
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
            this.logger.LogDebug("Получен запрос на добавление платформы '{Title}' в систему.", model.Title);

            var dbAnalogModules = this.context.AnalogModules
                .SearchManyOrDefault(model.AnalogModules.Select(e => e.Id));

            var dbPlatform = new PlatformEntity().GetBuilder()
                .SetAttributes(model)
                .SetAnalogModules(dbAnalogModules)
                .Build();

            if (this.context.Platforms.IsContained(dbPlatform))
            {
                throw new MtException(ErrorCode.EntityAlreadyExists, $"Сущность '{dbPlatform}' уже содержится в системе.");
            }

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
            await this.context.Platforms.AddAsync(entity, cancellationToken);
            await this.context.SaveChangesAsync(cancellationToken);

            this.logger.LogInformation("Платформа '{Title}' успешно добавлен в систему.", entity.Title);
            return new MessageModel
            {
                Message = $"'{entity}' была добавлена в систему.",
            };
        }
    }
}