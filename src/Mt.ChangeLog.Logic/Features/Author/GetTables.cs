using MediatR;
using Microsoft.Extensions.Logging;
using Mt.ChangeLog.DataAccess.Abstraction;
using Mt.ChangeLog.TransferObjects.Author;

namespace Mt.ChangeLog.Logic.Features.Author;

/// <summary>
/// Запрос на получение перечня моделей данных для таблиц <see cref="AuthorTableModel"/>.
/// </summary>
public static class GetTables
{
    /// <inheritdoc />
    public sealed class Query : IRequest<IEnumerable<AuthorTableModel>>
    {
    }

    /// <inheritdoc />
    public sealed class Handler : IRequestHandler<Query, IEnumerable<AuthorTableModel>>
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
        public async Task<IEnumerable<AuthorTableModel>> Handle(Query request, CancellationToken cancellationToken)
        {
            this.logger.LogDebug("Получен запрос на получение полного перечня табличного описания авторов.");

            var result = await this.repository.GetTableEntitiesAsync();

            this.logger.LogDebug("Запрос на получение полного перечня табличного описания авторов успешно выполнен, '{Count}' записей.", result.Count());
            return result.OrderBy(e => e.LastName).ThenBy(e => e.FirstName);
        }
    }
}