using System.ComponentModel.DataAnnotations;
using System.Globalization;

using Mt.Utilities;

namespace Mt.ChangeLog.TransferObjects.Historical;

/// <summary>
/// Модель ревизии проекта для дерева версий.
/// </summary>
public class ProjectRevisionTreeModel
{
    /// <summary>
    /// Инициализация экземпляра <see cref="ProjectRevisionTreeModel"/>.
    /// </summary>
    public ProjectRevisionTreeModel()
    {
        ParentId = Guid.Empty;
        Id = Guid.Empty;
        Prefix = DefaultString.Prefix;
        Title = DefaultString.Project;
        Version = DefaultString.Revision;
        Revision = DefaultString.Revision;
        ArmEdit = DefaultString.Version;
        Date = DateTime.UtcNow.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
        Platform = DefaultString.Platform;
    }

    /// <summary>
    /// ИД родительской ревизии.
    /// </summary>
    [Required]
    public Guid ParentId { get; set; }

    /// <summary>
    /// ИД.
    /// </summary>
    [Required]
    public Guid Id { get; set; }

    /// <summary>
    /// Префикс.
    /// </summary>
    /// <example>БФПО-000</example>
    [Required]
    [RegularExpression(StringFormat.Prefix)]
    public string Prefix { get; set; }

    /// <summary>
    /// Наименование.
    /// </summary>
    /// <example>ПМК</example>
    [Required]
    [StringLength(16, MinimumLength = 1)]
    public string Title { get; set; }

    /// <summary>
    /// Версия проекта.
    /// </summary>
    /// <example>00</example>
    [Required]
    [RegularExpression("^[0-9]{2}$")]
    public string Version { get; set; }

    /// <summary>
    /// Редакция проекта.
    /// </summary>
    /// <example>00</example>
    [Required]
    [RegularExpression("^[0-9]{2}$")]
    public string Revision { get; set; }

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
    /// <example>2000-01-01</example>
    [Required]
    [RegularExpression("^[0-9]{4}-[0-9]{2}-[0-9]{2}$")]
    public string Date { get; set; }

    /// <summary>
    /// Платформа.
    /// </summary>
    /// <example>БМРЗ-000</example>
    [Required]
    [RegularExpression(StringFormat.Platform)]
    public string Platform { get; set; }
}