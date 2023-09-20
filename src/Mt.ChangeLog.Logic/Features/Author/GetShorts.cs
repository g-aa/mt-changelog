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
    public sealed class Query : IRequest<IEnumerable<AuthorShortModel>>
    {
    }

    /// <inheritdoc />
    public sealed class Handler : IRequestHandler<Query, IEnumerable<AuthorShortModel>>
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
        public async Task<IEnumerable<AuthorShortModel>> Handle(Query request, CancellationToken cancellationToken)
        {
            this.logger.LogDebug("Получен запрос на получение полного перечня краткого описания авторов.");

            var result = await this.repository.GetShortEntitiesAsync();

            this.logger.LogDebug("Запрос на получение полного перечня краткого описания авторов успешно выполнен, '{Count}' записей.", result.Count());
            return result.OrderBy(e => e.LastName).ThenBy(e => e.FirstName);
        }
    }
}