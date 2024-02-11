using System.ComponentModel.DataAnnotations;

using Mt.Utilities;

namespace Mt.ChangeLog.TransferObjects.ProjectRevision;

/// <summary>
/// Последняя редакции проекта.
/// </summary>
public class LastProjectRevisionModel
{
    /// <summary>
    /// Инициализация экземпляра <see cref="LastProjectRevisionModel"/>.
    /// </summary>
    public LastProjectRevisionModel()
    {
        Prefix = DefaultString.Prefix;
        Title = DefaultString.Project;
        Version = DefaultString.Revision;
        Platform = DefaultString.Platform;
        ArmEdit = DefaultString.Version;
        Revision = DefaultString.Revision;
        Date = DateTime.UtcNow;
    }

    /// <summary>
    /// Префикс.
    /// </summary>
    /// <example>БФПО-000</example>
    [Required]
    [RegularExpression(StringFormat.Prefix)]
    public string Prefix { get; set; }

    /// <summary>
    /// наименование.
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
    [RegularExpression("^[0-9]{2}$")]
    public string Version { get; set; }

    /// <summary>
    /// Редакция.
    /// </summary>
    /// <example>00</example>
    [Required]
    [RegularExpression("^[0-9]{2}$")]
    public string Revision { get; set; }

    /// <summary>
    /// Платформа.
    /// </summary>
    /// <example>БМРЗ-000</example>
    [Required]
    [RegularExpression(StringFormat.Platform)]
    public string Platform { get; set; }

    /// <summary>
    /// Версия ArmEdit.
    /// </summary>
    /// <example>v0.00.00.00</example>
    [Required]
    [RegularExpression(StringFormat.Version)]
    public string ArmEdit { get; set; }

    /// <summary>
    /// Дата компиляции.
    /// </summary>
    [Required]
    public DateTime Date { get; set; }
}