using MediatR;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using Mt.ChangeLog.DataContext;
using Mt.ChangeLog.Logic.Mappers;
using Mt.ChangeLog.TransferObjects.Author;

namespace Mt.ChangeLog.Logic.Features.Author;

/// <summary>
/// Получить вклад авторов во все проекты.
/// </summary>
public static class GetContributions
{
    /// <inheritdoc />
    public sealed class Query : IRequest<IReadOnlyCollection<AuthorContributionModel>>
    {
    }

    /// <inheritdoc />
    public sealed class Handler : IRequestHandler<Query, IReadOnlyCollection<AuthorContributionModel>>
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
        public async Task<IReadOnlyCollection<AuthorContributionModel>> Handle(Query request, CancellationToken cancellationToken)
        {
            _logger.LogDebug("Получен запрос на получение перечня общего вклада всех авторов.");

            var result = await _context.AuthorContributions
                .Select(e => e.ToModel())
                .ToListAsync(cancellationToken);

            _logger.LogDebug("Запрос на получение перечня общего вклада всех авторов успешно выполнен, '{Count}' записей.", result.Count);
            return result;
        }
    }
}