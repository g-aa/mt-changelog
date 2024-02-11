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
        Id = Guid.NewGuid();
        Group = DefaultString.AlgorithmGroup;
        Title = DefaultString.Algorithm;
        ANSI = DefaultString.AlgorithmANSI;
        LogicalNode = DefaultString.LogicalNode;
        Description = DefaultString.Description;
        Default = false;
        Removable = true;
        ProjectRevisions = new HashSet<ProjectRevisionEntity>();
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
        return (RelayAlgorithmEntity e) => e.Id == Id || (e.Group == Group && e.Title == Title);
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return obj is RelayAlgorithmEntity algorithm && (Id.Equals(algorithm.Id) || (Group == algorithm.Group && Title == algorithm.Title));
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return HashCode.Combine(Group, Title, ANSI, LogicalNode);
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return $"ID: {Id}, ANSI: {ANSI}, {Title}";
    }
}