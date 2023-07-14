using Mt.ChangeLog.TransferObjects.Platform;

namespace Mt.ChangeLog.TransferObjects.AnalogModule;

/// <summary>
/// Полная модель аналогового модуля.
/// </summary>
public class AnalogModuleModel : AnalogModuleTableModel
{
    /// <summary>
    /// Перечень платформ.
    /// </summary>
    public IEnumerable<PlatformShortModel> Platforms { get; set; }

    /// <summary>
    /// Инициализация экземпляра <see cref="AnalogModuleModel"/>.
    /// </summary>
    public AnalogModuleModel() : base()
    {
        this.Platforms = new HashSet<PlatformShortModel>();
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return $"{base.ToString()}, номинальный ток: {this.Current}";
    }
}