using Mt.Utilities;

namespace Mt.ChangeLog.TransferObjects.Platform;

/// <summary>
/// Модель платформы БМРЗ для таблиц.
/// </summary>
public class PlatformTableModel : PlatformShortModel
{
    /// <summary>
    /// Инициализация экземпляра <see cref="PlatformTableModel"/>.
    /// </summary>
    public PlatformTableModel()
        : base()
    {
        this.Description = DefaultString.Description;
    }

    /// <summary>
    /// Описание.
    /// </summary>
    /// <example>Описание...</example>
    public string Description { get; set; }
}