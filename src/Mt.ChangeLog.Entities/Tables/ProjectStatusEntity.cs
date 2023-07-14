using Mt.Entities.Abstractions.Interfaces;
using Mt.Utilities;
using System.Linq.Expressions;

namespace Mt.ChangeLog.Entities.Tables;

/// <summary>
/// Сущность статус проекта.
/// </summary>
public class ProjectStatusEntity : IDefaultable, IEntity, IEqualityPredicate<ProjectStatusEntity>, IRemovable
{
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

    /// <summary>
    /// Инициализация экземпляра <see cref="ProjectStatusEntity"/>.
    /// </summary>
    public ProjectStatusEntity()
    {
        this.Id = Guid.NewGuid();
        this.Title = "Внутренний";
        this.Description = DefaultString.Description;
        this.Default = false;
        this.Removable = true;
        this.ProjectVersions = new HashSet<ProjectVersionEntity>();
    }

    /// <inheritdoc />
    public Expression<Func<ProjectStatusEntity, bool>> GetEqualityPredicate()
    {
        return (ProjectStatusEntity e) => e.Id == this.Id || e.Title == this.Title;
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return obj is ProjectStatusEntity e && (this.Id.Equals(e.Id) || this.Title == e.Title);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return HashCode.Combine(this.Title);
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return $"ID: {this.Id}, статус: {this.Title}";
    }
}