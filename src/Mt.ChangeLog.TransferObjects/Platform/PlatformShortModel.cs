using System.ComponentModel.DataAnnotations;

using Mt.Utilities;

namespace Mt.ChangeLog.TransferObjects.Platform;

/// <summary>
/// Краткая модель платформы БМРЗ.
/// </summary>
public class PlatformShortModel
{
    /// <summary>
    /// Инициализация экземпляра <see cref="PlatformShortModel"/>.
    /// </summary>
    public PlatformShortModel()
    {
        Id = Guid.NewGuid();
        Title = DefaultString.Platform;
    }

    /// <summary>
    /// ИД.
    /// </summary>
    [Required]
    public Guid Id { get; set; }

    /// <summary>
    /// Наименование.
    /// </summary>
    /// <example>БМРЗ-000</example>
    [Required]
    [RegularExpression(StringFormat.Platform)]
    public string Title { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return Title;
    }
}