using Mt.ChangeLog.TransferObjects.Protocol;
using Mt.Utilities;

namespace Mt.ChangeLog.TransferObjects.Communication;

/// <summary>
/// Полная модель коммуникационного модуля.
/// </summary>
public class CommunicationModel : CommunicationShortModel
{
    /// <summary>
    /// Инициализация экземпляра <see cref="CommunicationModel"/>.
    /// </summary>
    public CommunicationModel()
        : base()
    {
        this.Description = DefaultString.Description;
        this.Protocols = new HashSet<ProtocolShortModel>();
    }

    /// <summary>
    /// Описание.
    /// </summary>
    /// <example>Описание...</example>
    public string Description { get; set; }

    /// <summary>
    /// Перечень протоколов.
    /// </summary>
    public IEnumerable<ProtocolShortModel> Protocols { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return base.ToString();
    }
}