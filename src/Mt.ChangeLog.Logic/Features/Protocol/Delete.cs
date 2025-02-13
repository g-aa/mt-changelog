using FluentValidation;

using MediatR;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using Mt.ChangeLog.DataContext;
using Mt.ChangeLog.Entities.Tables;
using Mt.ChangeLog.TransferObjects.Other;
using Mt.ChangeLog.TransferObjects.Protocol;
using Mt.Entities.Abstractions.Extensions;
using Mt.Utilities.Exceptions;

namespace Mt.ChangeLog.Logic.Features.Protocol;

/// <summary>
/// Запрос на удаления модели аналогового модуля из системы <see cref="ProtocolModel"/>.
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
            _logger.LogDebug("Получен запрос на удаление протокола '{Model}' из системы.", model);

            var dbRemovable = _context.Protocols
                .Include(e => e.Communications).ThenInclude(e => e.Protocols)
                .AsSingleQuery()
                .Search(model.Id);

            if (dbRemovable.Default)
            {
                throw new MtException(ErrorCode.EntityCannotBeDeleted, $"Сущность по умолчанию '{dbRemovable}' не может быть удалена из системы.");
            }

            if (dbRemovable.Communications.Count != 0)
            {
                var defProtocol = _context.Protocols.First(e => e.Default);
                foreach (var dbModule in dbRemovable.Communications.Where(c => c.Protocols.Remove(dbRemovable) && c.Protocols.Count == 0))
                {
                    dbModule.Protocols.Add(defProtocol);
                }
            }

            _context.Protocols.Remove(dbRemovable);
            return SaveChangesAsync(dbRemovable, cancellationToken);
        }

        private async Task<MessageModel> SaveChangesAsync(ProtocolEntity entity, CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Протокол '{Entity}' успешно удален из системы.", entity);
            return new MessageModel
            {
                Message = $"'{entity}' был удален из системы.",
            };
        }
    }
}