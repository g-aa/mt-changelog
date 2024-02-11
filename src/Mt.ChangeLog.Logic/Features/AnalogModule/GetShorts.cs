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
    public sealed class Query : IRequest<IReadOnlyCollection<AnalogModuleShortModel>>
    {
    }

    /// <inheritdoc />
    public sealed class Handler : IRequestHandler<Query, IReadOnlyCollection<AnalogModuleShortModel>>
    {
        private readonly ILogger<Handler> _logger;

        private readonly IAnalogModuleRepository _repository;

        /// <summary>
        /// Инициализация нового экземпляра класса <see cref="Handler"/>.
        /// </summary>
        /// <param name="logger">Журнал логирования.</param>
        /// <param name="repository">Репозиторий с данными.</param>
        public Handler(ILogger<Handler> logger, IAnalogModuleRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        /// <inheritdoc />
        public async Task<IReadOnlyCollection<AnalogModuleShortModel>> Handle(Query request, CancellationToken cancellationToken)
        {
            _logger.LogDebug("Получен запрос на получение полного перечня краткого описания аналоговых модулей.");

            var result = await _repository.GetShortEntitiesAsync();

            _logger.LogDebug("Запрос на получение полного перечня краткого описания аналоговых модулей успешно выполнен, '{Count}' записей.", result.Count());
            return result.OrderBy(e => e.Title).ToList();
        }
    }
}