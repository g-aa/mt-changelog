using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Mt.ChangeLog.Context;
using Mt.ChangeLog.Entities.Extensions.Views;
using Mt.ChangeLog.Logic.Models;
using Mt.ChangeLog.TransferObjects.Other;
using Mt.Utilities;

namespace Mt.ChangeLog.Logic.Features.History
{
    /// <summary>
    /// Запрос на получение статистики по данным в системе.
    /// </summary>
    public sealed class GetStatistics
    {
        /// <inheritdoc />
        public sealed class Query : MtQuery<Unit, StatisticsModel>
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
                return $"{base.ToString()} - получение статистики по данным в системе.";
            }
        }

        /// <inheritdoc />
        public sealed class Handler : IRequestHandler<Query, StatisticsModel>
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
            public async Task<StatisticsModel> Handle(Query request, CancellationToken cancellationToken)
            {
                Check.NotNull(request, nameof(request));
                this.logger.LogInformation(request.ToString());

                var distributions = await this.context.ProjectStatuses.AsNoTracking()
                    .Include(e => e.ProjectVersions)
                    .OrderByDescending(e => e.ProjectVersions.Count)
                    .ToDictionaryAsync(k => k.Title, v => v.ProjectVersions.Count, cancellationToken);

                var sArmEdit = await this.context.ArmEdits.AsNoTracking()
                    .OrderByDescending(e => e.Version)
                    .FirstAsync(cancellationToken);

                var lastModifiedProjects = await this.context.LastProjectRevisions
                    .OrderByDescending(e => e.Date)
                    .Take(10)
                    .Select(e => e.ToHistoryShortModel())
                    .ToArrayAsync(cancellationToken);

                var contributions = await this.context.AuthorContributions
                    .Select(e => e.ToModel())
                    .ToArrayAsync(cancellationToken);

                var result = new StatisticsModel()
                {
                    ArmEdit = sArmEdit.Version,
                    ProjectCount = distributions.Sum(e => e.Value),
                    ProjectDistributions = distributions,
                    AuthorContributions = contributions,
                    LastModifiedProjects = lastModifiedProjects
                };

                return result;
            }
        }
    }
}