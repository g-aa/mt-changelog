using System.ComponentModel.DataAnnotations;

using Mt.Utilities;

namespace Mt.ChangeLog.TransferObjects.Author;

/// <summary>
/// Модель автора для таблиц.
/// </summary>
public class AuthorTableModel : AuthorShortModel
{
    /// <summary>
    /// Инициализация экземпляра <see cref="AuthorTableModel"/>.
    /// </summary>
    public AuthorTableModel()
        : base()
    {
        Position = DefaultString.Position;
    }

    /// <summary>
    /// Должность.
    /// </summary>
    /// <example>Инженер-системотехник</example>
    [Required]
    [StringLength(250, MinimumLength = 0)]
    public string Position { get; set; }
}