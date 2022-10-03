using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Mt.ChangeLog.Context;
using Mt.ChangeLog.Entities.Extensions.Tables;
using Mt.ChangeLog.Logic.Models;
using Mt.ChangeLog.TransferObjects.Communication;
using Mt.ChangeLog.TransferObjects.Other;
using Mt.Entities.Abstractions.Extensions;
using Mt.Utilities;
using Mt.Utilities.Exceptions;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Mt.ChangeLog.Logic.Features.Communication
{
    /// <summary>
    /// Запрос на добавления сущности <see cref="CommunicationModel"/>.
    /// </summary>
    public static class Add
    {
        /// <inheritdoc />
        public sealed class Command : MtCommand<CommunicationModel, BaseModel>, IValidatedRequest
        {
            /// <summary>
            /// Инициализация нового экземпляра класса <see cref="Command"/>.
            /// </summary>
            /// <param name="model">Модель.</param>
            public Command(CommunicationModel model) : base(model)
            {
            }

            /// <inheritdoc />
            public override string ToString()
            {
                return $"{base.ToString()} - добавление сущности вида {nameof(CommunicationModel)}.";
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
        public sealed class Handler : IRequestHandler<Command, BaseModel>
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
            public Task<BaseModel> Handle(Command request, CancellationToken cancellationToken)
            {
                var model = Check.NotNull(request, nameof(request)).Model;
                this.logger.LogInformation(request.ToString());

                var dbProtocols = this.context.Protocols
                    .SearchManyOrDefault(model.Protocols.Select(e => e.Id));

                var dbCommunication = CommunicationBuilder.GetBuilder()
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
            private async Task<BaseModel> SaveChangesAsync(Mt.ChangeLog.Entities.Tables.Communication entity, CancellationToken cancellationToken)
            {
                await this.context.Communications.AddAsync(entity, cancellationToken);
                await this.context.SaveChangesAsync(cancellationToken);
                return new BaseModel()
                {
                    Id = entity.Id,
                };
            }
        }
    }
}