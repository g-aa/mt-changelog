using MediatR;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using Mt.ChangeLog.DataContext;
using Mt.ChangeLog.Logic.Mappers;
using Mt.ChangeLog.TransferObjects.ArmEdit;

namespace Mt.ChangeLog.Logic.Features.ArmEdit;

/// <summary>
/// Запрос на получение перечня кратких моделей данных <see cref="ArmEditShortModel"/>.
/// </summary>
public static class GetShorts
{
    /// <inheritdoc />
    public sealed class Query : IRequest<IReadOnlyCollection<ArmEditShortModel>>
    {
    }

    /// <inheritdoc />
    public sealed class Handler : IRequestHandler<Query, IReadOnlyCollection<ArmEditShortModel>>
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
        public async Task<IReadOnlyCollection<ArmEditShortModel>> Handle(Query request, CancellationToken cancellationToken)
        {
            _logger.LogDebug("Получен запрос на получение полного перечня краткого описания ArmEdits.");

            var result = await _context.ArmEdits.AsNoTracking()
                .OrderBy(e => e.Version)
                .Select(e => e.ToShortModel())
                .ToListAsync(cancellationToken);

            _logger.LogDebug("Запрос на получение полного перечня краткого описания ArmEdits успешно выполнен, '{Count}' записей.", result.Count);
            return result;
        }
    }
}