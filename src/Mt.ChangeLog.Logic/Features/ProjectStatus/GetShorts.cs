using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Mt.ChangeLog.Context;
using Mt.ChangeLog.Entities.Extensions.Tables;
using Mt.ChangeLog.Logic.Models;
using Mt.ChangeLog.TransferObjects.ProjectStatus;
using Mt.Utilities;

namespace Mt.ChangeLog.Logic.Features.ProjectStatus
{
    /// <summary>
    /// Запрос на получение перечня кратких моделий данных <see cref="ProjectStatusShortModel"/>.
    /// </summary>
    public static class GetShorts
    {
        /// <inheritdoc />
        public sealed class Query : MtQuery<Unit, IEnumerable<ProjectStatusShortModel>>
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
                return $"{base.ToString()} - получение перечня сущностей вида {nameof(ProjectStatusShortModel)}.";
            }
        }

        /// <inheritdoc />
        public sealed class Handler : IRequestHandler<Query, IEnumerable<ProjectStatusShortModel>>
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
            public async Task<IEnumerable<ProjectStatusShortModel>> Handle(Query request, CancellationToken cancellationToken)
            {
                Check.NotNull(request, nameof(request));
                this.logger.LogInformation(request.ToString());

                var result = await this.context.ProjectStatuses.AsNoTracking()
                    .OrderBy(e => e.Title)
                    .Select(e => e.ToShortModel())
                    .ToListAsync(cancellationToken);

                return result;
            }
        }
    }
}