using Mt.Utilities;

namespace Mt.ChangeLog.Entities.Views;

/// <summary>
/// Представление истории проекта.
/// </summary>
public class ProjectHistoryRecordView
{
    /// <summary>
    /// Инициализация экземпляра класса <see cref="ProjectHistoryRecordView"/>.
    /// </summary>
    public ProjectHistoryRecordView()
    {
        ProjectVersionId = Guid.Empty;
        ParentRevisionId = Guid.Empty;
        ProjectRevisionId = Guid.Empty;
        Platform = DefaultString.Platform;
        Title = DefaultString.Project;
        Date = DateTime.Now;
        ArmEdit = DefaultString.Version;
        Algorithms = DefaultString.Algorithm;
        Authors = $"{DefaultString.LastName} {DefaultString.FirstName}";
        Protocols = DefaultString.Protocol;
        Reason = string.Empty;
        Description = string.Empty;
    }

    /// <summary>
    /// ИД версии проекта.
    /// </summary>
    public Guid ProjectVersionId { get; set; }

    /// <summary>
    /// ИД родительской редакции проекта.
    /// </summary>
    public Guid ParentRevisionId { get; set; }

    /// <summary>
    /// ИД редакции проекта.
    /// </summary>
    public Guid ProjectRevisionId { get; set; }

    /// <summary>
    /// Платформа.
    /// </summary>
    public string Platform { get; set; }

    /// <summary>
    /// Наименование проекта, комбинация: "ProjectVersion.Prefix"-"ProjectVersion.Title"-"ProjectVersion.Version"_"ProjectRevision.Revision".
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// Дата компиляции.
    /// </summary>
    public DateTime Date { get; set; }

    /// <summary>
    /// Версия ArmEdit.
    /// </summary>
    public string ArmEdit { get; set; }

    /// <summary>
    /// Перечень алгоритмов.
    /// </summary>
    public string Algorithms { get; set; }

    /// <summary>
    /// Перечень авторов.
    /// </summary>
    public string Authors { get; set; }

    /// <summary>
    /// Перечень протоколов.
    /// </summary>
    public string Protocols { get; set; }

    /// <summary>
    /// Причина изменений.
    /// </summary>
    public string Reason { get; set; }

    /// <summary>
    /// Описание.
    /// </summary>
    public string Description { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return Title;
    }
}