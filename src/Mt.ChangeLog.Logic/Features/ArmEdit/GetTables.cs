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
    public sealed class Query : IRequest<IReadOnlyCollection<ArmEditTableModel>>
    {
    }

    /// <inheritdoc />
    public sealed class Handler : IRequestHandler<Query, IReadOnlyCollection<ArmEditTableModel>>
    {
        private readonly ILogger<Handler> _logger;

        private readonly IArmEditRepository _repository;

        /// <summary>
        /// Инициализация нового экземпляра класса <see cref="Handler"/>.
        /// </summary>
        /// <param name="logger">Журнал логирования.</param>
        /// <param name="repository">Репозиторий с данными.</param>
        public Handler(ILogger<Handler> logger, IArmEditRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        /// <inheritdoc />
        public async Task<IReadOnlyCollection<ArmEditTableModel>> Handle(Query request, CancellationToken cancellationToken)
        {
            _logger.LogDebug("Получен запрос на получение полного перечня табличного описания ArmEdit.");

            var result = await _repository.GetTableEntitiesAsync();

            _logger.LogDebug("Запрос на получение полного перечня табличного описания ArmEdit успешно выполнен, '{Count}' записей.", result.Count());
            return result.OrderByDescending(e => e.Version).ToList();
        }
    }
}