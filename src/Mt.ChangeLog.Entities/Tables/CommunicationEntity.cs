using System.Linq.Expressions;

using Mt.Entities.Abstractions.Interfaces;
using Mt.Utilities;

namespace Mt.ChangeLog.Entities.Tables;

/// <summary>
/// Сущность коммуникационного модуля.
/// </summary>
public class CommunicationEntity : IDefaultable, IEntity, IEqualityPredicate<CommunicationEntity>, IRemovable
{
    /// <summary>
    /// Инициализация экземпляра <see cref="CommunicationEntity"/>.
    /// </summary>
    public CommunicationEntity()
    {
        Id = Guid.NewGuid();
        Title = DefaultString.Communication;
        Description = DefaultString.Description;
        Default = false;
        Removable = true;
        Protocols = new HashSet<ProtocolEntity>();
        ProjectRevisions = new HashSet<ProjectRevisionEntity>();
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
    /// Перечень протоколов.
    /// </summary>
    public ICollection<ProtocolEntity> Protocols { get; set; }

    /// <summary>
    /// Перечень редакций проектов.
    /// </summary>
    public ICollection<ProjectRevisionEntity> ProjectRevisions { get; set; }
    #endregion

    /// <inheritdoc />
    public Expression<Func<CommunicationEntity, bool>> GetEqualityPredicate()
    {
        return (CommunicationEntity e) => e.Id == Id || e.Title == Title;
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return obj is CommunicationEntity e && (Id.Equals(e.Id) || Title == e.Title);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return HashCode.Combine(Title);
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return $"ID: {Id}, адаптер: {Title}";
    }
}