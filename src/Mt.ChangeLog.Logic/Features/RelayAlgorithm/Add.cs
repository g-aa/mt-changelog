using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Mt.ChangeLog.DataContext;
using Mt.ChangeLog.Entities.Tables;
using Mt.ChangeLog.Logic.Extensions;
using Mt.ChangeLog.TransferObjects.Other;
using Mt.ChangeLog.TransferObjects.RelayAlgorithm;
using Mt.Entities.Abstractions.Extensions;
using Mt.Utilities.Exceptions;

namespace Mt.ChangeLog.Logic.Features.RelayAlgorithm;

/// <summary>
/// Запрос на добавления сущности <see cref="RelayAlgorithmModel"/>.
/// </summary>
public static class Add
{
    /// <inheritdoc />
    public sealed record Command(RelayAlgorithmModel Model) : IRequest<MessageModel>
    {
    }

    /// <inheritdoc />
    public sealed class Validator : AbstractValidator<Command>
    {
        /// <summary>
        /// Инициализация экземпляра <see cref="Validator"/>.
        /// </summary>
        /// <param name="validator">Relay algorithm model validator.</param>
        public Validator(IValidator<RelayAlgorithmModel> validator)
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
            _logger.LogDebug("Получен запрос на добавление алгоритма РЗиА '{Model}' в систему.", model);

            var dbAlgorithm = new RelayAlgorithmEntity().GetBuilder()
                .SetAttributes(model)
                .Build();

            if (_context.RelayAlgorithms.IsContained(dbAlgorithm))
            {
                throw new MtException(ErrorCode.EntityAlreadyExists, $"Сущность '{dbAlgorithm}' уже содержится в системе.");
            }

            return SaveChangesAsync(dbAlgorithm, cancellationToken);
        }

        /// <summary>
        /// Сохранить изменения сущности.
        /// </summary>
        /// <param name="entity">Сущность.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Результат выполнения.</returns>
        private async Task<MessageModel> SaveChangesAsync(RelayAlgorithmEntity entity, CancellationToken cancellationToken)
        {
            await _context.RelayAlgorithms.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Алгоритм РЗиА '{Entity}' успешно добавлен в систему.", entity);
            return new MessageModel
            {
                Message = $"'{entity}' был добавлен в систему.",
            };
        }
    }
}