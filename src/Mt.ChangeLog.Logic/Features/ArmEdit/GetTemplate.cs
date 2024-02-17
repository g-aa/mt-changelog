using MediatR;
using Microsoft.Extensions.Logging;
using Mt.ChangeLog.TransferObjects.ArmEdit;

namespace Mt.ChangeLog.Logic.Features.ArmEdit;

/// <summary>
/// Запрос на получение шаблона <see cref="ArmEditModel"/>.
/// </summary>
public static class GetTemplate
{
    /// <inheritdoc />
    public sealed class Query : IRequest<ArmEditModel>
    {
    }

    /// <inheritdoc />
    public sealed class Handler : IRequestHandler<Query, ArmEditModel>
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
        public Task<ArmEditModel> Handle(Query request, CancellationToken cancellationToken)
        {
            _logger.LogDebug("Получен запрос на создание шаблона ArmEdit.");

            var model = new ArmEditModel();

            _logger.LogDebug("Запрос на создание шаблона ArmEdit '{Model}' выполнен успешно.", model);
            return Task.FromResult(model);
        }
    }
}