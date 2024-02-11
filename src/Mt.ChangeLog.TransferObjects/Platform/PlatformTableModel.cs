using System.ComponentModel.DataAnnotations;

using Mt.Utilities;

namespace Mt.ChangeLog.TransferObjects.Platform;

/// <summary>
/// Модель платформы БМРЗ для таблиц.
/// </summary>
public class PlatformTableModel : PlatformShortModel
{
    /// <summary>
    /// Инициализация экземпляра <see cref="PlatformTableModel"/>.
    /// </summary>
    public PlatformTableModel()
        : base()
    {
        Description = DefaultString.Description;
    }

    /// <summary>
    /// Описание.
    /// </summary>
    /// <example>Описание...</example>
    [Required]
    [StringLength(500, MinimumLength = 0)]
    public string Description { get; set; }
}