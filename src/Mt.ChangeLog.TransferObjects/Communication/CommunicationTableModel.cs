using System.ComponentModel.DataAnnotations;

using Mt.Utilities;

namespace Mt.ChangeLog.TransferObjects.Communication;

/// <summary>
/// Модель коммуникационного модуля для таблиц.
/// </summary>
public class CommunicationTableModel : CommunicationShortModel
{
    /// <summary>
    /// Инициализация экземпляра <see cref="CommunicationTableModel"/>.
    /// </summary>
    public CommunicationTableModel()
        : base()
    {
        Protocols = DefaultString.Protocol;
        Description = DefaultString.Description;
    }

    /// <summary>
    /// Перечень протоколов через ','.
    /// </summary>
    /// <example>ModBus-RTU, Modbus-MT, Modbus-TCP</example>
    [Required]
    [MinLength(1)]
    public string Protocols { get; set; }

    /// <summary>
    /// Описание.
    /// </summary>
    /// <example>Описание...</example>
    [Required]
    [StringLength(500, MinimumLength = 0)]
    public string Description { get; set; }
}