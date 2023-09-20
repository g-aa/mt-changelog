using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Mt.ChangeLog.DataContext;
using Mt.ChangeLog.Logic.Mappers;
using Mt.ChangeLog.TransferObjects.Communication;

namespace Mt.ChangeLog.Logic.Features.Communication;

/// <summary>
/// Запрос на получение перечня моделей данных для таблиц <see cref="CommunicationTableModel"/>.
/// </summary>
public static class GetTables
{
    /// <inheritdoc />
    public sealed class Query : IRequest<IEnumerable<CommunicationTableModel>>
    {
    }

    /// <inheritdoc />
    public sealed class Handler : IRequestHandler<Query, IEnumerable<CommunicationTableModel>>
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
        public async Task<IEnumerable<CommunicationTableModel>> Handle(Query request, CancellationToken cancellationToken)
        {
            this.logger.LogDebug("Получен запрос на получение полного перечня табличного описания коммуникационных модулей.");

            var result = await this.context.Communications.AsNoTracking()
                .Include(e => e.Protocols)
                .OrderBy(e => e.Title)
                .Select(e => e.ToTableModel())
                .ToListAsync(cancellationToken);

            this.logger.LogDebug("Запрос на получение полного перечня табличного описания коммуникационных модулей успешно выполнен, '{Count}' записей.", result.Count);
            return result;
        }
    }
}