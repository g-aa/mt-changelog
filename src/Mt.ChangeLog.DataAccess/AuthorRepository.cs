using Dapper;
using Microsoft.Extensions.Logging;
using Mt.ChangeLog.DataAccess.Abstractions;
using Mt.ChangeLog.TransferObjects.Author;
using System.Data;

namespace Mt.ChangeLog.DataAccess
{
    /// <summary>
    /// Репозиторий для работы с авторами проектов.
    /// </summary>
    internal sealed class AuthorRepository : AbstractRepository, IAuthorRepository
    {
        /// <summary>
        /// Инициализация экземпляра класса <see cref="AuthorRepository"/>.
        /// </summary>
        /// <param name="logger">Журнал логирования.</param>
        /// <param name="connection">Подключение к базе данных.</param>
        public AuthorRepository(ILogger<AuthorRepository> logger, IDbConnection connection)
            : base(logger, connection)
        {
        }

        /// <inheritdoc />
        public async Task<IEnumerable<AuthorContributionModel>> GetAuthorContributionsAsync()
        {
            var qSql = @$"SELECT * FROM ""{Schema}"".""AuthorContribution"";";
            var result = await this.connection.QueryAsync<AuthorContributionModel>(qSql);
            return result;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<AuthorProjectContributionModel>> GetAuthorProjectContributionsAsync()
        {
            var qSql = @$"SELECT * FROM ""{Schema}"".""AuthorProjectContribution"";";
            var result = await this.connection.QueryAsync<AuthorProjectContributionModel>(qSql);
            return result;
        }

        /// <inheritdoc />
        public async Task<AuthorModel> GetEntityAsync(Guid guid)
        {
            var qSql = @$"SELECT * FROM ""{Schema}"".""get_Author""(@guid);";
            var result = await this.connection.QuerySingleAsync<AuthorModel>(qSql, new { guid });
            return result;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<AuthorShortModel>> GetShortEntitiesAsync()
        {
            var qSql = @$"SELECT * FROM ""{Schema}"".""get_ShortAuthors""();";
            var result = await this.connection.QueryAsync<AuthorShortModel>(qSql);
            return result;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<AuthorTableModel>> GetTableEntitiesAsync()
        {
            var qSql = @$"SELECT * FROM ""{Schema}"".""get_TableAuthors""();";
            var result = await this.connection.QueryAsync<AuthorTableModel>(qSql);
            return result;
        }
    }
}