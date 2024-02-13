using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Mt.ChangeLog.DataContext;
using Mt.ChangeLog.Entities.Tables;
using Mt.ChangeLog.Logic.Extensions;
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
            RuleFor(e => e.ProjectStatusId)
                .Must((command, id) => id == command.Model.Id)
                .WithMessage("Значение параметра '{PropertyName}' не равен значению идентификатора в модели из тела запроса.");

            RuleFor(e => e.Model).SetValidator(validator);
        }
    }

    /// <inheritdoc />
    public sealed class Handler : IRequestHandler<Command, MessageModel>
    {
        private readonly ILogger<Handler> _logger;

        private readonly MtContext _context;

        /// <summary>
        /// Инициализация нового экземпляра класса <see cref="Handler"/>.
        /// </summary>
        /// <param name="logger">Журнал логирования.</param>
        /// <param name="context">Контекст данных.</param>
        public Handler(ILogger<Handler> logger, MtContext context)
        {
            _logger = logger;
            _context = context;
        }

        /// <inheritdoc />
        public Task<MessageModel> Handle(Command request, CancellationToken cancellationToken)
        {
            var model = request.Model;
            _logger.LogDebug("Получен запрос на обновление данных статуса проекта '{Model}' в системе.", model);

            var dbProjectStatus = _context.ProjectStatuses.Search(model.Id);
            if (dbProjectStatus.Default)
            {
                throw new MtException(ErrorCode.EntityCannotBeModified, $"Сущность по умолчанию '{dbProjectStatus}' не может быть обновлена.");
            }

            dbProjectStatus.GetBuilder().SetAttributes(model).Build();
            return SaveChangesAsync(dbProjectStatus, cancellationToken);
        }

        /// <summary>
        /// Сохранить изменения сущности.
        /// </summary>
        /// <param name="entity">Сущность.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Результат выполнения.</returns>
        private async Task<MessageModel> SaveChangesAsync(ProjectStatusEntity entity, CancellationToken cancellationToken)
        {
            _context.ProjectStatuses.Update(entity);
            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Статус проекта '{Entity}' успешно обновлен в системе.", entity);
            return new MessageModel
            {
                Message = $"'{entity}' обновлен в системе.",
            };
        }
    }
}