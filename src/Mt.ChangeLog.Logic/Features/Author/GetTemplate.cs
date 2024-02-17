using MediatR;
using Microsoft.Extensions.Logging;
using Mt.ChangeLog.TransferObjects.Author;

namespace Mt.ChangeLog.Logic.Features.Author;

/// <summary>
/// Запрос на получение шаблона <see cref="AuthorModel"/>.
/// </summary>
public static class GetTemplate
{
    /// <inheritdoc />
    public sealed class Query : IRequest<AuthorModel>
    {
    }

    /// <inheritdoc />
    public sealed class Handler : IRequestHandler<Query, AuthorModel>
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
        public Task<AuthorModel> Handle(Query request, CancellationToken cancellationToken)
        {
            _logger.LogDebug("Получен запрос на создание шаблона автора.");

            var model = new AuthorModel();

            _logger.LogDebug("Запрос на создание шаблона автор '{Model}' выполнен успешно.", model);
            return Task.FromResult(model);
        }
    }
}