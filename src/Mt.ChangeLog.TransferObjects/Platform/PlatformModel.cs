using Mt.ChangeLog.TransferObjects.AnalogModule;

namespace Mt.ChangeLog.TransferObjects.Platform;

/// <summary>
/// Полная модель платформы БМРЗ.
/// </summary>
public class PlatformModel : PlatformTableModel
{
    /// <summary>
    /// Перечень аналоговых модулей.
    /// </summary>
    public IEnumerable<AnalogModuleShortModel> AnalogModules { get; set; }

    /// <summary>
    /// Инициализация экземпляра <see cref="PlatformModel"/>.
    /// </summary>
    public PlatformModel() : base()
    {
        this.AnalogModules = new HashSet<AnalogModuleShortModel>();
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return base.ToString();
    }
}