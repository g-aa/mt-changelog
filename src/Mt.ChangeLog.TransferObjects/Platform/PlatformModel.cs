using System.ComponentModel.DataAnnotations;

using Mt.ChangeLog.TransferObjects.AnalogModule;

namespace Mt.ChangeLog.TransferObjects.Platform;

/// <summary>
/// Полная модель платформы БМРЗ.
/// </summary>
public class PlatformModel : PlatformTableModel
{
    /// <summary>
    /// Инициализация экземпляра <see cref="PlatformModel"/>.
    /// </summary>
    public PlatformModel()
        : base()
    {
        AnalogModules = new HashSet<AnalogModuleShortModel>();
    }

    /// <summary>
    /// Перечень аналоговых модулей.
    /// </summary>
    [Required]
    public IReadOnlyCollection<AnalogModuleShortModel> AnalogModules { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return base.ToString();
    }
}