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
    [Required]
    public Guid Id { get; set; }

    /// <summary>
    /// Наименование.
    /// </summary>
    /// <example>БМРЗ-000</example>
    [Required]
    [RegularExpression(StringFormat.AnalogModule)]
    public string Title { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return Title;
    }
}