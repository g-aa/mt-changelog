using MediatR;
using Microsoft.Extensions.Logging;
using Mt.ChangeLog.TransferObjects.RelayAlgorithm;

namespace Mt.ChangeLog.Logic.Features.RelayAlgorithm;

/// <summary>
/// Запрос на получение шаблона модели <see cref="RelayAlgorithmModel"/>.
/// </summary>
public static class GetTemplate
{
    /// <inheritdoc />
    public sealed class Query : IRequest<RelayAlgorithmModel>
    {
    }

    /// <inheritdoc />
    public sealed class Handler : IRequestHandler<Query, RelayAlgorithmModel>
    {
        private readonly ILogger<Handler> _logger;

        /// <summary>
        /// Инициализация нового экземпляра класса <see cref="Handler"/>.
        /// </summary>
        /// <param name="logger">Журнал логирования.</param>
        public Handler(ILogger<Handler> logger)
        {
            _logger = logger;
        }

        /// <inheritdoc />
        public Task<RelayAlgorithmModel> Handle(Query request, CancellationToken cancellationToken)
        {
            _logger.LogDebug("Получен запрос на создание шаблона протокола.");
            var result = new RelayAlgorithmModel();
            _logger.LogDebug("Запрос на создание шаблона протокола '{Result}' выполнен успешно.", result);
            return Task.FromResult(result);
        }
    }
}