using System.ComponentModel.DataAnnotations;

using Mt.Utilities;

namespace Mt.ChangeLog.TransferObjects.Author;

/// <summary>
/// Модель автор общий вклад в проекты.
/// </summary>
public class AuthorContributionModel
{
    /// <summary>
    /// Инициализация экземпляра <see cref="AuthorContributionModel"/>.
    /// </summary>
    public AuthorContributionModel()
    {
        Author = $"{DefaultString.LastName} {DefaultString.FirstName}";
    }

    /// <summary>
    /// Ф.И.О. автора (LastName FirstName).
    /// </summary>
    /// <example>Иванов Иван</example>
    [Required]
    [StringLength(65, MinimumLength = 3)]
    public string Author { get; set; }

    /// <summary>
    /// Общий вклад в проекты.
    /// </summary>
    [Required]
    [Range(0, int.MaxValue)]
    public int Contribution { get; set; }
}