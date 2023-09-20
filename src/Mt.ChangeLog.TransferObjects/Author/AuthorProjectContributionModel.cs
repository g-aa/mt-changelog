using Mt.Utilities;

namespace Mt.ChangeLog.TransferObjects.Author;

/// <summary>
/// Модель автор вклад в проекты.
/// </summary>
public class AuthorProjectContributionModel : AuthorContributionModel
{
    /// <summary>
    /// Инициализация экземпляра <see cref="AuthorProjectContributionModel"/>.
    /// </summary>
    public AuthorProjectContributionModel()
    {
        this.ProjectPrefix = DefaultString.Prefix;
        this.ProjectTitle = DefaultString.Project;
        this.ProjectVersion = DefaultString.Revision;
    }

    /// <summary>
    /// Префикс наименования проекта.
    /// </summary>
    /// <example>БФПО-000</example>
    public string ProjectPrefix { get; set; }

    /// <summary>
    /// Заголовок проекта.
    /// </summary>
    /// <example>ПМК</example>
    public string ProjectTitle { get; set; }

    /// <summary>
    /// Версия проекта.
    /// </summary>
    /// <example>00</example>
    public string ProjectVersion { get; set; }
}