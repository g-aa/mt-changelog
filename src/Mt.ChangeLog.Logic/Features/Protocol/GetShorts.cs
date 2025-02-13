using MediatR;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using Mt.ChangeLog.DataContext;
using Mt.ChangeLog.Logic.Mappers;
using Mt.ChangeLog.TransferObjects.Protocol;

namespace Mt.ChangeLog.Logic.Features.Protocol;

/// <summary>
/// Запрос на получение перечня кратких моделей данных <see cref="ProtocolShortModel"/>.
/// </summary>
public static class GetShorts
{
    /// <inheritdoc />
    public sealed class Query : IRequest<IReadOnlyCollection<ProtocolShortModel>>
    {
    }

    /// <inheritdoc />
    public sealed class Handler : IRequestHandler<Query, IReadOnlyCollection<ProtocolShortModel>>
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
        public async Task<IReadOnlyCollection<ProtocolShortModel>> Handle(Query request, CancellationToken cancellationToken)
        {
            _logger.LogDebug("Получен запрос на получение полного перечня краткого описания протоколов.");

            var result = await _context.Protocols.AsNoTracking()
                .OrderBy(p => p.Title)
                .Select(p => p.ToShortModel())
                .ToListAsync(cancellationToken);

            _logger.LogDebug("Запрос на получение полного перечня краткого описания протоколов успешно выполнен, '{Count}' записей.", result.Count);
            return result;
        }
    }
}