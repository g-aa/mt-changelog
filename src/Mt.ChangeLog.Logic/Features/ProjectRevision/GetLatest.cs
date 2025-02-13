using MediatR;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using Mt.ChangeLog.DataContext;
using Mt.ChangeLog.Logic.Mappers;
using Mt.ChangeLog.TransferObjects.ProjectRevision;

namespace Mt.ChangeLog.Logic.Features.ProjectRevision;

/// <summary>
/// Запрос на получение перечня кратких моделей данных <see cref="LastProjectRevisionModel"/>.
/// </summary>
public static class GetLatest
{
    /// <inheritdoc />
    public sealed class Query : IRequest<IReadOnlyCollection<LastProjectRevisionModel>>
    {
    }

    /// <inheritdoc />
    public sealed class Handler : IRequestHandler<Query, IReadOnlyCollection<LastProjectRevisionModel>>
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
        public async Task<IReadOnlyCollection<LastProjectRevisionModel>> Handle(Query request, CancellationToken cancellationToken)
        {
            _logger.LogDebug("Получен запрос на предоставление данных о последних редакциях проектов.");

            var result = await _context.LastProjectRevisions
                .Select(e => e.ToModel())
                .ToArrayAsync(cancellationToken);

            _logger.LogDebug("Запрос на получение данных о последних редакциях проектов выполнен успешно, '{Count}' записей.", result.Length);
            return result;
        }
    }
}