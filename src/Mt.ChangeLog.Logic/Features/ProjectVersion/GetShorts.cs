using MediatR;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using Mt.ChangeLog.DataContext;
using Mt.ChangeLog.Logic.Mappers;
using Mt.ChangeLog.TransferObjects.ProjectVersion;

namespace Mt.ChangeLog.Logic.Features.ProjectVersion;

/// <summary>
/// Запрос на получение перечня кратких моделей данных <see cref="ProjectVersionShortModel"/>.
/// </summary>
public static class GetShorts
{
    /// <inheritdoc />
    public sealed class Query : IRequest<IReadOnlyCollection<ProjectVersionShortModel>>
    {
    }

    /// <inheritdoc />
    public sealed class Handler : IRequestHandler<Query, IReadOnlyCollection<ProjectVersionShortModel>>
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
        public async Task<IReadOnlyCollection<ProjectVersionShortModel>> Handle(Query request, CancellationToken cancellationToken)
        {
            _logger.LogDebug("Получен запрос на получение полного перечня краткого описания версий проектов.");

            var result = await _context.ProjectVersions.AsNoTracking()
                .Include(e => e.AnalogModule)
                .OrderBy(e => e.AnalogModule!.Title).ThenBy(e => e.Title).ThenBy(e => e.Version)
                .Select(e => e.ToShortModel())
                .ToListAsync(cancellationToken);

            _logger.LogDebug("Запрос на получение полного перечня краткого описания версий проектов успешно выполнен, '{Count}' записей.", result.Count);
            return result;
        }
    }
}