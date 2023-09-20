using MediatR;
using Microsoft.Extensions.Logging;
using Mt.ChangeLog.DataAccess.Abstraction;
using Mt.ChangeLog.TransferObjects.Author;

namespace Mt.ChangeLog.Logic.Features.Author;

/// <summary>
/// Получить вклад авторов во все проекты.
/// </summary>
public static class GetContributions
{
    /// <inheritdoc />
    public sealed class Query : IRequest<IEnumerable<AuthorContributionModel>>
    {
    }

    /// <inheritdoc />
    public sealed class Handler : IRequestHandler<Query, IEnumerable<AuthorContributionModel>>
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
        public async Task<IEnumerable<AuthorContributionModel>> Handle(Query request, CancellationToken cancellationToken)
        {
            this.logger.LogDebug("Получен запрос на получение перечня общего вклада всех авторов.");

            var result = await this.repository.GetAuthorContributionsAsync();

            this.logger.LogDebug("Запрос на получение перечня общего вклада всех авторов успешно выполнен, '{Count}' записей.", result.Count());
            return result;
        }
    }
}