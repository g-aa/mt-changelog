using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Mt.ChangeLog.DataContext;
using Mt.ChangeLog.Logic.Mappers;
using Mt.ChangeLog.TransferObjects.ProjectRevision;

namespace Mt.ChangeLog.Logic.Features.ProjectRevision;

/// <summary>
/// Запрос на получение перечня моделей данных для таблиц <see cref="ProjectRevisionTableModel"/>.
/// </summary>
public static class GetTables
{
    /// <inheritdoc />
    public sealed class Query : IRequest<IReadOnlyCollection<ProjectRevisionTableModel>>
    {
    }

    /// <inheritdoc />
    public sealed class Handler : IRequestHandler<Query, IReadOnlyCollection<ProjectRevisionTableModel>>
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
        public async Task<IReadOnlyCollection<ProjectRevisionTableModel>> Handle(Query request, CancellationToken cancellationToken)
        {
            _logger.LogDebug("Получен запрос на получение полного перечня табличного описания редакций проектов.");

            var result = await _context.ProjectRevisions.AsNoTracking()
                .Include(pr => pr.ArmEdit)
                .Include(pr => pr.ProjectVersion!.AnalogModule)
                .OrderByDescending(pr => pr.Date).ThenByDescending(pr => pr.ArmEdit!.Version)
                .Select(pr => pr.ToTableModel())
                .ToListAsync(cancellationToken);

            _logger.LogDebug("Запрос на получение полного перечня табличного описания редакций проекта успешно выполнен, '{Count}' записей.", result.Count);
            return result;
        }
    }
}