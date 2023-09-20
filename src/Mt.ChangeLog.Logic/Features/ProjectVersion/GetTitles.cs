using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Mt.ChangeLog.DataContext;

namespace Mt.ChangeLog.Logic.Features.ProjectVersion;

/// <summary>
/// Запрос на получение перечня наименование версий проектов.
/// </summary>
public static class GetTitles
{
    /// <inheritdoc />
    public sealed class Query : IRequest<IEnumerable<string>>
    {
    }

    /// <inheritdoc />
    public sealed class Handler : IRequestHandler<Query, IEnumerable<string>>
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
        public async Task<IEnumerable<string>> Handle(Query request, CancellationToken cancellationToken)
        {
            this.logger.LogDebug("Получен запрос на получение перечня наименование версий проектов.");

            var result = await this.context.ProjectVersions.AsNoTracking()
                .Select(e => e.Title)
                .Distinct()
                .OrderBy(e => e)
                .ToListAsync(cancellationToken);

            this.logger.LogDebug("Запрос нна получение перечня наименование версий проектов выполнен успешно, '{Count}' записей.", result.Count);

            return result;
        }
    }
}