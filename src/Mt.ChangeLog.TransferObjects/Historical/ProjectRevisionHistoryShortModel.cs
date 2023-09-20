using Mt.Utilities;

namespace Mt.ChangeLog.TransferObjects.Historical;

/// <summary>
/// Краткая модель истории ревизии проекта.
/// </summary>
public class ProjectRevisionHistoryShortModel
{
    /// <summary>
    /// Инициализация экземпляра <see cref="ProjectRevisionHistoryShortModel"/>.
    /// </summary>
    public ProjectRevisionHistoryShortModel()
    {
        this.Id = Guid.Empty;
        this.Title = "БФПО-000-ПМК-00_00";
        this.Date = DateTime.UtcNow;
        this.Platform = DefaultString.Platform;
    }

    /// <summary>
    /// ИД.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Наименование проекта, комбинация: "ProjectVersion.Prefix"-"ProjectVersion.Title"-"ProjectVersion.Version"_"ProjectRevision.Revision".
    /// </summary>
    /// <example>БФПО-000-ПМК-00_00</example>
    public string Title { get; set; }

    /// <summary>
    /// Дата компиляции.
    /// </summary>
    public DateTime Date { get; set; }

    /// <summary>
    /// Платформа.
    /// </summary>
    /// <example>БМРЗ-000</example>
    public string Platform { get; set; }
}