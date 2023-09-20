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
        private readonly ILogger<Handler> logger;

        /// <summary>
        /// Инициализация нового экземпляра класса <see cref="Handler"/>.
        /// </summary>
        /// <param name="logger">Журнал логирования.</param>
        public Handler(ILogger<Handler> logger)
        {
            this.logger = logger;
        }

        /// <inheritdoc />
        public Task<ArmEditModel> Handle(Query request, CancellationToken cancellationToken)
        {
            this.logger.LogDebug("Получен запрос на создание шаблона ArmEdit.");

            var model = new ArmEditModel();

            this.logger.LogDebug("Запрос на создание шаблона ArmEdit '{DIVG}' '{Version}' выполнен успешно.", model.DIVG, model.Version);
            return Task.FromResult(model);
        }
    }
}