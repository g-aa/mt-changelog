using System.ComponentModel.DataAnnotations;

using Mt.Utilities;

namespace Mt.ChangeLog.TransferObjects.AnalogModule;

/// <summary>
/// Краткая модель аналогового модуля.
/// </summary>
public class AnalogModuleShortModel
{
    /// <summary>
    /// Инициализация экземпляра <see cref="AnalogModuleShortModel"/>.
    /// </summary>
    public AnalogModuleShortModel()
    {
        Id = Guid.NewGuid();
        Title = DefaultString.AnalogModule;
    }

    /// <summary>
    /// ИД.
    /// </summary>
    /// <example>00000000-0000-0000-0000-000000000000</example>
    [Required]
    public Guid Id { get; set; }

    /// <summary>
    /// Наименование.
    /// </summary>
    /// <example>БМРЗ-000</example>
    [Required]
    [StringLength(9, MinimumLength = 7)]
    [RegularExpression(StringFormat.AnalogModule)]
    public string Title { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return Title;
    }
}