using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Mt.ChangeLog.DataContext;
using Mt.ChangeLog.Logic.Mappers;
using Mt.ChangeLog.TransferObjects.Other;

namespace Mt.ChangeLog.Logic.Features.History;

/// <summary>
/// Запрос на получение статистики по данным в системе.
/// </summary>
public sealed class GetStatistics
{
    /// <inheritdoc />
    public sealed class Query : IRequest<StatisticsModel>
    {
    }

    /// <inheritdoc />
    public sealed class Handler : IRequestHandler<Query, StatisticsModel>
    {
        private readonly ILogger<Handler> logger;

        private readonly MtContext context;

        /// <summary>
        /// Инициализация нового экземпляра класса <see cref="Handler"/>.
        /// </summary>
        /// <param name="logger">Журнал логирования.</param>
        /// <param name="context">Контекст данных.</param>
        public Handler(ILogger<Handler> logger, MtContext context)
        {
            this.logger = logger;
            this.context = context;
        }

        /// <inheritdoc />
        public async Task<StatisticsModel> Handle(Query request, CancellationToken cancellationToken)
        {
            this.logger.LogDebug("Получен запрос на предоставление статистики по данным в системе.");

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

            var result = new StatisticsModel
            {
                ArmEdit = sArmEdit.Version,
                ProjectCount = distributions.Sum(e => e.Value),
                ProjectDistributions = distributions,
                AuthorContributions = contributions,
                LastModifiedProjects = lastModifiedProjects,
            };

            return result;
        }
    }
}