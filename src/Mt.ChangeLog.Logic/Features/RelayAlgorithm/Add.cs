using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Mt.ChangeLog.Context;
using Mt.ChangeLog.Entities.Extensions.Tables;
using Mt.ChangeLog.Entities.Tables;
using Mt.ChangeLog.Logic.Models;
using Mt.ChangeLog.TransferObjects.Other;
using Mt.ChangeLog.TransferObjects.RelayAlgorithm;
using Mt.Entities.Abstractions.Extensions;
using Mt.Utilities;
using Mt.Utilities.Exceptions;

namespace Mt.ChangeLog.Logic.Features.RelayAlgorithm
{
    /// <summary>
    /// Запрос на добавления сущности <see cref="RelayAlgorithmModel"/>.
    /// </summary>
    public static class Add
    {
        /// <inheritdoc />
        public sealed class Command : MtCommand<RelayAlgorithmModel, MessageModel>, IValidatedRequest
        {
            /// <summary>
            /// Инициализация нового экземпляра класса <see cref="Command"/>.
            /// </summary>
            /// <param name="model">Модель данных.</param>
            public Command(RelayAlgorithmModel model) : base(model)
            {
            }

            /// <inheritdoc />
            public override string ToString()
            {
                return $"{base.ToString()} - добавление сущности вида {nameof(RelayAlgorithmModel)}.";
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
            public CommandValidator(RelayAlgorithmValidator validator)
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

                var dbAlgorithm = RelayAlgorithmBuilder.GetBuilder()
                    .SetAttributes(model)
                    .Build();

                if (this.context.RelayAlgorithms.IsContained(dbAlgorithm))
                {
                    throw new MtException(ErrorCode.EntityAlreadyExists, $"Сущность '{dbAlgorithm}' уже содержится в системе.");
                }

                return this.SaveChangesAsync(dbAlgorithm, cancellationToken);
            }

            /// <summary>
            /// Сохранить изменения сущности.
            /// </summary>
            /// <param name="entity">Сущность.</param>
            /// <param name="cancellationToken">Токен отмены.</param>
            /// <returns>Результат выполнения.</returns>
            private async Task<MessageModel> SaveChangesAsync(RelayAlgorithmEntity entity, CancellationToken cancellationToken)
            {
                await this.context.RelayAlgorithms.AddAsync(entity, cancellationToken);
                await this.context.SaveChangesAsync(cancellationToken);
                return new MessageModel()
                {
                    Message = $"'{entity}' был добавлен в систему.",
                };
            }
        }
    }
}