using System.Linq.Expressions;

using Mt.Entities.Abstractions.Interfaces;
using Mt.Utilities;

namespace Mt.ChangeLog.Entities.Tables;

/// <summary>
/// Сущность версии проекта.
/// </summary>
public class ProjectVersionEntity : IEntity, IEqualityPredicate<ProjectVersionEntity>
{
    /// <summary>
    /// Инициализация экземпляра <see cref="ProjectVersionEntity"/>.
    /// </summary>
    public ProjectVersionEntity()
    {
        Id = Guid.NewGuid();
        DIVG = DefaultString.DIVG;
        Prefix = DefaultString.Prefix;
        Title = DefaultString.Project;
        Version = DefaultString.Revision;
        Description = DefaultString.Description;
        ProjectRevisions = new HashSet<ProjectRevisionEntity>();
    }

    /// <inheritdoc />
    public Guid Id { get; set; }

    /// <summary>
    /// ДИВГ.
    /// </summary>
    public string DIVG { get; set; }

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
    /// Описание.
    /// </summary>
    public string Description { get; set; }

    #region [ Relationships ]

    /// <summary>
    /// ИД платформы.
    /// </summary>
    public Guid PlatformId { get; set; }

    /// <summary>
    /// Платформа.
    /// </summary>
    public PlatformEntity? Platform { get; set; }

    /// <summary>
    /// ИД аналогового модуля.
    /// </summary>
    public Guid AnalogModuleId { get; set; }

    /// <summary>
    /// Аналоговый модуль.
    /// </summary>
    public AnalogModuleEntity? AnalogModule { get; set; }

    /// <summary>
    /// ИД статуса проекта.
    /// </summary>
    public Guid ProjectStatusId { get; set; }

    /// <summary>
    /// Статус проекта.
    /// </summary>
    public ProjectStatusEntity? ProjectStatus { get; set; }

    /// <summary>
    /// Перечень редакций проектов.
    /// </summary>
    public ICollection<ProjectRevisionEntity> ProjectRevisions { get; set; }
    #endregion

    /// <inheritdoc />
    public Expression<Func<ProjectVersionEntity, bool>> GetEqualityPredicate()
    {
        return (ProjectVersionEntity e) =>
            e.Id == Id || e.DIVG == DIVG || (e.Prefix == Prefix && e.Title == Title && e.Version == Version);
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return obj is ProjectVersionEntity e && (Id.Equals(e.Id)
            || DIVG == e.DIVG
            || (Prefix == e.Prefix && Title == e.Title && Version == e.Version));
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return HashCode.Combine(DIVG, Prefix, Title, Version);
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return $"ID: {Id}, {DIVG}, {Prefix}-{Title}-{Version}";
    }
}