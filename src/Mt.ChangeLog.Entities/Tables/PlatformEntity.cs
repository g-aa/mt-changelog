using System.Linq.Expressions;

using Mt.Entities.Abstractions.Interfaces;
using Mt.Utilities;

namespace Mt.ChangeLog.Entities.Tables;

/// <summary>
/// Сущность платформы БМРЗ.
/// </summary>
public class PlatformEntity : IDefaultable, IEntity, IEqualityPredicate<PlatformEntity>, IRemovable
{
    /// <summary>
    /// Инициализация экземпляра <see cref="PlatformEntity"/>.
    /// </summary>
    public PlatformEntity()
    {
        Id = Guid.NewGuid();
        Title = DefaultString.Platform;
        Description = DefaultString.Description;
        Default = false;
        Removable = true;
        AnalogModules = new HashSet<AnalogModuleEntity>();
        Projects = new HashSet<ProjectVersionEntity>();
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
    /// Перечень аналоговых модулей.
    /// </summary>
    public ICollection<AnalogModuleEntity> AnalogModules { get; set; }

    /// <summary>
    /// Перечень версий проектов.
    /// </summary>
    public ICollection<ProjectVersionEntity> Projects { get; set; }
    #endregion

    /// <inheritdoc />
    public Expression<Func<PlatformEntity, bool>> GetEqualityPredicate()
    {
        return (PlatformEntity e) => e.Id == Id || e.Title == Title;
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return obj is PlatformEntity platform && (Id.Equals(platform.Id) || Title == platform.Title);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return HashCode.Combine(Title);
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return $"ID: {Id}, платформа: {Title}";
    }
}