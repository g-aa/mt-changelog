using MediatR;
using Microsoft.Extensions.Logging;
using Mt.ChangeLog.DataAccess.Abstractions;
using Mt.ChangeLog.Logic.Models;
using Mt.ChangeLog.TransferObjects.Author;
using Mt.Utilities;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Mt.ChangeLog.Logic.Features.Author
{
    /// <summary>
    /// Запрос на получение перечня кратких моделий данных <see cref="AuthorShortModel"/>.
    /// </summary>
    public static class GetShorts
    {
        /// <inheritdoc />
        public sealed class Query : MtQuery<Unit, IEnumerable<AuthorShortModel>>
        {
            /// <summary>
            /// Инициализация нового экземпляра класса <see cref="Query"/>.
            /// </summary>
            public Query() : base(Unit.Value)
            {
            }

            /// <inheritdoc />
            public override string ToString()
            {
                return $"{base.ToString()} - получение перечня сущностей вида {nameof(AuthorShortModel)}.";
            }
        }

        /// <inheritdoc />
        public sealed class Handler : IRequestHandler<Query, IEnumerable<AuthorShortModel>>
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
            public async Task<IEnumerable<AuthorShortModel>> Handle(Query request, CancellationToken cancellationToken)
            {
                Check.NotNull(request, nameof(request));
                this.logger.LogInformation(request.ToString());

                var result = await this.repository.GetShortEntitiesAsync();
                return result.OrderBy(e => e.LastName).ThenBy(e => e.FirstName);
            }
        }
    }
}