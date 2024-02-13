using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Mt.ChangeLog.DataContext;
using Mt.ChangeLog.Entities.Tables;
using Mt.ChangeLog.Logic.Extensions;
using Mt.ChangeLog.TransferObjects.Communication;
using Mt.ChangeLog.TransferObjects.Other;
using Mt.Entities.Abstractions.Extensions;
using Mt.Utilities.Exceptions;

namespace Mt.ChangeLog.Logic.Features.Communication;

/// <summary>
/// Запрос на обновление сущности <see cref="CommunicationModel"/>.
/// </summary>
public static class Update
{
    /// <inheritdoc />
    public sealed record Command(Guid CommunicationId, CommunicationModel Model) : IRequest<MessageModel>
    {
    }

    /// <inheritdoc />
    public sealed class Validator : AbstractValidator<Command>
    {
        /// <summary>
        /// Инициализация экземпляра <see cref="Validator"/>.
        /// </summary>
        /// <param name="validator">Communication model validator.</param>
        public Validator(IValidator<CommunicationModel> validator)
        {
            RuleFor(e => e.CommunicationId)
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
            _logger.LogDebug("Получен запрос на обновление данных коммуникационного модуля '{Model}' в системе.", model);

            var dbCommunication = _context.Communications
                .Include(e => e.Protocols)
                .Search(model.Id);

            if (dbCommunication.Default)
            {
                throw new MtException(ErrorCode.EntityCannotBeModified, $"Сущность по умолчанию '{dbCommunication}' не может быть обновлена.");
            }

            var dbProtocols = _context.Protocols
                .SearchManyOrDefault(model.Protocols.Select(e => e.Id));
            dbCommunication.GetBuilder()
                .SetAttributes(model)
                .SetProtocols(dbProtocols)
                .Build();

            return SaveChangesAsync(dbCommunication, cancellationToken);
        }

        /// <summary>
        /// Сохранить изменения сущности.
        /// </summary>
        /// <param name="entity">Сущность.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Результат выполнения.</returns>
        private async Task<MessageModel> SaveChangesAsync(CommunicationEntity entity, CancellationToken cancellationToken)
        {
            _context.Communications.Update(entity);
            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Коммуникационный модуль '{Entity}' успешно обновлен в системе.", entity);
            return new MessageModel
            {
                Message = $"'{entity}' обновлен в системе.",
            };
        }
    }
}