using Mt.ChangeLog.TransferObjects.Author;

namespace Mt.ChangeLog.DataAccess.Abstraction;

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
    Task<IReadOnlyCollection<AuthorContributionModel>> GetAuthorContributionsAsync();

    /// <summary>
    /// Получить вклад авторов по отдельным проектам.
    /// </summary>
    /// <returns>Перечень авторов и их вклад по проектам.</returns>
    Task<IReadOnlyCollection<AuthorProjectContributionModel>> GetAuthorProjectContributionsAsync();
}