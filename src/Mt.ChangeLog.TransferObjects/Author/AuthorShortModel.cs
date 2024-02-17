using System.ComponentModel.DataAnnotations;

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
        Id = Guid.NewGuid();
        FirstName = DefaultString.FirstName;
        LastName = DefaultString.LastName;
    }

    /// <summary>
    /// ИД.
    /// </summary>
    /// <example>00000000-0000-0000-0000-000000000000</example>
    [Required]
    public Guid Id { get; set; }

    /// <summary>
    /// Имя.
    /// </summary>
    /// <example>Иван</example>
    [Required]
    [StringLength(32, MinimumLength = 1)]
    public string FirstName { get; set; }

    /// <summary>
    /// Фамилия.
    /// </summary>
    /// <example>Иванов</example>
    [Required]
    [StringLength(32, MinimumLength = 1)]
    public string LastName { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return $"{LastName} {FirstName}";
    }
}