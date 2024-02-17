using System.ComponentModel.DataAnnotations;

using Mt.ChangeLog.TransferObjects.Platform;

namespace Mt.ChangeLog.TransferObjects.AnalogModule;

/// <summary>
/// Полная модель аналогового модуля.
/// </summary>
public class AnalogModuleModel : AnalogModuleTableModel
{
    /// <summary>
    /// Инициализация экземпляра <see cref="AnalogModuleModel"/>.
    /// </summary>
    public AnalogModuleModel()
        : base()
    {
        Platforms = new HashSet<PlatformShortModel>();
    }

    /// <summary>
    /// Перечень платформ.
    /// </summary>
    [Required]
    public IReadOnlyCollection<PlatformShortModel> Platforms { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return $"{base.ToString()}, номинальный ток: {Current}";
    }
}