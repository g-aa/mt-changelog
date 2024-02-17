using System.ComponentModel.DataAnnotations;

using Mt.Utilities;

namespace Mt.ChangeLog.TransferObjects.AnalogModule;

/// <summary>
/// Модель аналогового модуля для таблиц.
/// </summary>
public class AnalogModuleTableModel : AnalogModuleShortModel
{
    /// <summary>
    /// Инициализация экземпляра <see cref="AnalogModuleTableModel"/>.
    /// </summary>
    public AnalogModuleTableModel()
        : base()
    {
        DIVG = DefaultString.DIVG;
        Current = DefaultString.Current;
        Description = DefaultString.Description;
    }

    /// <summary>
    /// ДИВГ.
    /// </summary>
    /// <example>ДИВГ.00000-00</example>
    [Required]
    [StringLength(13, MinimumLength = 13)]
    [RegularExpression(StringFormat.DIVG)]
    public string DIVG { get; set; }

    /// <summary>
    /// Номинальный ток.
    /// </summary>
    /// <example>0A</example>
    [Required]
    [StringLength(2, MinimumLength = 2)]
    [RegularExpression(StringFormat.Current)]
    public string Current { get; set; }

    /// <summary>
    /// Описание.
    /// </summary>
    /// <example>Описание...</example>
    [Required]
    [StringLength(500, MinimumLength = 0)]
    public string Description { get; set; }
}