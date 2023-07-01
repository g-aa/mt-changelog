using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Mt.ChangeLog.Context;
using Mt.ChangeLog.Entities.Extensions.Tables;
using Mt.ChangeLog.Entities.Tables;
using Mt.ChangeLog.Logic.Models;
using Mt.ChangeLog.TransferObjects.Communication;
using Mt.Entities.Abstractions.Extensions;
using Mt.Utilities;
using Mt.Utilities.Exceptions;

namespace Mt.ChangeLog.Logic.Features.Communication
{
    /// <summary>
    /// Запрос на обновление сущности <see cref="CommunicationModel"/>.
    /// </summary>
    public static class Update
    {
        /// <inheritdoc />
        public sealed class Command : MtCommand<CommunicationModel, string>, IValidatedRequest
        {
            /// <summary>
            /// Инициализация нового экземпляра класса <see cref="Command"/>.
            /// </summary>
            /// <param name="model">Базовая модель.</param>
            public Command(CommunicationModel model) : base(model)
            {
            }

            /// <inheritdoc />
            public override string ToString()
            {
                return $"{base.ToString()} - обновление сущности вида {nameof(CommunicationModel)}.";
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
            public CommandValidator(CommunicationModelValidator validator)
            {
                this.RuleFor(e => e.Model)
                    .SetValidator(Check.NotNull(validator, nameof(validator)));
            }
        }

        /// <inheritdoc />
        public sealed class Handler : IRequestHandler<Command, string>
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
            public Task<string> Handle(Command request, CancellationToken cancellationToken)
            {
                var model = Check.NotNull(request, nameof(request)).Model;
                this.logger.LogInformation(request.ToString());

                var dbCommunication = this.context.Communications
                    .Include(e => e.Protocols)
                    .Search(model.Id);

                if (dbCommunication.Default)
                {
                    throw new MtException(ErrorCode.EntityCannotBeModified, $"Сущность по умолчанию '{dbCommunication}' не может быть обновлена.");
                }

                var dbProtocols = this.context.Protocols
                    .SearchManyOrDefault(model.Protocols.Select(e => e.Id));
                dbCommunication.GetBuilder()
                    .SetAttributes(model)
                    .SetProtocols(dbProtocols)
                    .Build();

                return this.SaveChangesAsync(dbCommunication, cancellationToken);
            }

            /// <summary>
            /// Сохранить изменения сущности.
            /// </summary>
            /// <param name="entity">Сущность.</param>
            /// <param name="cancellationToken">Токен отмены.</param>
            /// <returns>Результат выполнения.</returns>
            private async Task<string> SaveChangesAsync(CommunicationEntity entity, CancellationToken cancellationToken)
            {
                this.context.Communications.Update(entity);
                await this.context.SaveChangesAsync(cancellationToken);
                return $"'{entity}' обновлен в системе.";
            }
        }
    }
}