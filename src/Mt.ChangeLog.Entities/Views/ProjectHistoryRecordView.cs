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
        this.ProjectVersionId = Guid.Empty;
        this.ParentRevisionId = Guid.Empty;
        this.ProjectRevisionId = Guid.Empty;
        this.Platform = DefaultString.Platform;
        this.Title = DefaultString.Project;
        this.Date = DateTime.Now;
        this.ArmEdit = DefaultString.Version;
        this.Algorithms = DefaultString.Algorithm;
        this.Authors = $"{DefaultString.LastName} {DefaultString.FirstName}";
        this.Protocols = DefaultString.Protocol;
        this.Reason = string.Empty;
        this.Description = string.Empty;
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
        return this.Title;
    }
}