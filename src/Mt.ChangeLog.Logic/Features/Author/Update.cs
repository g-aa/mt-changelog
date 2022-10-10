using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Mt.ChangeLog.Context;
using Mt.ChangeLog.Entities.Extensions.Tables;
using Mt.ChangeLog.Logic.Models;
using Mt.ChangeLog.TransferObjects.Author;
using Mt.Entities.Abstractions.Extensions;
using Mt.Utilities;
using Mt.Utilities.Exceptions;
using System.Threading;
using System.Threading.Tasks;

namespace Mt.ChangeLog.Logic.Features.Author
{
    /// <summary>
    /// Запрос на обновление сущности <see cref="AuthorModel"/>.
    /// </summary>
    public static class Update
    {
        /// <inheritdoc />
        public sealed class Command : MtCommand<AuthorModel, string>, IValidatedRequest
        {
            /// <summary>
            /// Инициализация нового экземпляра класса <see cref="Command"/>.
            /// </summary>
            /// <param name="model">Модель данных.</param>
            public Command(AuthorModel model) : base(model)
            {
            }

            /// <inheritdoc />
            public override string ToString()
            {
                return $"{base.ToString()} - обновление сущности вида {nameof(AuthorModel)}.";
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
            public CommandValidator(AuthorModelValidator validator)
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

                var dbAuthor = this.context.Authors.Search(model.Id);
                if (dbAuthor.Default)
                {
                    throw new MtException(ErrorCode.EntityCannotBeModified, $"Сущность по умолчанию '{dbAuthor}' не может быть обновлена.");
                }

                dbAuthor.GetBuilder().SetAttributes(model).Build();
                return this.SaveChangesAsync(dbAuthor, cancellationToken);
            }

            /// <summary>
            /// Сохранить изменения сущности.
            /// </summary>
            /// <param name="entity">Сущность.</param>
            /// <param name="cancellationToken">Токен отмены.</param>
            /// <returns>Результат выполнения.</returns>
            private async Task<string> SaveChangesAsync(Mt.ChangeLog.Entities.Tables.Author entity, CancellationToken cancellationToken)
            {
                this.context.Authors.Update(entity);
                await this.context.SaveChangesAsync(cancellationToken);
                return $"'{entity}' обновлен в системе.";
            }
        }
    }
}