using MediatR;
using Microsoft.Extensions.Logging;
using Mt.ChangeLog.TransferObjects.ProjectStatus;

namespace Mt.ChangeLog.Logic.Features.ProjectStatus;

/// <summary>
/// Запрос на получение шаблона <see cref="ProjectStatusModel"/>.
/// </summary>
public static class GetTemplate
{
    /// <inheritdoc />
    public sealed class Query : IRequest<ProjectStatusModel>
    {
    }

    /// <inheritdoc />
    public sealed class Handler : IRequestHandler<Query, ProjectStatusModel>
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
        public Task<ProjectStatusModel> Handle(Query request, CancellationToken cancellationToken)
        {
            _logger.LogDebug("Получен запрос на создание шаблона статуса проекта.");
            var model = new ProjectStatusModel();
            _logger.LogDebug("Запрос на создание шаблона статуса проекта '{Model}' выполнен успешно.", model);
            return Task.FromResult(model);
        }
    }
}