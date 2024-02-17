using System.Linq.Expressions;

using Mt.Entities.Abstractions.Interfaces;
using Mt.Utilities;

namespace Mt.ChangeLog.Entities.Tables;

/// <summary>
/// Сущность протокола информационного обмена.
/// </summary>
public class ProtocolEntity : IDefaultable, IEntity, IEqualityPredicate<ProtocolEntity>, IRemovable
{
    /// <summary>
    /// Инициализация экземпляра <see cref="ProtocolEntity"/>.
    /// </summary>
    public ProtocolEntity()
    {
        Id = Guid.NewGuid();
        Title = DefaultString.Protocol;
        Description = DefaultString.Description;
        Default = false;
        Removable = true;
        Communications = new HashSet<CommunicationEntity>();
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
    /// Перечень коммуникационных модулей.
    /// </summary>
    public ICollection<CommunicationEntity> Communications { get; set; }
    #endregion

    /// <inheritdoc />
    public Expression<Func<ProtocolEntity, bool>> GetEqualityPredicate()
    {
        return (ProtocolEntity e) => e.Id == Id || e.Title == Title;
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return obj is ProtocolEntity protocol && (Id.Equals(protocol.Id) || Title == protocol.Title);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return HashCode.Combine(Title);
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return $"ID: {Id}, протокол: {Title}";
    }
}