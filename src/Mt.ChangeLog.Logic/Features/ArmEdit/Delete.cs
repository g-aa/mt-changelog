using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Mt.ChangeLog.DataContext;
using Mt.ChangeLog.Entities.Tables;
using Mt.ChangeLog.TransferObjects.ArmEdit;
using Mt.ChangeLog.TransferObjects.Other;
using Mt.Entities.Abstractions.Extensions;
using Mt.Utilities.Exceptions;

namespace Mt.ChangeLog.Logic.Features.ArmEdit;

/// <summary>
/// Запрос на удаления модели аналогового модуля из системы <see cref="ArmEditModel"/>.
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
            _logger.LogDebug("Получен запрос на удаление ArmEdit '{Model}' из системы.", model);

            var dbRemovable = _context.ArmEdits
                .Include(e => e.ProjectRevisions)
                .Search(model.Id);

            if (dbRemovable.Default)
            {
                throw new MtException(ErrorCode.EntityCannotBeDeleted, $"Сущность по умолчанию '{dbRemovable}' нельзя удалить из системы.");
            }

            if (dbRemovable.ProjectRevisions.Count != 0)
            {
                throw new MtException(ErrorCode.EntityCannotBeDeleted, $"Сущность '{dbRemovable}' используется в редакциях БФПО и не может быть удалена из системы.");
            }

            return SaveChangesAsync(dbRemovable, cancellationToken);
        }

        /// <summary>
        /// Сохранить изменения сущности.
        /// </summary>
        /// <param name="entity">Сущность.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Результат выполнения.</returns>
        private async Task<MessageModel> SaveChangesAsync(ArmEditEntity entity, CancellationToken cancellationToken)
        {
            _context.ArmEdits.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("ArmEdit '{Entity}' успешно удален из системы.", entity);
            return new MessageModel
            {
                Message = $"'{entity}' был удален из системы.",
            };
        }
    }
}