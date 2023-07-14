using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Mt.ChangeLog.Context;
using Mt.ChangeLog.Entities.Extensions.Tables;
using Mt.ChangeLog.Entities.Tables;
using Mt.ChangeLog.Logic.Models;
using Mt.ChangeLog.TransferObjects.Other;
using Mt.ChangeLog.TransferObjects.Protocol;
using Mt.Entities.Abstractions.Extensions;
using Mt.Utilities;
using Mt.Utilities.Exceptions;

namespace Mt.ChangeLog.Logic.Features.Protocol
{
    /// <summary>
    /// Запрос на обновление сущности <see cref="ProtocolModel"/>.
    /// </summary>
    public static class Update
    {
        /// <inheritdoc />
        public sealed class Command : MtCommand<ProtocolModel, MessageModel>, IValidatedRequest
        {
            /// <summary>
            /// Инициализация нового экземпляра класса <see cref="Command"/>.
            /// </summary>
            /// <param name="model">Модель данных.</param>
            public Command(ProtocolModel model) : base(model)
            {
            }

            /// <inheritdoc />
            public override string ToString()
            {
                return $"{base.ToString()} - обновление сущности вида {nameof(ProtocolModel)}.";
            }
        }

        /// <summary>
        /// Валидатор модели <see cref="Command"/>.
        /// </summary>
        public sealed class CommandValidator : AbstractValidator<Command>
        {
            /// <summary>
            /// Инициализация экземпляра <see cref="CommandValidator"/>.
            /// </summary>
            public CommandValidator(ProtocolValidator validator)
            {
                this.RuleFor(e => e.Model)
                    .SetValidator(Check.NotNull(validator, nameof(validator)));
            }
        }

        /// <inheritdoc />
        public sealed class Handler : IRequestHandler<Command, MessageModel>
        {
            /// <summary>
            /// Журнал логирования.
            /// </summary>
            private readonly ILogger<Handler> logger;

            /// <summary>
            /// Контекст данных.
            /// </summary>
            private readonly MtContext context;

            /// <summary>
            /// Инициализация нового экземпляра класса <see cref="Handler"/>.
            /// </summary>
            /// <param name="logger">Журнал логирования.</param>
            /// <param name="context">Контекст данных.</param>
            public Handler(ILogger<Handler> logger, MtContext context)
            {
                this.logger = Check.NotNull(logger, nameof(logger));
                this.context = Check.NotNull(context, nameof(context));
            }

            /// <inheritdoc />
            public Task<MessageModel> Handle(Command request, CancellationToken cancellationToken)
            {
                var model = Check.NotNull(request, nameof(request)).Model;
                this.logger.LogInformation(request.ToString());

                var dbProtocol = this.context.Protocols.Search(model.Id);
                if (dbProtocol.Default)
                {
                    throw new MtException(ErrorCode.EntityCannotBeModified, $"Сущность по умолчанию '{dbProtocol}' не может быть обновлена.");
                }

                var dbModules = this.context.Communications
                    .SearchManyOrDefault(model.Communications.Select(e => e.Id));
                dbProtocol.GetBuilder()
                    .SetAttributes(model)
                    .SetModules(dbModules)
                    .Build();

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
                this.context.Protocols.Update(entity);
                await this.context.SaveChangesAsync(cancellationToken);
                return new MessageModel()
                {
                    Message = $"'{entity}' обновлен в системе.",
                };
            }
        }
    }
}