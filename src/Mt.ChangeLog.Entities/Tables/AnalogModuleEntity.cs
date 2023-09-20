using System.Linq.Expressions;

using Mt.Entities.Abstractions.Interfaces;
using Mt.Utilities;

namespace Mt.ChangeLog.Entities.Tables;

/// <summary>
/// Сущность аналогового модуля.
/// </summary>
public class AnalogModuleEntity : IDefaultable, IEntity, IEqualityPredicate<AnalogModuleEntity>, IRemovable
{
    /// <summary>
    /// Инициализация экземпляра <see cref="AnalogModuleEntity"/>.
    /// </summary>
    public AnalogModuleEntity()
    {
        this.Id = Guid.NewGuid();
        this.DIVG = DefaultString.DIVG;
        this.Title = DefaultString.AnalogModule;
        this.Current = DefaultString.Current;
        this.Description = DefaultString.Description;
        this.Default = false;
        this.Removable = true;
        this.Projects = new HashSet<ProjectVersionEntity>();
        this.Platforms = new HashSet<PlatformEntity>();
    }

    /// <inheritdoc />
    public Guid Id { get; set; }

    /// <summary>
    /// ДИВГ.
    /// </summary>
    public string DIVG { get; set; }

    /// <summary>
    /// Наименование.
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// Номинальный ток.
    /// </summary>
    public string Current { get; set; }

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
    public ICollection<ProjectVersionEntity> Projects { get; set; }

    /// <summary>
    /// Перечень платформ.
    /// </summary>
    public ICollection<PlatformEntity> Platforms { get; set; }
    #endregion

    /// <inheritdoc />
    public Expression<Func<AnalogModuleEntity, bool>> GetEqualityPredicate()
    {
        /*
         * пока нет данных по ДИВГ-ам
         * return (AnalogModule e) => e.Id == this.Id || e.DIVG == this.DIVG || e.Title == this.Title
         */
        return (AnalogModuleEntity e) => e.Id == this.Id || e.Title == this.Title;
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return obj is AnalogModuleEntity module && (this.Id.Equals(module.Id) || this.Title == module.Title);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return HashCode.Combine(this.DIVG, this.Title, this.Current);
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return $"ID: {this.Id}, модуль: {this.Title}";
    }
}