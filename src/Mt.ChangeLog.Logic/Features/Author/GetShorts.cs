using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Mt.ChangeLog.DataContext;
using Mt.ChangeLog.Logic.Mappers;
using Mt.ChangeLog.TransferObjects.Author;

namespace Mt.ChangeLog.Logic.Features.Author;

/// <summary>
/// Запрос на получение перечня кратких моделей данных <see cref="AuthorShortModel"/>.
/// </summary>
public static class GetShorts
{
    /// <inheritdoc />
    public sealed class Query : IRequest<IReadOnlyCollection<AuthorShortModel>>
    {
    }

    /// <inheritdoc />
    public sealed class Handler : IRequestHandler<Query, IReadOnlyCollection<AuthorShortModel>>
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
        public async Task<IReadOnlyCollection<AuthorShortModel>> Handle(Query request, CancellationToken cancellationToken)
        {
            _logger.LogDebug("Получен запрос на получение полного перечня краткого описания авторов.");

            var result = await _context.Authors.AsNoTracking()
                .OrderBy(e => e.LastName)
                .ThenBy(e => e.FirstName)
                .Select(e => e.ToShortModel())
                .ToListAsync(cancellationToken);

            _logger.LogDebug("Запрос на получение полного перечня краткого описания авторов успешно выполнен, '{Count}' записей.", result.Count);
            return result;
        }
    }
}