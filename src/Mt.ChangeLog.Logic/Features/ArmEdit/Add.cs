using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Mt.ChangeLog.DataContext;
using Mt.ChangeLog.Entities.Tables;
using Mt.ChangeLog.Logic.Extensions;
using Mt.ChangeLog.TransferObjects.ArmEdit;
using Mt.ChangeLog.TransferObjects.Other;
using Mt.Entities.Abstractions.Extensions;
using Mt.Utilities.Exceptions;

namespace Mt.ChangeLog.Logic.Features.ArmEdit;

/// <summary>
/// Запрос на добавления сущности <see cref="ArmEditModel"/>.
/// </summary>
public static class Add
{
    /// <inheritdoc />
    public sealed record Command(ArmEditModel Model) : IRequest<MessageModel>
    {
    }

    /// <inheritdoc />
    public sealed class Validator : AbstractValidator<Command>
    {
        /// <summary>
        /// Инициализация экземпляра <see cref="Validator"/>.
        /// </summary>
        /// <param name="validator">ArmEdit model validator.</param>
        public Validator(IValidator<ArmEditModel> validator)
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
            _logger.LogDebug("Получен запрос на добавление ArmEdit '{Model}' в систему.", model);

            var dbArmEdit = new ArmEditEntity().GetBuilder()
                .SetAttributes(model)
                .Build();

            if (_context.ArmEdits.IsContained(dbArmEdit))
            {
                throw new MtException(ErrorCode.EntityAlreadyExists, $"Сущность '{dbArmEdit}' уже содержится в системе.");
            }

            _context.ArmEdits.Add(dbArmEdit);
            return SaveChangesAsync(dbArmEdit, cancellationToken);
        }

        private async Task<MessageModel> SaveChangesAsync(ArmEditEntity entity, CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("ArmEdit '{Entity}' успешно добавлен в систему.", entity);
            return new MessageModel
            {
                Message = $"'{entity}' был добавлен в систему.",
            };
        }
    }
}