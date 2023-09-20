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
    public sealed class Query : IRequest<IEnumerable<ProjectRevisionTableModel>>
    {
    }

    /// <inheritdoc />
    public sealed class Handler : IRequestHandler<Query, IEnumerable<ProjectRevisionTableModel>>
    {
        private readonly ILogger<Handler> logger;

        private readonly MtContext context;

        /// <summary>
        /// Инициализация нового экземпляра класса <see cref="Handler"/>.
        /// </summary>
        /// <param name="logger">Журнал логирования.</param>
        /// <param name="context">Контекст данных.</param>
        public Handler(ILogger<Handler> logger, MtContext context)
        {
            this.logger = logger;
            this.context = context;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<ProjectRevisionTableModel>> Handle(Query request, CancellationToken cancellationToken)
        {
            this.logger.LogDebug("Получен запрос на получение полного перечня табличного описания редакций проектов.");

            var result = await this.context.ProjectRevisions.AsNoTracking()
                .Include(pr => pr.ArmEdit)
                .Include(pr => pr.ProjectVersion!.AnalogModule)
                .OrderByDescending(pr => pr.Date).ThenByDescending(pr => pr.ArmEdit!.Version)
                .Select(pr => pr.ToTableModel())
                .ToListAsync(cancellationToken);

            this.logger.LogDebug("Запрос на получение полного перечня табличного описания редакций проекта успешно выполнен, '{Count}' записей.", result.Count);
            return result;
        }
    }
}