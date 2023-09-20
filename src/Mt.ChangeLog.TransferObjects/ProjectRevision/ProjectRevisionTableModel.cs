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
        this.Date = DateTime.Now;
        this.ArmEdit = DefaultString.Version;
        this.Reason = DefaultString.Reason;
    }

    /// <summary>
    /// Дата компиляции.
    /// </summary>
    public DateTime Date { get; set; }

    /// <summary>
    /// Версия ArmEdit.
    /// </summary>
    /// <example>v0.00.00.00</example>
    public string ArmEdit { get; set; }

    /// <summary>
    /// Причина изменений.
    /// </summary>
    /// <example>Причина изменения...</example>
    public string Reason { get; set; }
}