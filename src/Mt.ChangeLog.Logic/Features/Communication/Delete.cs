using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Mt.ChangeLog.Context;
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
    /// Запрос на удаления модели аналогового модуля из системы <see cref="CommunicationModel"/>.
    /// </summary>
    public static class Delete
    {
        /// <inheritdoc />
        public sealed class Command : MtCommand<BaseModel, string>, IValidatedRequest
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
                return $"{base.ToString()} - удаление сущности вида {nameof(CommunicationModel)}.";
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
                Check.NotNull(request, nameof(request));
                this.logger.LogInformation(request.ToString());

                var dbRemovable = this.context.Communications
                    .Include(e => e.ProjectRevisions)
                    .Include(e => e.Protocols).ThenInclude(e => e.Communications)
                    .AsSingleQuery()
                    .Search(request.Model.Id);

                if (dbRemovable.Default)
                {
                    throw new MtException(ErrorCode.EntityCannotBeDeleted, $"Сущность по умолчанию '{dbRemovable}' не может быть удалена из системы.");
                }

                if (dbRemovable.ProjectRevisions.Any())
                {
                    throw new MtException(ErrorCode.EntityCannotBeDeleted, $"Сущность '{dbRemovable}' используемая в редакциях проектов не может быть удалена из системы.");

                }

                if (dbRemovable.Protocols.Any())
                {
                    var defModule = this.context.Communications.First(e => e.Default);
                    foreach (var dbProtocols in dbRemovable.Protocols.Where(p => p.Communications.Remove(dbRemovable) && !p.Communications.Any()))
                    {
                        dbProtocols.Communications.Add(defModule);
                    }
                }

                return this.SaveChangesAsync(dbRemovable, cancellationToken);
            }

            /// <summary>
            /// Сохранить изменения сущности.
            /// </summary>
            /// <param name="entity">Сущность.</param>
            /// <param name="cancellationToken">Токен отмены.</param>
            /// <returns>Результат выполнения.</returns>
            private async Task<string> SaveChangesAsync(Mt.ChangeLog.Entities.Tables.Communication entity, CancellationToken cancellationToken)
            {
                this.context.Communications.Remove(entity);
                await this.context.SaveChangesAsync(cancellationToken);
                return $"'{entity}' был удален из системы.";
            }
        }
    }
}