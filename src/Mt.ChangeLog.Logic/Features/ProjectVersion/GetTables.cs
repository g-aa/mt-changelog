using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Mt.ChangeLog.DataContext;
using Mt.ChangeLog.Logic.Mappers;
using Mt.ChangeLog.TransferObjects.ProjectVersion;

namespace Mt.ChangeLog.Logic.Features.ProjectVersion;

/// <summary>
/// Запрос на получение перечня моделей данных для таблиц <see cref="ProjectVersionTableModel"/>.
/// </summary>
public static class GetTables
{
    /// <inheritdoc />
    public sealed class Query : IRequest<IReadOnlyCollection<ProjectVersionTableModel>>
    {
    }

    /// <inheritdoc />
    public sealed class Handler : IRequestHandler<Query, IReadOnlyCollection<ProjectVersionTableModel>>
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
        public async Task<IReadOnlyCollection<ProjectVersionTableModel>> Handle(Query request, CancellationToken cancellationToken)
        {
            _logger.LogDebug("Получен запрос на получение полного перечня табличного описания версий проекта.");

            var result = await _context.ProjectVersions.AsNoTracking()
                .Include(e => e.AnalogModule)
                .Include(e => e.Platform)
                .Include(e => e.ProjectStatus)
                .OrderBy(e => e.AnalogModule!.Title).ThenBy(e => e.Title).ThenBy(e => e.Version)
                .Select(e => e.ToTableModel())
                .ToListAsync(cancellationToken);

            _logger.LogDebug("Запрос на получение полного перечня табличного описания версий проекта успешно выполнен, '{Count}' записей.", result.Count);
            return result;
        }
    }
}