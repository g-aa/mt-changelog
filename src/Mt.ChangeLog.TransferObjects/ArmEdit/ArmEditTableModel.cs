using Mt.Utilities;

namespace Mt.ChangeLog.TransferObjects.ArmEdit;

/// <summary>
/// Модель аналогового модуля для таблиц.
/// </summary>
public class ArmEditTableModel : ArmEditShortModel
{
    /// <summary>
    /// Инициализация экземпляра <see cref="ArmEditTableModel"/>.
    /// </summary>
    public ArmEditTableModel()
        : base()
    {
        this.DIVG = DefaultString.DIVG;
        this.Date = DateTime.Now;
        this.Description = DefaultString.Description;
    }

    /// <summary>
    /// ДИВГ.
    /// </summary>
    /// <example>ДИВГ.00000-00</example>
    public string DIVG { get; set; }

    /// <summary>
    /// Дата и время компиляции.
    /// </summary>
    public DateTime Date { get; set; }

    /// <summary>
    /// Описание.
    /// </summary>
    /// <example>Описание...</example>
    public string Description { get; set; }
}