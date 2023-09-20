using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Mt.ChangeLog.DataContext;
using Mt.ChangeLog.Logic.Mappers;
using Mt.ChangeLog.TransferObjects.Platform;

namespace Mt.ChangeLog.Logic.Features.Platform;

/// <summary>
/// Запрос на получение перечня кратких моделей данных <see cref="PlatformShortModel"/>.
/// </summary>
public static class GetShorts
{
    /// <inheritdoc />
    public sealed class Query : IRequest<IEnumerable<PlatformShortModel>>
    {
    }

    /// <inheritdoc />
    public sealed class Handler : IRequestHandler<Query, IEnumerable<PlatformShortModel>>
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
        public async Task<IEnumerable<PlatformShortModel>> Handle(Query request, CancellationToken cancellationToken)
        {
            this.logger.LogDebug("Получен запрос на получение полного перечня краткого описания платформы.");

            var result = await this.context.Platforms.AsNoTracking()
                .OrderBy(e => e.Title)
                .Select(e => e.ToShortModel())
                .ToListAsync(cancellationToken);

            this.logger.LogDebug("Запрос на получение полного перечня краткого описания платформ успешно выполнен, '{Count}' записей.", result.Count);
            return result;
        }
    }
}