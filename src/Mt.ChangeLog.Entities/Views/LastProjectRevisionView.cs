using Mt.Utilities;

namespace Mt.ChangeLog.Entities.Views;

/// <summary>
/// Сущность последняя редакции проекта.
/// </summary>
public class LastProjectRevisionView
{
    /// <summary>
    /// Инициализация экземпляра класса <see cref="LastProjectRevisionView"/>.
    /// </summary>
    public LastProjectRevisionView()
    {
        ProjectVersionId = Guid.Empty;
        ProjectRevisionId = Guid.Empty;
        Prefix = DefaultString.Prefix;
        Title = DefaultString.Project;
        Version = DefaultString.Revision;
        Revision = DefaultString.Revision;
        Platform = DefaultString.Platform;
        ArmEdit = DefaultString.Version;
        Date = DateTime.Now;
    }

    /// <summary>
    /// ИД версии проекта.
    /// </summary>
    public Guid ProjectVersionId { get; set; }

    /// <summary>
    /// ИД редакции проекта.
    /// </summary>
    public Guid ProjectRevisionId { get; set; }

    /// <summary>
    /// Префикс.
    /// </summary>
    public string Prefix { get; set; }

    /// <summary>
    /// Наименование.
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// Версия.
    /// </summary>
    public string Version { get; set; }

    /// <summary>
    /// Редакция.
    /// </summary>
    public string Revision { get; set; }

    /// <summary>
    /// Платформа.
    /// </summary>
    public string Platform { get; set; }

    /// <summary>
    /// Версия ArmEdit.
    /// </summary>
    public string ArmEdit { get; set; }

    /// <summary>
    /// Дата компиляции.
    /// </summary>
    public DateTime Date { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return $"{Prefix}-{Title}-{Version}_{Revision}";
    }
}