using MediatR;
using Microsoft.Extensions.Logging;
using Mt.ChangeLog.DataAccess.Abstraction;
using Mt.ChangeLog.TransferObjects.ArmEdit;

namespace Mt.ChangeLog.Logic.Features.ArmEdit;

/// <summary>
/// Получить актуальную версию ArmEdit.
/// </summary>
public static class GetActual
{
    /// <inheritdoc />
    public sealed class Query : IRequest<ArmEditModel>
    {
    }

    /// <inheritdoc />
    public sealed class Handler : IRequestHandler<Query, ArmEditModel>
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
        public async Task<ArmEditModel> Handle(Query request, CancellationToken cancellationToken)
        {
            _logger.LogDebug("Получен запрос на предоставление данных об актуальном ArmEdit.");

            var result = await _repository.GetActualAsync();

            _logger.LogDebug("Запрос на получение данных об актуальном ArmEdit '{Result}' выполнен успешно.", result);
            return result;
        }
    }
}