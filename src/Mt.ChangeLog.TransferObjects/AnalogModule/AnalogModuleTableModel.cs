using Mt.Utilities;

namespace Mt.ChangeLog.TransferObjects.AnalogModule;

/// <summary>
/// Модель аналогового модуля для таблиц.
/// </summary>
public class AnalogModuleTableModel : AnalogModuleShortModel
{
    /// <summary>
    /// ДИВГ.
    /// </summary>
    /// <example>ДИВГ.00000-00</example>
    public string DIVG { get; set; }

    /// <summary>
    /// Номинальный ток.
    /// </summary>
    /// <example>0A</example>
    public string Current { get; set; }

    /// <summary>
    /// Описание.
    /// </summary>
    /// <example>Описание...</example>
    public string Description { get; set; }

    /// <summary>
    /// Инициализация экземпляра <see cref="AnalogModuleTableModel"/>.
    /// </summary>
    public AnalogModuleTableModel() : base()
    {
        this.DIVG = DefaultString.DIVG;
        this.Current = DefaultString.Current;
        this.Description = DefaultString.Description;
    }
}