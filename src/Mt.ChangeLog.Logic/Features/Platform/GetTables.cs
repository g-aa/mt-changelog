using MediatR;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using Mt.ChangeLog.DataContext;
using Mt.ChangeLog.Logic.Mappers;
using Mt.ChangeLog.TransferObjects.Platform;

namespace Mt.ChangeLog.Logic.Features.Platform;

/// <summary>
/// Запрос на получение перечня моделей данных для таблиц <see cref="PlatformTableModel"/>.
/// </summary>
public static class GetTables
{
    /// <inheritdoc />
    public sealed class Query : IRequest<IReadOnlyCollection<PlatformTableModel>>
    {
    }

    /// <inheritdoc />
    public sealed class Handler : IRequestHandler<Query, IReadOnlyCollection<PlatformTableModel>>
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
        public async Task<IReadOnlyCollection<PlatformTableModel>> Handle(Query request, CancellationToken cancellationToken)
        {
            _logger.LogDebug("Получен запрос на получение полного перечня табличного описания платформ.");

            var result = await _context.Platforms.AsNoTracking()
                .OrderBy(e => e.Title)
                .Select(e => e.ToTableModel())
                .ToListAsync(cancellationToken);

            _logger.LogDebug("Запрос на получение полного перечня табличного описания платформ успешно выполнен, '{Count}' записей.", result.Count);
            return result;
        }
    }
}