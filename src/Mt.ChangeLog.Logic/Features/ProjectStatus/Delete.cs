using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Mt.ChangeLog.DataContext;
using Mt.ChangeLog.Entities.Tables;
using Mt.ChangeLog.TransferObjects.Other;
using Mt.ChangeLog.TransferObjects.ProjectStatus;
using Mt.Entities.Abstractions.Extensions;
using Mt.Utilities.Exceptions;

namespace Mt.ChangeLog.Logic.Features.ProjectStatus;

/// <summary>
/// Запрос на удаления модели аналогового модуля из системы <see cref="ProjectStatusModel"/>.
/// </summary>
public static class Delete
{
    /// <inheritdoc />
    public sealed record Command(BaseModel Model) : IRequest<MessageModel>
    {
    }

    /// <inheritdoc />
    public sealed class CommandValidator : AbstractValidator<Command>
    {
        /// <summary>
        /// Инициализация экземпляра <see cref="CommandValidator"/>.
        /// </summary>
        /// <param name="validator">Base model validator.</param>
        public CommandValidator(IValidator<BaseModel> validator)
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
            this.logger.LogDebug("Получен запрос на удаление статуса проектов '{Id}' из системы.", model.Id);

            var dbRemovable = this.context.ProjectStatuses
                .Include(e => e.ProjectVersions)
                .Search(model.Id);

            if (dbRemovable.Default)
            {
                throw new MtException(ErrorCode.EntityCannotBeDeleted, $"Сущность по умолчанию '{dbRemovable}' не может быть удалена из системы.");
            }

            if (dbRemovable.ProjectVersions.Any())
            {
                throw new MtException(ErrorCode.EntityCannotBeDeleted, $"Сущность '{dbRemovable}' используется в проектах и не может быть удалена из системы.");
            }

            return this.SaveChangesAsync(dbRemovable, cancellationToken);
        }

        /// <summary>
        /// Сохранить изменения сущности.
        /// </summary>
        /// <param name="entity">Сущность.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Результат выполнения.</returns>
        private async Task<MessageModel> SaveChangesAsync(ProjectStatusEntity entity, CancellationToken cancellationToken)
        {
            this.context.ProjectStatuses.Remove(entity);
            await this.context.SaveChangesAsync(cancellationToken);

            this.logger.LogInformation("Статус проекта '{Title}' успешно удален из системы.", entity.Title);
            return new MessageModel
            {
                Message = $"'{entity}' был удален из системы.",
            };
        }
    }
}