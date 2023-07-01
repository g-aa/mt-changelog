using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Mt.ChangeLog.DataAccess.Abstractions;
using Mt.ChangeLog.Logic.Models;
using Mt.ChangeLog.TransferObjects.Author;
using Mt.ChangeLog.TransferObjects.Other;
using Mt.Utilities;

namespace Mt.ChangeLog.Logic.Features.Author
{
    /// <summary>
    /// Запрос на получение перечня моделий данных для таблиц <see cref="AuthorModel"/>.
    /// </summary>
    public static class GetById
    {
        /// <inheritdoc />
        public sealed class Query : MtQuery<BaseModel, AuthorModel>, IValidatedRequest
        {
            /// <summary>
            /// Инициализация нового экземпляра класса <see cref="Query"/>.
            /// </summary>
            /// <param name="model">Базовая модель.</param>
            public Query(BaseModel model) : base(model)
            {
            }

            /// <inheritdoc />
            public override string ToString()
            {
                return $"{base.ToString()} - получение сущности вида {nameof(AuthorModel)}.";
            }
        }

        /// <summary>
        /// Валидатор модели <see cref="Query"/>.
        /// </summary>
        public sealed class QueryValidator : AbstractValidator<Query>
        {
            /// <summary>
            /// Инициализация экземпляра <see cref="QueryValidator"/>.
            /// </summary>
            public QueryValidator(BaseModelValidator validator)
            {
                this.RuleFor(e => e.Model)
                    .SetValidator(Check.NotNull(validator, nameof(validator)));
            }
        }

        /// <inheritdoc />
        public sealed class Handler : IRequestHandler<Query, AuthorModel>
        {
            /// <summary>
            /// Журнал логирования.
            /// </summary>
            private readonly ILogger<Handler> logger;

            /// <summary>
            /// Репозиторий с данными.
            /// </summary>
            private readonly IAuthorRepository repository;

            /// <summary>
            /// Инициализация нового экземпляра класса <see cref="Handler"/>.
            /// </summary>
            /// <param name="logger">Журнал логирования.</param>
            /// <param name="repository">Репозиторий с данными.</param>
            public Handler(ILogger<Handler> logger, IAuthorRepository repository)
            {
                this.logger = Check.NotNull(logger, nameof(logger));
                this.repository = Check.NotNull(repository, nameof(repository));
            }

            /// <inheritdoc />
            public async Task<AuthorModel> Handle(Query request, CancellationToken cancellationToken)
            {
                Check.NotNull(request, nameof(request));
                this.logger.LogInformation(request.ToString());

                var result = await this.repository.GetEntityAsync(request.Model.Id);
                return result;
            }
        }
    }
}