using System.Linq.Expressions;

using Mt.Entities.Abstractions.Interfaces;
using Mt.Utilities;

namespace Mt.ChangeLog.Entities.Tables;

/// <summary>
/// Сущность автора.
/// </summary>
public class AuthorEntity : IEntity, IDefaultable, IEqualityPredicate<AuthorEntity>, IRemovable
{
    /// <summary>
    /// Инициализация экземпляра <see cref="AuthorEntity"/>.
    /// </summary>
    public AuthorEntity()
    {
        Id = Guid.NewGuid();
        FirstName = DefaultString.FirstName;
        LastName = DefaultString.LastName;
        Position = DefaultString.Position;
        Default = false;
        Removable = true;
        ProjectRevisions = new HashSet<ProjectRevisionEntity>();
    }

    /// <inheritdoc />
    public Guid Id { get; set; }

    /// <summary>
    /// Имя.
    /// </summary>
    public string FirstName { get; set; }

    /// <summary>
    /// Фамилия.
    /// </summary>
    public string LastName { get; set; }

    /// <summary>
    /// Должность.
    /// </summary>
    public string Position { get; set; }

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
    public Expression<Func<AuthorEntity, bool>> GetEqualityPredicate()
    {
        return (AuthorEntity e) => e.Id == Id || (e.FirstName == FirstName && e.LastName == LastName);
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return obj is AuthorEntity e && (Id.Equals(e.Id) || (FirstName == e.FirstName && LastName == e.LastName));
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return HashCode.Combine(FirstName, LastName);
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return $"ID: {Id}, {LastName} {FirstName}";
    }
}