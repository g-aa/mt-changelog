using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Mt.ChangeLog.DataContext;
using Mt.ChangeLog.Entities.Tables;
using Mt.ChangeLog.TransferObjects.AnalogModule;
using Mt.ChangeLog.TransferObjects.Other;
using Mt.Entities.Abstractions.Extensions;
using Mt.Utilities.Exceptions;

namespace Mt.ChangeLog.Logic.Features.AnalogModule;

/// <summary>
/// Запрос на удаления модели аналогового модуля из системы <see cref="AnalogModuleModel"/>.
/// </summary>
public static class Delete
{
    /// <inheritdoc />
    public sealed record Command(BaseModel Model) : IRequest<MessageModel>
    {
    }

    /// <inheritdoc />
    public sealed class Validator : AbstractValidator<Command>
    {
        /// <summary>
        /// Инициализация экземпляра <see cref="Validator"/>.
        /// </summary>
        /// <param name="validator">Base model validator.</param>
        public Validator(IValidator<BaseModel> validator)
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
            this.logger.LogDebug("Получен запрос на удаление аналогового модуля '{Id}' из системы.", model.Id);

            var dbRemovable = this.context.AnalogModules
                .Include(e => e.Projects)
                .Include(e => e.Platforms).ThenInclude(e => e.AnalogModules)
                .AsSingleQuery()
                .Search(request.Model.Id);

            if (dbRemovable.Default)
            {
                throw new MtException(ErrorCode.EntityCannotBeDeleted, $"Сущность по умолчанию '{dbRemovable}' не может быть удалена из системы.");
            }

            if (dbRemovable.Projects.Any())
            {
                throw new MtException(ErrorCode.EntityCannotBeDeleted, $"Сущность '{dbRemovable}' используемая в проектах не может быть удалена из системы.");
            }

            if (dbRemovable.Platforms.Any())
            {
                var defModule = this.context.AnalogModules.First(e => e.Default);
                foreach (var dbPlatform in dbRemovable.Platforms.Where(p => p.AnalogModules.Remove(dbRemovable) && !p.AnalogModules.Any()))
                {
                    dbPlatform.AnalogModules.Add(defModule);
                }
            }

            return this.SaveChangesAsync(dbRemovable, cancellationToken);
        }

        /// <summary>
        /// Сохранить изменения сущности.
        /// </summary>
        /// <param name="entity">Сущность.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Результат выполнения.</returns>
        private async Task<MessageModel> SaveChangesAsync(AnalogModuleEntity entity, CancellationToken cancellationToken)
        {
            this.context.AnalogModules.Remove(entity);
            await this.context.SaveChangesAsync(cancellationToken);

            this.logger.LogInformation("Аналоговый модуль '{DIVG}' '{Title}' успешно удален из системы.", entity.DIVG, entity.Title);
            return new MessageModel
            {
                Message = $"'{entity}' был удален из системы.",
            };
        }
    }
}