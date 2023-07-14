using Mt.Entities.Abstractions.Interfaces;
using Mt.Utilities;
using System.Linq.Expressions;

namespace Mt.ChangeLog.Entities.Tables;

/// <summary>
/// Сущность ArmEdit.
/// </summary>
public class ArmEditEntity : IDefaultable, IEntity, IEqualityPredicate<ArmEditEntity>, IRemovable
{
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

    /// <summary>
    /// Инициализация экземпляра <see cref="ArmEditEntity"/>.
    /// </summary>
    public ArmEditEntity()
    {
        this.Id = Guid.NewGuid();
        this.DIVG = DefaultString.DIVG;
        this.Version = DefaultString.Version;
        this.Date = DateTime.Now;
        this.Description = DefaultString.Description;
        this.Default = false;
        this.Removable = true;
        this.ProjectRevisions = new HashSet<ProjectRevisionEntity>();
    }

    /// <inheritdoc />
    public Expression<Func<ArmEditEntity, bool>> GetEqualityPredicate()
    {
        /*
         * пока нет полных данных по ДИВГ-ам
         * return (ArmEdit e) => e.Id == this.Id || e.DIVG == this.DIVG || e.Version == this.Version
         */
        return (ArmEditEntity e) => e.Id == this.Id || e.Version == this.Version;
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return obj is ArmEditEntity arm && (this.Id.Equals(arm.Id) || this.Version == arm.Version);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return HashCode.Combine(this.DIVG, this.Version);
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return $"ID: {this.Id}, ArmEdit: {this.DIVG}, {this.Version}";
    }
}