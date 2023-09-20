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
        this.Protocols = DefaultString.Protocol;
        this.Description = DefaultString.Description;
    }

    /// <summary>
    /// Перечень протоколов через ','.
    /// </summary>
    /// <example>ModBus-RTU, Modbus-MT, Modbus-TCP</example>
    public string Protocols { get; set; }

    /// <summary>
    /// Описание.
    /// </summary>
    /// <example>Описание...</example>
    public string Description { get; set; }
}