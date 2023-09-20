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
        this.Id = Guid.NewGuid();
        this.Title = DefaultString.Protocol;
    }

    /// <summary>
    /// ИД.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Наименование.
    /// </summary>
    /// <example>Modbus-MT</example>
    public string Title { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return this.Title;
    }
}