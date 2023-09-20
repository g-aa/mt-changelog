using MediatR;
using Microsoft.Extensions.Logging;
using Mt.ChangeLog.DataAccess.Abstraction;
using Mt.ChangeLog.TransferObjects.AnalogModule;

namespace Mt.ChangeLog.Logic.Features.AnalogModule;

/// <summary>
/// Запрос на получение перечня моделей данных для таблиц <see cref="AnalogModuleTableModel"/>.
/// </summary>
public static class GetTables
{
    /// <inheritdoc />
    public sealed class Query : IRequest<IEnumerable<AnalogModuleTableModel>>
    {
    }

    /// <inheritdoc />
    public sealed class Handler : IRequestHandler<Query, IEnumerable<AnalogModuleTableModel>>
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
        public async Task<IEnumerable<AnalogModuleTableModel>> Handle(Query request, CancellationToken cancellationToken)
        {
            this.logger.LogDebug("Получен запрос на получение полного перечня табличного описания аналоговых модулей.");

            var result = await this.repository.GetTableEntitiesAsync();

            this.logger.LogDebug("Запрос на получение полного перечня табличного описания аналоговых модулей успешно выполнен, '{Count}' записей.", result.Count());
            return result.OrderBy(e => e.Title);
        }
    }
}