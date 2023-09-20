using System.Linq.Expressions;

using Mt.Entities.Abstractions.Interfaces;
using Mt.Utilities;

namespace Mt.ChangeLog.Entities.Tables;

/// <summary>
/// Сущность алгоритма РЗиА.
/// </summary>
public class RelayAlgorithmEntity : IEntity, IDefaultable, IEqualityPredicate<RelayAlgorithmEntity>, IRemovable
{
    /// <summary>
    /// Инициализация экземпляра <see cref="RelayAlgorithmEntity"/>.
    /// </summary>
    public RelayAlgorithmEntity()
    {
        this.Id = Guid.NewGuid();
        this.Group = DefaultString.AlgorithmGroup;
        this.Title = DefaultString.Algorithm;
        this.ANSI = DefaultString.AlgorithmANSI;
        this.LogicalNode = DefaultString.LogicalNode;
        this.Description = DefaultString.Description;
        this.Default = false;
        this.Removable = true;
        this.ProjectRevisions = new HashSet<ProjectRevisionEntity>();
    }

    /// <inheritdoc />
    public Guid Id { get; set; }

    /// <summary>
    /// Наименование группы.
    /// </summary>
    public string Group { get; set; }

    /// <summary>
    /// Наименование.
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// Код ANSI.
    /// </summary>
    public string ANSI { get; set; }

    /// <summary>
    /// Логический узел IEC-61850.
    /// </summary>
    public string LogicalNode { get; set; }

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
    /// Перечень редакций проектов.
    /// </summary>
    public ICollection<ProjectRevisionEntity> ProjectRevisions { get; set; }
    #endregion

    /// <inheritdoc />
    public Expression<Func<RelayAlgorithmEntity, bool>> GetEqualityPredicate()
    {
        return (RelayAlgorithmEntity e) => e.Id == this.Id || (e.Group == this.Group && e.Title == this.Title);
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return obj is RelayAlgorithmEntity algorithm && (this.Id.Equals(algorithm.Id) || (this.Group == algorithm.Group && this.Title == algorithm.Title));
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return HashCode.Combine(this.Group, this.Title, this.ANSI, this.LogicalNode);
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return $"ID: {this.Id}, ANSI: {this.ANSI}, {this.Title}";
    }
}