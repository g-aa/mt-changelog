using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Mt.ChangeLog.DataContext;
using Mt.ChangeLog.Logic.Mappers;
using Mt.ChangeLog.TransferObjects.ProjectRevision;

namespace Mt.ChangeLog.Logic.Features.ProjectRevision;

/// <summary>
/// Запрос на получение перечня кратких моделей данных <see cref="ProjectRevisionShortModel"/>.
/// </summary>
public static class GetShorts
{
    /// <inheritdoc />
    public sealed class Query : IRequest<IReadOnlyCollection<ProjectRevisionShortModel>>
    {
    }

    /// <inheritdoc />
    public sealed class Handler : IRequestHandler<Query, IReadOnlyCollection<ProjectRevisionShortModel>>
    {
        private readonly ILogger<Handler> _logger;

        private readonly MtContext _context;

        /// <summary>
        /// Инициализация нового экземпляра класса <see cref="Handler"/>.
        /// </summary>
        /// <param name="logger">Журнал логирования.</param>
        /// <param name="context">Контекст данных.</param>
        public Handler(ILogger<Handler> logger, MtContext context)
        {
            _logger = logger;
            _context = context;
        }

        /// <inheritdoc />
        public async Task<IReadOnlyCollection<ProjectRevisionShortModel>> Handle(Query request, CancellationToken cancellationToken)
        {
            _logger.LogDebug("Получен запрос на получение полного перечня краткого описания редакций проектов.");

            var result = await _context.ProjectRevisions.AsNoTracking()
                .Include(pr => pr.ProjectVersion!.AnalogModule)
                .OrderBy(pr => pr.ProjectVersion!.AnalogModule!.Title)
                .ThenBy(pr => pr.ProjectVersion!.Title)
                .ThenBy(pr => pr.ProjectVersion!.Version)
                .ThenBy(pr => pr.Revision)
                .Select(pr => pr.ToShortModel())
                .ToArrayAsync(cancellationToken);

            _logger.LogDebug("Запрос на получение полного перечня краткого описания редакций проектов успешно выполнен, '{Count}' записей.", result.Length);
            return result;
        }
    }
}