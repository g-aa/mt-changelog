using System.ComponentModel.DataAnnotations;

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
        ProjectPrefix = DefaultString.Prefix;
        ProjectTitle = DefaultString.Project;
        ProjectVersion = DefaultString.Revision;
    }

    /// <summary>
    /// Префикс наименования проекта.
    /// </summary>
    /// <example>БФПО-000</example>
    [Required]
    [StringLength(8, MinimumLength = 4)]
    [RegularExpression(StringFormat.Prefix)]
    public string ProjectPrefix { get; set; }

    /// <summary>
    /// Заголовок проекта.
    /// </summary>
    /// <example>ПМК</example>
    [Required]
    [StringLength(16, MinimumLength = 2)]
    public string ProjectTitle { get; set; }

    /// <summary>
    /// Версия проекта.
    /// </summary>
    /// <example>00</example>
    [Required]
    [StringLength(2, MinimumLength = 2)]
    [RegularExpression("^[0-9]{2}$")]
    public string ProjectVersion { get; set; }
}