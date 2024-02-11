using MediatR;
using Microsoft.Extensions.Logging;
using Mt.ChangeLog.DataAccess.Abstraction;
using Mt.ChangeLog.TransferObjects.Author;

namespace Mt.ChangeLog.Logic.Features.Author;

/// <summary>
/// Получить вклад авторов по отдельным проектам.
/// </summary>
public static class GetProjectContributions
{
    /// <inheritdoc />
    public sealed class Query : IRequest<IReadOnlyCollection<AuthorProjectContributionModel>>
    {
    }

    /// <inheritdoc />
    public sealed class Handler : IRequestHandler<Query, IReadOnlyCollection<AuthorProjectContributionModel>>
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
        public async Task<IReadOnlyCollection<AuthorProjectContributionModel>> Handle(Query request, CancellationToken cancellationToken)
        {
            _logger.LogDebug("Получен запрос на получение перечня вкладов всех авторов по отдельным проектам.");

            var result = await _repository.GetAuthorProjectContributionsAsync();

            _logger.LogDebug("Запрос на получение перечня общего вклада всех авторов успешно выполнен, '{Count}' записей.", result.Count());
            return result.ToList();
        }
    }
}