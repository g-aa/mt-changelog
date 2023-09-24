using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Mt.ChangeLog.DataContext;
using Mt.ChangeLog.Entities.Tables;
using Mt.ChangeLog.Logic.Mappers;
using Mt.ChangeLog.TransferObjects.Communication;
using Mt.ChangeLog.TransferObjects.Other;
using Mt.Entities.Abstractions.Extensions;
using Mt.Utilities.Exceptions;

namespace Mt.ChangeLog.Logic.Features.Communication;

/// <summary>
/// Запрос на добавления сущности <see cref="CommunicationModel"/>.
/// </summary>
public static class Add
{
    /// <inheritdoc />
    public sealed record Command(CommunicationModel Model) : IRequest<MessageModel>
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
            this.logger.LogDebug("Получен запрос на добавление коммуникационного модуля '{Model}' в систему.", model);

            var dbProtocols = this.context.Protocols
                .SearchManyOrDefault(model.Protocols.Select(e => e.Id));

            var dbCommunication = new CommunicationEntity().GetBuilder()
                .SetAttributes(model)
                .SetProtocols(dbProtocols)
                .Build();

            if (this.context.Communications.IsContained(dbCommunication))
            {
                throw new MtException(ErrorCode.EntityAlreadyExists, $"Сущность '{dbCommunication}' уже содержится в системе.");
            }

            return this.SaveChangesAsync(dbCommunication, cancellationToken);
        }

        /// <summary>
        /// Сохранить изменения сущности.
        /// </summary>
        /// <param name="entity">Сущность.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Результат выполнения.</returns>
        private async Task<MessageModel> SaveChangesAsync(CommunicationEntity entity, CancellationToken cancellationToken)
        {
            await this.context.Communications.AddAsync(entity, cancellationToken);
            await this.context.SaveChangesAsync(cancellationToken);

            this.logger.LogInformation("Коммуникационный модуль '{Entity}' успешно добавлен в систему.", entity);
            return new MessageModel
            {
                Message = $"'{entity}' был добавлен в систему.",
            };
        }
    }
}