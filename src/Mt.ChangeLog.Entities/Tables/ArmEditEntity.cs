using System.Linq.Expressions;

using Mt.Entities.Abstractions.Interfaces;
using Mt.Utilities;

namespace Mt.ChangeLog.Entities.Tables;

/// <summary>
/// Сущность ArmEdit.
/// </summary>
public class ArmEditEntity : IDefaultable, IEntity, IEqualityPredicate<ArmEditEntity>, IRemovable
{
    /// <summary>
    /// Инициализация экземпляра <see cref="ArmEditEntity"/>.
    /// </summary>
    public ArmEditEntity()
    {
        Id = Guid.NewGuid();
        DIVG = DefaultString.DIVG;
        Version = DefaultString.Version;
        Date = DateTime.Now;
        Description = DefaultString.Description;
        Default = false;
        Removable = true;
        ProjectRevisions = new HashSet<ProjectRevisionEntity>();
    }

    /// <inheritdoc />
    public Guid Id { get; set; }

    /// <summary>
    /// ДИВГ.
    /// </summary>
    public string DIVG { get; set; }

    /// <summary>
    /// Версия ArmEdit.
    /// </summary>
    public string Version { get; set; }

    /// <summary>
    /// Дата и время компиляции.
    /// </summary>
    public DateTime Date { get; set; }

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
    public ICollection<ProjectRevisionEntity> ProjectRevisions { get; set; }
    #endregion

    /// <inheritdoc />
    public Expression<Func<ArmEditEntity, bool>> GetEqualityPredicate()
    {
        /*
         * пока нет полных данных по ДИВГ-ам
         * return (ArmEdit e) => e.Id == Id || e.DIVG == DIVG || e.Version == Version
         */
        return (ArmEditEntity e) => e.Id == Id || e.Version == Version;
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return obj is ArmEditEntity arm && (Id.Equals(arm.Id) || Version == arm.Version);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return HashCode.Combine(DIVG, Version);
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return $"ID: {Id}, ArmEdit: {DIVG}, {Version}";
    }
}