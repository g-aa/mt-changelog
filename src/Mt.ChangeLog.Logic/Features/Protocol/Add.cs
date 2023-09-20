using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Mt.ChangeLog.DataContext;
using Mt.ChangeLog.Entities.Tables;
using Mt.ChangeLog.Logic.Mappers;
using Mt.ChangeLog.TransferObjects.Other;
using Mt.ChangeLog.TransferObjects.Protocol;
using Mt.Entities.Abstractions.Extensions;
using Mt.Utilities.Exceptions;

namespace Mt.ChangeLog.Logic.Features.Protocol;

/// <summary>
/// Запрос на добавления сущности <see cref="ProtocolModel"/>.
/// </summary>
public static class Add
{
    /// <inheritdoc />
    public sealed record Command(ProtocolModel Model) : IRequest<MessageModel>
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
            this.logger.LogDebug("Получен запрос на добавление протокола '{Model}' в систему.", model);

            var dbModules = this.context.Communications
                .SearchManyOrDefault(model.Communications.Select(e => e.Id));

            var dbProtocol = new ProtocolEntity().GetBuilder()
                .SetAttributes(model)
                .SetModules(dbModules)
                .Build();

            if (this.context.Protocols.IsContained(dbProtocol))
            {
                throw new MtException(ErrorCode.EntityAlreadyExists, $"Сущность '{dbProtocol}' уже содержится в системе.");
            }

            return this.SaveChangesAsync(dbProtocol, cancellationToken);
        }

        /// <summary>
        /// Сохранить изменения сущности.
        /// </summary>
        /// <param name="entity">Сущность.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Результат выполнения.</returns>
        private async Task<MessageModel> SaveChangesAsync(ProtocolEntity entity, CancellationToken cancellationToken)
        {
            await this.context.Protocols.AddAsync(entity, cancellationToken);
            await this.context.SaveChangesAsync(cancellationToken);

            this.logger.LogInformation("Протокол '{Entity}' успешно добавлен в систему.", entity);
            return new MessageModel
            {
                Message = $"'{entity}' был добавлен в систему.",
            };
        }
    }
}