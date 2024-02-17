using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Mt.ChangeLog.DataContext;
using Mt.ChangeLog.Logic.Mappers;
using Mt.ChangeLog.TransferObjects.RelayAlgorithm;

namespace Mt.ChangeLog.Logic.Features.RelayAlgorithm;

/// <summary>
/// Запрос на получение перечня кратких моделей данных <see cref="RelayAlgorithmShortModel"/>.
/// </summary>
public static class GetShorts
{
    /// <inheritdoc />
    public sealed class Query : IRequest<IReadOnlyCollection<RelayAlgorithmShortModel>>
    {
    }

    /// <inheritdoc />
    public sealed class Handler : IRequestHandler<Query, IReadOnlyCollection<RelayAlgorithmShortModel>>
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
        public async Task<IReadOnlyCollection<RelayAlgorithmShortModel>> Handle(Query request, CancellationToken cancellationToken)
        {
            _logger.LogDebug("Получен запрос на получение полного перечня краткого описания алгоритмов РЗиА.");

            var result = await _context.RelayAlgorithms.AsNoTracking()
                .OrderBy(e => e.Group)
                .ThenBy(e => e.Title)
                .Select(e => e.ToShortModel())
                .ToListAsync(cancellationToken);

            _logger.LogDebug("Запрос на получение полного перечня краткого описания алгоритмов РЗиА успешно выполнен, '{Count}' записей.", result.Count);
            return result;
        }
    }
}