using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Mt.ChangeLog.Context;
using Mt.ChangeLog.Logic.Models;
using Mt.ChangeLog.TransferObjects.Other;
using Mt.ChangeLog.TransferObjects.ProjectVersion;
using Mt.Entities.Abstractions.Extensions;
using Mt.Utilities;

namespace Mt.ChangeLog.Logic.Features.ProjectVersion
{
    /// <summary>
    /// Запрос на удаления модели аналогового модуля из системы <see cref="ProjectVersionModel"/>.
    /// </summary>
    public static class Delete
    {
        /// <inheritdoc />
        public sealed class Command : MtCommand<BaseModel, MessageModel>, IValidatedRequest
        {
            /// <summary>
            /// Инициализация нового экземпляра класса <see cref="Command"/>.
            /// </summary>
            /// <param name="model">Базовая модель.</param>
            public Command(BaseModel model) : base(model)
            {
            }

            /// <inheritdoc />
            public override string ToString()
            {
                return $"{base.ToString()} - удаление сущности вида {nameof(ProjectVersionModel)}.";
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
            public CommandValidator(BaseModelValidator validator)
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
            public async Task<MessageModel> Handle(Command request, CancellationToken cancellationToken)
            {
                Check.NotNull(request, nameof(request));
                this.logger.LogInformation(request.ToString());

                var dbRemovable = this.context.ProjectVersions.Search(request.Model.Id);
                this.context.ProjectVersions.Remove(dbRemovable);
                await this.context.SaveChangesAsync();

                return new MessageModel()
                {
                    Message = $"'{dbRemovable}' был удалена из системы.",
                };
            }
        }
    }
}