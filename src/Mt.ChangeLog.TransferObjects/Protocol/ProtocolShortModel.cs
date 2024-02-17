using System.ComponentModel.DataAnnotations;

using Mt.Utilities;

namespace Mt.ChangeLog.TransferObjects.Protocol;

/// <summary>
/// Краткая модель протокола информационного обмена.
/// </summary>
public class ProtocolShortModel
{
    /// <summary>
    /// Инициализация экземпляра <see cref="ProtocolShortModel"/>.
    /// </summary>
    public ProtocolShortModel()
    {
        Id = Guid.NewGuid();
        Title = DefaultString.Protocol;
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
    /// <example>Modbus-MT</example>
    [Required]
    [StringLength(32, MinimumLength = 1)]
    public string Title { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return Title;
    }
}