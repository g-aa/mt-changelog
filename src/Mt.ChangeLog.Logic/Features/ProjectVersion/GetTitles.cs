using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Mt.ChangeLog.Context;
using Mt.ChangeLog.Logic.Models;
using Mt.Utilities;

namespace Mt.ChangeLog.Logic.Features.ProjectVersion
{
    /// <summary>
    /// Запрос на получение перечня наименование версий проектов.
    /// </summary>
    public static class GetTitles
    {
        /// <inheritdoc />
        public sealed class Query : MtQuery<Unit, IEnumerable<string>>
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
                return $"{base.ToString()} - получение перечня наименование версий проектов.";
            }
        }

        /// <inheritdoc />
        public sealed class Handler : IRequestHandler<Query, IEnumerable<string>>
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
            public async Task<IEnumerable<string>> Handle(Query request, CancellationToken cancellationToken)
            {
                Check.NotNull(request, nameof(request));
                logger.LogInformation(request.ToString());

                var result = await context.ProjectVersions.AsNoTracking()
                    .Select(e => e.Title)
                    .Distinct()
                    .OrderBy(e => e)
                    .ToListAsync(cancellationToken);

                return result;
            }
        }
    }
}