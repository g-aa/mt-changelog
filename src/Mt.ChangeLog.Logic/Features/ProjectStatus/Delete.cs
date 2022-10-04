using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Mt.ChangeLog.Context;
using Mt.ChangeLog.Logic.Models;
using Mt.ChangeLog.TransferObjects.Other;
using Mt.ChangeLog.TransferObjects.ProjectStatus;
using Mt.Entities.Abstractions.Extensions;
using Mt.Utilities;
using Mt.Utilities.Exceptions;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Mt.ChangeLog.Logic.Features.ProjectStatus
{
    /// <summary>
    /// Запрос на удаления модели аналогового модуля из системы <see cref="ProjectStatusModel"/>.
    /// </summary>
    public static class Delete
    {
        /// <inheritdoc />
        public sealed class Command : MtCommand<BaseModel, StatusModel>, IValidatedRequest
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
                return $"{base.ToString()} - удаление сущности вида {nameof(ProjectStatusModel)}.";
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
        public sealed class Handler : IRequestHandler<Command, StatusModel>
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
            public Task<StatusModel> Handle(Command request, CancellationToken cancellationToken)
            {
                var model = Check.NotNull(request, nameof(request)).Model;
                this.logger.LogInformation(request.ToString());

                var dbRemovable = this.context.ProjectStatuses
                    .Include(e => e.ProjectVersions)
                    .Search(model.Id);

                if (dbRemovable.Default)
                {
                    throw new MtException(ErrorCode.EntityCannotBeDeleted, $"Сущность по умолчанию '{dbRemovable}' не может быть удалена из системы.");
                }

                if (dbRemovable.ProjectVersions.Any())
                {
                    throw new MtException(ErrorCode.EntityCannotBeDeleted, $"Сущность '{dbRemovable}' используется в проектах и неможет быть удалена из системы.");
                }

                return this.SaveChangesAsync(dbRemovable, cancellationToken);
            }

            /// <summary>
            /// Сохранить изменения сущности.
            /// </summary>
            /// <param name="entity">Сущность.</param>
            /// <param name="cancellationToken">Токен отмены.</param>
            /// <returns>Результат выполнения.</returns>
            private async Task<StatusModel> SaveChangesAsync(Mt.ChangeLog.Entities.Tables.ProjectStatus entity, CancellationToken cancellationToken)
            {
                this.context.ProjectStatuses.Remove(entity);
                await this.context.SaveChangesAsync(cancellationToken);
                return new StatusModel($"'{entity}' был удален из системы.");
            }
        }
    }
}