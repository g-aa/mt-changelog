using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Mt.ChangeLog.DataContext;
using Mt.ChangeLog.Entities.Tables;
using Mt.ChangeLog.Logic.Extensions;
using Mt.ChangeLog.TransferObjects.Other;
using Mt.ChangeLog.TransferObjects.Protocol;
using Mt.Entities.Abstractions.Extensions;
using Mt.Utilities.Exceptions;

namespace Mt.ChangeLog.Logic.Features.Protocol;

/// <summary>
/// Запрос на обновление сущности <see cref="ProtocolModel"/>.
/// </summary>
public static class Update
{
    /// <inheritdoc />
    public sealed record Command(Guid ProtocolId, ProtocolModel Model) : IRequest<MessageModel>
    {
    }

    /// <inheritdoc />
    public sealed class Validator : AbstractValidator<Command>
    {
        /// <summary>
        /// Инициализация экземпляра <see cref="Validator"/>.
        /// </summary>
        /// <param name="validator">Protocol model validator.</param>
        public Validator(IValidator<ProtocolModel> validator)
        {
            RuleFor(e => e.ProtocolId)
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
            _logger.LogDebug("Получен запрос на обновление данных протокола '{Model}' в системе.", model);

            var dbProtocol = _context.Protocols.Search(model.Id);
            if (dbProtocol.Default)
            {
                throw new MtException(ErrorCode.EntityCannotBeModified, $"Сущность по умолчанию '{dbProtocol}' не может быть обновлена.");
            }

            var dbModules = _context.Communications
                .SearchManyOrDefault(model.Communications.Select(e => e.Id));
            dbProtocol.GetBuilder()
                .SetAttributes(model)
                .SetModules(dbModules)
                .Build();

            return SaveChangesAsync(dbProtocol, cancellationToken);
        }

        /// <summary>
        /// Сохранить изменения сущности.
        /// </summary>
        /// <param name="entity">Сущность.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Результат выполнения.</returns>
        private async Task<MessageModel> SaveChangesAsync(ProtocolEntity entity, CancellationToken cancellationToken)
        {
            _context.Protocols.Update(entity);
            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Протокол '{Entity}' успешно обновлен в системе.", entity);
            return new MessageModel
            {
                Message = $"'{entity}' обновлен в системе.",
            };
        }
    }
}