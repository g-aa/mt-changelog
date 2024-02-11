using MediatR;
using Microsoft.Extensions.Logging;
using Mt.ChangeLog.DataAccess.Abstraction;
using Mt.ChangeLog.TransferObjects.Author;

namespace Mt.ChangeLog.Logic.Features.Author;

/// <summary>
/// Запрос на получение перечня кратких моделей данных <see cref="AuthorShortModel"/>.
/// </summary>
public static class GetShorts
{
    /// <inheritdoc />
    public sealed class Query : IRequest<IReadOnlyCollection<AuthorShortModel>>
    {
    }

    /// <inheritdoc />
    public sealed class Handler : IRequestHandler<Query, IReadOnlyCollection<AuthorShortModel>>
    {
        private readonly ILogger<Handler> _logger;

        private readonly IAuthorRepository _repository;

        /// <summary>
        /// Инициализация нового экземпляра класса <see cref="Handler"/>.
        /// </summary>
        /// <param name="logger">Журнал логирования.</param>
        /// <param name="repository">Репозиторий с данными.</param>
        public Handler(ILogger<Handler> logger, IAuthorRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        /// <inheritdoc />
        public async Task<IReadOnlyCollection<AuthorShortModel>> Handle(Query request, CancellationToken cancellationToken)
        {
            _logger.LogDebug("Получен запрос на получение полного перечня краткого описания авторов.");

            var result = await _repository.GetShortEntitiesAsync();

            _logger.LogDebug("Запрос на получение полного перечня краткого описания авторов успешно выполнен, '{Count}' записей.", result.Count());
            return result.OrderBy(e => e.LastName).ThenBy(e => e.FirstName).ToList();
        }
    }
}