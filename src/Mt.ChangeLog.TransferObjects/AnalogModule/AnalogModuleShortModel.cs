using Mt.Utilities;

namespace Mt.ChangeLog.TransferObjects.AnalogModule;

/// <summary>
/// Краткая модель аналогового модуля.
/// </summary>
public class AnalogModuleShortModel
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
    /// Инициализация экземпляра <see cref="AnalogModuleShortModel"/>.
    /// </summary>
    public AnalogModuleShortModel()
    {
        this.Id = Guid.NewGuid();
        this.Title = DefaultString.AnalogModule;
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return this.Title;
    }
}