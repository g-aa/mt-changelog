using Mt.ChangeLog.TransferObjects.Author;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mt.ChangeLog.DataAccess.Abstractions
{
    /// <summary>
    /// Репозиторий для работы с авторами проектов.
    /// </summary>
    public interface IAuthorRepository :
        IReadingRepository<AuthorModel, AuthorShortModel, AuthorTableModel>
    {
        /// <summary>
        /// Получить вклад авторов в проекты (БФПО).
        /// </summary>
        /// <returns>Перечень авторов и их вклад в проекты.</returns>
        Task<IEnumerable<AuthorContributionModel>> GetAuthorContributionsAsync();

        /// <summary>
        /// Получить вклад авторов по отдельным проектам.
        /// </summary>
        /// <returns>Перечень авторов и их вклад по проектам.</returns>
        Task<IEnumerable<AuthorProjectContributionModel>> GetAuthorProjectContributionsAsync();
    }
}