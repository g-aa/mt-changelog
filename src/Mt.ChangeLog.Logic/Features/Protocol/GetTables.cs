using MediatR;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using Mt.ChangeLog.DataContext;
using Mt.ChangeLog.Logic.Mappers;
using Mt.ChangeLog.TransferObjects.Protocol;

namespace Mt.ChangeLog.Logic.Features.Protocol;

/// <summary>
/// Запрос на получение перечня моделей данных для таблиц <see cref="ProtocolTableModel"/>.
/// </summary>
public static class GetTables
{
    /// <inheritdoc />
    public sealed class Query : IRequest<IReadOnlyCollection<ProtocolTableModel>>
    {
    }

    /// <inheritdoc />
    public sealed class Handler : IRequestHandler<Query, IReadOnlyCollection<ProtocolTableModel>>
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
        public async Task<IReadOnlyCollection<ProtocolTableModel>> Handle(Query request, CancellationToken cancellationToken)
        {
            _logger.LogDebug("Получен запрос на получение полного перечня табличного описания протокола.");

            var result = await _context.Protocols.AsNoTracking()
                .OrderBy(p => p.Title)
                .Select(p => p.ToTableModel())
                .ToListAsync(cancellationToken);

            _logger.LogDebug("Запрос на получение полного перечня табличного описания протокола успешно выполнен, '{Count}' записей.", result.Count);
            return result;
        }
    }
}