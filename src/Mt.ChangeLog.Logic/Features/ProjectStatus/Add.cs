using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Mt.ChangeLog.Context;
using Mt.ChangeLog.Entities.Extensions.Tables;
using Mt.ChangeLog.Entities.Tables;
using Mt.ChangeLog.Logic.Models;
using Mt.ChangeLog.TransferObjects.Other;
using Mt.ChangeLog.TransferObjects.ProjectStatus;
using Mt.Entities.Abstractions.Extensions;
using Mt.Utilities;
using Mt.Utilities.Exceptions;

namespace Mt.ChangeLog.Logic.Features.ProjectStatus
{
    /// <summary>
    /// Запрос на добавления сущности <see cref="ProjectStatusModel"/>.
    /// </summary>
    public static class Add
    {
        /// <inheritdoc />
        public sealed class Command : MtCommand<ProjectStatusModel, MessageModel>, IValidatedRequest
        {
            /// <summary>
            /// Инициализация нового экземпляра класса <see cref="Command"/>.
            /// </summary>
            /// <param name="model">Модель данных.</param>
            public Command(ProjectStatusModel model) : base(model)
            {
            }

            /// <inheritdoc />
            public override string ToString()
            {
                return $"{base.ToString()} - добавление сущности вида {nameof(ProjectStatusModel)}.";
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
            public CommandValidator(ProjectStatusValidator validator)
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

                var dbProjectStatus = ProjectStatusBuilder.GetBuilder()
                    .SetAttributes(model)
                    .Build();

                if (this.context.ProjectStatuses.IsContained(dbProjectStatus))
                {
                    throw new MtException(ErrorCode.EntityAlreadyExists, $"Сущность '{dbProjectStatus}' уже содержится в системе.");
                }

                return this.SaveChangesAsync(dbProjectStatus, cancellationToken);
            }

            /// <summary>
            /// Сохранить изменения сущности.
            /// </summary>
            /// <param name="entity">Сущность.</param>
            /// <param name="cancellationToken">Токен отмены.</param>
            /// <returns>Результат выполнения.</returns>
            private async Task<MessageModel> SaveChangesAsync(ProjectStatusEntity entity, CancellationToken cancellationToken)
            {
                await this.context.ProjectStatuses.AddAsync(entity, cancellationToken);
                await this.context.SaveChangesAsync(cancellationToken);
                return new MessageModel()
                {
                    Message = $"'{entity}' был добавлен в систему.",
                };
            }
        }
    }
}