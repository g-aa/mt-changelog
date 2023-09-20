using MediatR;
using Microsoft.Extensions.Logging;
using Mt.ChangeLog.DataAccess.Abstraction;
using Mt.ChangeLog.TransferObjects.ArmEdit;

namespace Mt.ChangeLog.Logic.Features.ArmEdit;

/// <summary>
/// Запрос на получение перечня моделей данных для таблиц <see cref="ArmEditTableModel"/>.
/// </summary>
public static class GetTables
{
    /// <inheritdoc />
    public sealed class Query : IRequest<IEnumerable<ArmEditTableModel>>
    {
    }

    /// <inheritdoc />
    public sealed class Handler : IRequestHandler<Query, IEnumerable<ArmEditTableModel>>
    {
        private readonly ILogger<Handler> logger;

        private readonly IArmEditRepository repository;

        /// <summary>
        /// Инициализация нового экземпляра класса <see cref="Handler"/>.
        /// </summary>
        /// <param name="logger">Журнал логирования.</param>
        /// <param name="repository">Репозиторий с данными.</param>
        public Handler(ILogger<Handler> logger, IArmEditRepository repository)
        {
            this.logger = logger;
            this.repository = repository;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<ArmEditTableModel>> Handle(Query request, CancellationToken cancellationToken)
        {
            this.logger.LogDebug("Получен запрос на получение полного перечня табличного описания ArmEdit.");

            var result = await this.repository.GetTableEntitiesAsync();

            this.logger.LogDebug("Запрос на получение полного перечня табличного описания ArmEdit успешно выполнен, '{Count}' записей.", result.Count());
            return result.OrderByDescending(e => e.Version);
        }
    }
}