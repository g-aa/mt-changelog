using System.ComponentModel.DataAnnotations;

using Mt.ChangeLog.TransferObjects.Communication;

namespace Mt.ChangeLog.TransferObjects.Protocol;

/// <summary>
/// Полная модель протокола информационного обмена.
/// </summary>
public class ProtocolModel : ProtocolTableModel
{
    /// <summary>
    /// Инициализация экземпляра <see cref="ProtocolModel"/>.
    /// </summary>
    public ProtocolModel()
        : base()
    {
        Communications = new HashSet<CommunicationShortModel>();
    }

    /// <summary>
    /// Перечень коммуникационных модулей.
    /// </summary>
    [Required]
    public IReadOnlyCollection<CommunicationShortModel> Communications { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return base.ToString();
    }
}