using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Mt.ChangeLog.Context;
using Mt.ChangeLog.Entities.Extensions.Tables;
using Mt.ChangeLog.Entities.Tables;
using Mt.ChangeLog.Logic.Models;
using Mt.ChangeLog.TransferObjects.ArmEdit;
using Mt.Entities.Abstractions.Extensions;
using Mt.Utilities;
using Mt.Utilities.Exceptions;
using System.Threading;
using System.Threading.Tasks;

namespace Mt.ChangeLog.Logic.Features.ArmEdit
{
    /// <summary>
    /// Запрос на добавления сущности <see cref="ArmEditModel"/>.
    /// </summary>
    public static class Add
    {
        /// <inheritdoc />
        public sealed class Command : MtCommand<ArmEditModel, string>, IValidatedRequest
        {
            /// <summary>
            /// Инициализация нового экземпляра класса <see cref="Command"/>.
            /// </summary>
            /// <param name="model">Модель данных.</param>
            public Command(ArmEditModel model) : base(model)
            {
            }

            /// <inheritdoc />
            public override string ToString()
            {
                return $"{base.ToString()} - добавление сущности вида {nameof(ArmEditModel)}.";
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
            public CommandValidator(ArmEditModelValidator validator)
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

                var dbArmEdit = ArmEditBuilder.GetBuilder()
                    .SetAttributes(model)
                    .Build();

                if (this.context.ArmEdits.IsContained(dbArmEdit))
                {
                    throw new MtException(ErrorCode.EntityAlreadyExists, $"Сущность '{dbArmEdit}' уже содержится в системе.");
                }

                return this.SaveChangesAsync(dbArmEdit, cancellationToken);
            }

            /// <summary>
            /// Сохранить изменения сущности.
            /// </summary>
            /// <param name="entity">Сущность.</param>
            /// <param name="cancellationToken">Токен отмены.</param>
            /// <returns>Результат выполнения.</returns>
            private async Task<string> SaveChangesAsync(ArmEditEntity entity, CancellationToken cancellationToken)
            {
                await this.context.ArmEdits.AddAsync(entity);
                await this.context.SaveChangesAsync(cancellationToken);
                return $"'{entity}' был добавлен в систему.";
            }
        }
    }
}