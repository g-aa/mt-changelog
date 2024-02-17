using System.ComponentModel.DataAnnotations;

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
        Description = DefaultString.Description;
        Protocols = new HashSet<ProtocolShortModel>();
    }

    /// <summary>
    /// Описание.
    /// </summary>
    /// <example>Описание...</example>
    [Required]
    [StringLength(500, MinimumLength = 0)]
    public string Description { get; set; }

    /// <summary>
    /// Перечень протоколов.
    /// </summary>
    [Required]
    public IReadOnlyCollection<ProtocolShortModel> Protocols { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return base.ToString();
    }
}