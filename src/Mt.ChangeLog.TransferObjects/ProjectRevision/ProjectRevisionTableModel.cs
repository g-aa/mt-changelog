using System.ComponentModel.DataAnnotations;

using Mt.Utilities;

namespace Mt.ChangeLog.TransferObjects.ProjectRevision;

/// <summary>
/// Модель редакции проекта для таблиц.
/// </summary>
public class ProjectRevisionTableModel : ProjectRevisionShortModel
{
    /// <summary>
    /// Инициализация экземпляра <see cref="ProjectRevisionTableModel"/>.
    /// </summary>
    public ProjectRevisionTableModel()
        : base()
    {
        Date = DateTime.Now;
        ArmEdit = DefaultString.Version;
        Reason = DefaultString.Reason;
    }

    /// <summary>
    /// Дата компиляции.
    /// </summary>
    [Required]
    public DateTime Date { get; set; }

    /// <summary>
    /// Версия ArmEdit.
    /// </summary>
    /// <example>v0.00.00.00</example>
    [Required]
    [StringLength(11, MinimumLength = 11)]
    [RegularExpression(StringFormat.Version)]
    public string ArmEdit { get; set; }

    /// <summary>
    /// Причина изменений.
    /// </summary>
    /// <example>Причина изменения...</example>
    [Required]
    [StringLength(500, MinimumLength = 0)]
    public string Reason { get; set; }
}