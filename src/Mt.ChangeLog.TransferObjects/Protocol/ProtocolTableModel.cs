using System.ComponentModel.DataAnnotations;

using Mt.Utilities;

namespace Mt.ChangeLog.TransferObjects.Protocol;

/// <summary>
/// Модель протокола информационного обмена для таблиц.
/// </summary>
public class ProtocolTableModel : ProtocolShortModel
{
    /// <summary>
    /// Инициализация экземпляра <see cref="ProtocolTableModel"/>.
    /// </summary>
    public ProtocolTableModel()
        : base()
    {
        Description = DefaultString.Description;
    }

    /// <summary>
    /// Описание.
    /// </summary>
    /// <example>Описание...</example>
    [Required]
    [StringLength(500, MinimumLength = 0)]
    public string Description { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return base.ToString();
    }
}