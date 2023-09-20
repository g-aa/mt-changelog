using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Mt.ChangeLog.DataContext;
using Mt.ChangeLog.Entities.Tables;
using Mt.ChangeLog.Logic.Mappers;
using Mt.ChangeLog.TransferObjects.Other;
using Mt.ChangeLog.TransferObjects.ProjectStatus;
using Mt.Entities.Abstractions.Extensions;
using Mt.Utilities.Exceptions;

namespace Mt.ChangeLog.Logic.Features.ProjectStatus;

/// <summary>
/// Запрос на обновление сущности <see cref="ProjectStatusModel"/>.
/// </summary>
public static class Update
{
    /// <inheritdoc />
    public sealed record Command(Guid ProjectStatusId, ProjectStatusModel Model) : IRequest<MessageModel>
    {
    }

    /// <inheritdoc />
    public sealed class Validator : AbstractValidator<Command>
    {
        /// <summary>
        /// Инициализация экземпляра <see cref="Validator"/>.
        /// </summary>
        /// <param name="validator">Project status model validator.</param>
        public Validator(IValidator<ProjectStatusModel> validator)
        {
            this.RuleFor(e => e.ProjectStatusId)
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
            this.logger.LogDebug("Получен запрос на обновление данных статуса проекта '{Title}' в системе.", model.Title);

            var dbProjectStatus = this.context.ProjectStatuses.Search(model.Id);
            if (dbProjectStatus.Default)
            {
                throw new MtException(ErrorCode.EntityCannotBeModified, $"Сущность по умолчанию '{dbProjectStatus}' не может быть обновлена.");
            }

            dbProjectStatus.GetBuilder().SetAttributes(model).Build();
            return this.SaveChangesAsync(dbProjectStatus, cancellationToken);
        }

        /// <summary>
        /// Сохранить изменения сущности.
        /// </summary>
        /// <param name="entity">Сущность.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Результат выполнения.</returns>
        private async Task<MessageModel> SaveChangesAsync(ProjectStatusEntity entity, CancellationToken cancellationToken)
        {
            this.context.ProjectStatuses.Update(entity);
            await this.context.SaveChangesAsync(cancellationToken);

            this.logger.LogInformation("Статус проекта '{Title}' успешно обновлен в системе.", entity.Title);
            return new MessageModel
            {
                Message = $"'{entity}' обновлен в системе.",
            };
        }
    }
}