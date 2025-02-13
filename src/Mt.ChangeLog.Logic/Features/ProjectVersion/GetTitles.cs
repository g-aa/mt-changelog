using MediatR;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using Mt.ChangeLog.DataContext;

namespace Mt.ChangeLog.Logic.Features.ProjectVersion;

/// <summary>
/// Запрос на получение перечня наименование версий проектов.
/// </summary>
public static class GetTitles
{
    /// <inheritdoc />
    public sealed class Query : IRequest<IReadOnlyCollection<string>>
    {
    }

    /// <inheritdoc />
    public sealed class Handler : IRequestHandler<Query, IReadOnlyCollection<string>>
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
        public async Task<IReadOnlyCollection<string>> Handle(Query request, CancellationToken cancellationToken)
        {
            _logger.LogDebug("Получен запрос на получение перечня наименование версий проектов.");

            var result = await _context.ProjectVersions.AsNoTracking()
                .Select(e => e.Title)
                .Distinct()
                .OrderBy(e => e)
                .ToListAsync(cancellationToken);

            _logger.LogDebug("Запрос нна получение перечня наименование версий проектов выполнен успешно, '{Count}' записей.", result.Count);
            return result;
        }
    }
}