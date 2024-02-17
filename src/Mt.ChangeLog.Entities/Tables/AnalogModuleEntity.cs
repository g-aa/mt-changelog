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
        Id = Guid.NewGuid();
        DIVG = DefaultString.DIVG;
        Title = DefaultString.AnalogModule;
        Current = DefaultString.Current;
        Description = DefaultString.Description;
        Default = false;
        Removable = true;
        Projects = new HashSet<ProjectVersionEntity>();
        Platforms = new HashSet<PlatformEntity>();
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
         * return (AnalogModule e) => e.Id == Id || e.DIVG == DIVG || e.Title == Title
         */
        return (AnalogModuleEntity e) => e.Id == Id || e.Title == Title;
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return obj is AnalogModuleEntity module && (Id.Equals(module.Id) || Title == module.Title);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return HashCode.Combine(DIVG, Title, Current);
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return $"ID: {Id}, модуль: {Title}";
    }
}