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
    public sealed class Query : IRequest<IEnumerable<AuthorProjectContributionModel>>
    {
    }

    /// <inheritdoc />
    public sealed class Handler : IRequestHandler<Query, IEnumerable<AuthorProjectContributionModel>>
    {
        private readonly ILogger<Handler> logger;

        private readonly IAuthorRepository repository;

        /// <summary>
        /// Инициализация нового экземпляра класса <see cref="Handler"/>.
        /// </summary>
        /// <param name="logger">Журнал логирования.</param>
        /// <param name="repository">Репозиторий с данными.</param>
        public Handler(ILogger<Handler> logger, IAuthorRepository repository)
        {
            this.logger = logger;
            this.repository = repository;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<AuthorProjectContributionModel>> Handle(Query request, CancellationToken cancellationToken)
        {
            this.logger.LogDebug("Получен запрос на получение перечня вкладов всех авторов по отдельным проектам.");

            var result = await this.repository.GetAuthorProjectContributionsAsync();

            this.logger.LogDebug("Запрос на получение перечня общего вклада всех авторов успешно выполнен, '{Count}' записей.", result.Count());
            return result;
        }
    }
}