using Mt.Entities.Abstractions.Interfaces;
using Mt.Utilities;
using System.Linq.Expressions;

namespace Mt.ChangeLog.Entities.Tables;

/// <summary>
/// Сущность протокола информационнго обмена.
/// </summary>
public class ProtocolEntity : IDefaultable, IEntity, IEqualityPredicate<ProtocolEntity>, IRemovable
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
    /// Перечень коммуникационных модулей.
    /// </summary>
    public ICollection<CommunicationEntity> Communications { get; set; }
    #endregion

    /// <summary>
    /// Инициализация экземпляра <see cref="ProtocolEntity"/>.
    /// </summary>
    public ProtocolEntity()
    {
        this.Id = Guid.NewGuid();
        this.Title = DefaultString.Protocol;
        this.Description = DefaultString.Description;
        this.Default = false;
        this.Removable = true;
        this.Communications = new HashSet<CommunicationEntity>();
    }

    /// <inheritdoc />
    public Expression<Func<ProtocolEntity, bool>> GetEqualityPredicate()
    {
        return (ProtocolEntity e) => e.Id == this.Id || e.Title == this.Title;
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return obj is ProtocolEntity protocol && (this.Id.Equals(protocol.Id) || this.Title == protocol.Title);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return HashCode.Combine(this.Title);
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return $"ID: {this.Id}, протокол: {this.Title}";
    }
}