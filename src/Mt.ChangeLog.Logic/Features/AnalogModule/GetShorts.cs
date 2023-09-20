using MediatR;
using Microsoft.Extensions.Logging;
using Mt.ChangeLog.DataAccess.Abstraction;
using Mt.ChangeLog.TransferObjects.AnalogModule;

namespace Mt.ChangeLog.Logic.Features.AnalogModule;

/// <summary>
/// Запрос на получение перечня кратких моделей данных <see cref="AnalogModuleShortModel"/>.
/// </summary>
public static class GetShorts
{
    /// <inheritdoc />
    public sealed class Query : IRequest<IEnumerable<AnalogModuleShortModel>>
    {
    }

    /// <inheritdoc />
    public sealed class Handler : IRequestHandler<Query, IEnumerable<AnalogModuleShortModel>>
    {
        private readonly ILogger<Handler> logger;

        private readonly IAnalogModuleRepository repository;

        /// <summary>
        /// Инициализация нового экземпляра класса <see cref="Handler"/>.
        /// </summary>
        /// <param name="logger">Журнал логирования.</param>
        /// <param name="repository">Репозиторий с данными.</param>
        public Handler(ILogger<Handler> logger, IAnalogModuleRepository repository)
        {
            this.logger = logger;
            this.repository = repository;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<AnalogModuleShortModel>> Handle(Query request, CancellationToken cancellationToken)
        {
            this.logger.LogDebug("Получен запрос на получение полного перечня краткого описания аналоговых модулей.");

            var result = await this.repository.GetShortEntitiesAsync();

            this.logger.LogDebug("Запрос на получение полного перечня краткого описания аналоговых модулей успешно выполнен, '{Count}' записей.", result.Count());
            return result.OrderBy(e => e.Title);
        }
    }
}