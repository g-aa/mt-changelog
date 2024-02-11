using System.ComponentModel.DataAnnotations;

using Mt.Utilities;

namespace Mt.ChangeLog.TransferObjects.Historical;

/// <summary>
/// Краткая модель записи истории проекта.
/// </summary>
public class ProjectHistoryRecordShortModel
{
    /// <summary>
    /// Инициализация экземпляра <see cref="ProjectHistoryRecordShortModel"/>.
    /// </summary>
    public ProjectHistoryRecordShortModel()
    {
        ProjectRevisionId = Guid.Empty;
        Title = "БФПО-000-ПМК-00_00";
        Date = DateTime.UtcNow;
        Platform = DefaultString.Platform;
    }

    /// <summary>
    /// ИД редакции проекта.
    /// </summary>
    /// <example>00000000-0000-0000-0000-000000000000</example>
    [Required]
    public Guid ProjectRevisionId { get; set; }

    /// <summary>
    /// Наименование проекта, комбинация: "ProjectVersion.Prefix"-"ProjectVersion.Title"-"ProjectVersion.Version"_"ProjectRevision.Revision".
    /// </summary>
    /// <example>БФПО-000-ПМК-00_00</example>
    [Required]
    [StringLength(20, MinimumLength = 13)]
    [RegularExpression(StringFormat.BFPO)]
    public string Title { get; set; }

    /// <summary>
    /// Дата компиляции.
    /// </summary>
    [Required]
    public DateTime Date { get; set; }

    /// <summary>
    /// Платформа.
    /// </summary>
    /// <example>БМРЗ-000</example>
    [Required]
    [StringLength(10, MinimumLength = 7)]
    [RegularExpression(StringFormat.Platform)]
    public string Platform { get; set; }
}