using MediatR;
using Microsoft.Extensions.Logging;
using Mt.ChangeLog.DataAccess.Abstraction;
using Mt.ChangeLog.TransferObjects.ArmEdit;

namespace Mt.ChangeLog.Logic.Features.ArmEdit;

/// <summary>
/// Запрос на получение перечня кратких моделей данных <see cref="ArmEditShortModel"/>.
/// </summary>
public static class GetShorts
{
    /// <inheritdoc />
    public sealed class Query : IRequest<IEnumerable<ArmEditShortModel>>
    {
    }

    /// <inheritdoc />
    public sealed class Handler : IRequestHandler<Query, IEnumerable<ArmEditShortModel>>
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
        public async Task<IEnumerable<ArmEditShortModel>> Handle(Query request, CancellationToken cancellationToken)
        {
            this.logger.LogDebug("Получен запрос на получение полного перечня краткого описания ArmEdits.");

            var result = await this.repository.GetShortEntitiesAsync();

            this.logger.LogDebug("Запрос на получение полного перечня краткого описания ArmEdits успешно выполнен, '{Count}' записей.", result.Count());
            return result.OrderByDescending(e => e.Version);
        }
    }
}