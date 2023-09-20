using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Mt.ChangeLog.DataContext;
using Mt.ChangeLog.Entities.Tables;
using Mt.ChangeLog.Logic.Mappers;
using Mt.ChangeLog.TransferObjects.AnalogModule;
using Mt.ChangeLog.TransferObjects.Other;
using Mt.Entities.Abstractions.Extensions;
using Mt.Utilities.Exceptions;

namespace Mt.ChangeLog.Logic.Features.AnalogModule;

/// <summary>
/// Запрос на добавления сущности <see cref="AnalogModuleModel"/>.
/// </summary>
public static class Add
{
    /// <inheritdoc />
    public sealed record Command(AnalogModuleModel Model) : IRequest<MessageModel>
    {
    }

    /// <inheritdoc />
    public sealed class Validator : AbstractValidator<Command>
    {
        /// <summary>
        /// Инициализация экземпляра <see cref="Validator"/>.
        /// </summary>
        /// <param name="validator">Analog module model validator.</param>
        public Validator(IValidator<AnalogModuleModel> validator)
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
            this.logger.LogDebug("Получен запрос на добавление аналогового модуля '{DIVG}' '{Title}' в систему.", model.DIVG, model.Title);

            var dbPlatforms = this.context.Platforms.SearchManyOrDefault(model.Platforms.Select(e => e.Id));

            var dbAnalogModule = new AnalogModuleEntity().GetBuilder()
                .SetAttributes(model)
                .SetPlatforms(dbPlatforms)
                .Build();

            if (this.context.AnalogModules.IsContained(dbAnalogModule))
            {
                throw new MtException(ErrorCode.EntityAlreadyExists, $"Сущность '{dbAnalogModule}' уже содержится в системе.");
            }

            return this.SaveChangesAsync(dbAnalogModule, cancellationToken);
        }

        /// <summary>
        /// Сохранить изменения сущности.
        /// </summary>
        /// <param name="entity">Сущность.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Результат выполнения.</returns>
        private async Task<MessageModel> SaveChangesAsync(AnalogModuleEntity entity, CancellationToken cancellationToken)
        {
            await this.context.AnalogModules.AddAsync(entity, cancellationToken);
            await this.context.SaveChangesAsync(cancellationToken);

            this.logger.LogInformation("Аналоговый модуль '{DIVG}' '{Title}' успешно добавлен в систему.", entity.DIVG, entity.Title);
            return new MessageModel
            {
                Message = $"'{entity}' был добавлен в систему.",
            };
        }
    }
}