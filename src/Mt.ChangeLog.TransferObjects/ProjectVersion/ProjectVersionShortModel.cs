using System.ComponentModel.DataAnnotations;

using Mt.Utilities;

namespace Mt.ChangeLog.TransferObjects.ProjectVersion;

/// <summary>
/// Краткая модель версии проекта.
/// </summary>
public class ProjectVersionShortModel
{
    /// <summary>
    /// Инициализация экземпляра <see cref="ProjectVersionShortModel"/>.
    /// </summary>
    public ProjectVersionShortModel()
    {
        Id = Guid.NewGuid();
        Prefix = DefaultString.Prefix;
        Title = DefaultString.Project;
        Version = DefaultString.Revision;
    }

    /// <summary>
    /// ИД.
    /// </summary>
    /// <example>00000000-0000-0000-0000-000000000000</example>
    [Required]
    public Guid Id { get; set; }

    /// <summary>
    /// Префикс.
    /// </summary>
    /// <example>БФПО-000</example>
    [Required]
    [StringLength(8, MinimumLength = 4)]
    [RegularExpression(StringFormat.Prefix)]
    public string Prefix { get; set; }

    /// <summary>
    /// Наименование.
    /// </summary>
    /// <example>ПМК</example>
    [Required]
    [StringLength(16, MinimumLength = 2)]
    public string Title { get; set; }

    /// <summary>
    /// Версия.
    /// </summary>
    /// <example>00</example>
    [Required]
    [StringLength(2, MinimumLength = 2)]
    [RegularExpression("^[0-9]{2}$")]
    public string Version { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return $"{Prefix}-{Title}-{Version}";
    }
}