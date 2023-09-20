using Mt.Utilities;

namespace Mt.ChangeLog.TransferObjects.Author;

/// <summary>
/// Краткая модель автора.
/// </summary>
public class AuthorShortModel
{
    /// <summary>
    /// Инициализация экземпляра <see cref="AuthorShortModel"/>.
    /// </summary>
    public AuthorShortModel()
    {
        this.Id = Guid.NewGuid();
        this.FirstName = DefaultString.FirstName;
        this.LastName = DefaultString.LastName;
    }

    /// <summary>
    /// ИД.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Имя.
    /// </summary>
    /// <example>Иван</example>
    public string FirstName { get; set; }

    /// <summary>
    /// Фамилия.
    /// </summary>
    /// <example>Иванов</example>
    public string LastName { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return $"{this.LastName} {this.FirstName}";
    }
}