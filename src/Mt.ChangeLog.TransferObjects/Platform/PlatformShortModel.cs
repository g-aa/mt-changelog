using Mt.Utilities;

namespace Mt.ChangeLog.TransferObjects.Platform;

/// <summary>
/// Краткая модель платформы БМРЗ.
/// </summary>
public class PlatformShortModel
{
    /// <summary>
    /// ИД.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Наименование.
    /// </summary>
    /// <example>БМРЗ-000</example>
    public string Title { get; set; }

    /// <summary>
    /// Инициализация экземпляра <see cref="PlatformShortModel"/>.
    /// </summary>
    public PlatformShortModel()
    {
        this.Id = Guid.NewGuid();
        this.Title = DefaultString.Platform;
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return this.Title;
    }
}