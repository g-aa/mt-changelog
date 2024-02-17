using System.Linq.Expressions;

using Mt.Entities.Abstractions.Interfaces;
using Mt.Utilities;

namespace Mt.ChangeLog.Entities.Tables;

/// <summary>
/// Сущность статус проекта.
/// </summary>
public class ProjectStatusEntity : IDefaultable, IEntity, IEqualityPredicate<ProjectStatusEntity>, IRemovable
{
    /// <summary>
    /// Инициализация экземпляра <see cref="ProjectStatusEntity"/>.
    /// </summary>
    public ProjectStatusEntity()
    {
        Id = Guid.NewGuid();
        Title = "Внутренний";
        Description = DefaultString.Description;
        Default = false;
        Removable = true;
        ProjectVersions = new HashSet<ProjectVersionEntity>();
    }

    /// <inheritdoc />
    public Guid Id { get; set; }

    /// <summary>
    /// Наименование.
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// Описание.
    /// </summary>
    public string Description { get; set; }

    /// <inheritdoc />
    public bool Default { get; set; }

    /// <inheritdoc />
    public bool Removable { get; set; }

    #region [ Relationships ]

    /// <summary>
    /// Перечень версий проектов.
    /// </summary>
    public ICollection<ProjectVersionEntity> ProjectVersions { get; set; }
    #endregion

    /// <inheritdoc />
    public Expression<Func<ProjectStatusEntity, bool>> GetEqualityPredicate()
    {
        return (ProjectStatusEntity e) => e.Id == Id || e.Title == Title;
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return obj is ProjectStatusEntity e && (Id.Equals(e.Id) || Title == e.Title);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return HashCode.Combine(Title);
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return $"ID: {Id}, статус: {Title}";
    }
}