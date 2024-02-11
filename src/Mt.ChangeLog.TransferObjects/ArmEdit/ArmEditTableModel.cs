using System.ComponentModel.DataAnnotations;

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
        DIVG = DefaultString.DIVG;
        Date = DateTime.Now;
        Description = DefaultString.Description;
    }

    /// <summary>
    /// ДИВГ.
    /// </summary>
    /// <example>ДИВГ.00000-00</example>
    [Required]
    [RegularExpression(StringFormat.DIVG)]
    public string DIVG { get; set; }

    /// <summary>
    /// Дата и время компиляции.
    /// </summary>
    [Required]
    public DateTime Date { get; set; }

    /// <summary>
    /// Описание.
    /// </summary>
    /// <example>Описание...</example>
    [Required]
    [StringLength(500, MinimumLength = 0)]
    public string Description { get; set; }
}