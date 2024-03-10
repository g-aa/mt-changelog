using Mt.Utilities;

namespace Mt.ChangeLog.Entities.Views;

/// <summary>
/// Представление автор вклад в проект.
/// </summary>
public class AuthorProjectContributionView
{
    /// <summary>
    /// Инициализация экземпляра класса <see cref="AuthorProjectContributionView"/>.
    /// </summary>
    public AuthorProjectContributionView()
    {
        Author = $"{DefaultString.LastName} {DefaultString.FirstName}";
        Contribution = 0;
        ProjectPrefix = DefaultString.Prefix;
        ProjectTitle = DefaultString.Project;
        ProjectVersion = DefaultString.Revision;
    }

    /// <summary>
    /// Ф.И.О. автора.
    /// </summary>
    /// <remarks>Author.LastName Author.FirstName.</remarks>
    public string Author { get; set; }

    /// <summary>
    /// Общий вклад в проекты.
    /// </summary>
    public int Contribution { get; set; }

    /// <summary>
    /// Префикс наименования проекта.
    /// </summary>
    public string ProjectPrefix { get; set; }

    /// <summary>
    /// Заголовок проекта.
    /// </summary>
    public string ProjectTitle { get; set; }

    /// <summary>
    /// Версия проекта.
    /// </summary>
    public string ProjectVersion { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return $"{Author}, {ProjectPrefix}-{ProjectTitle}-{ProjectVersion}, вклад: {Contribution}";
    }
}