using Mt.Utilities;

namespace Mt.ChangeLog.TransferObjects.RelayAlgorithm;

/// <summary>
/// Краткая модель алгоритма РЗиА.
/// </summary>
public class RelayAlgorithmShortModel
{
    /// <summary>
    /// Инициализация экземпляра <see cref="RelayAlgorithmShortModel"/>.
    /// </summary>
    public RelayAlgorithmShortModel()
    {
        this.Id = Guid.NewGuid();
        this.Title = DefaultString.Algorithm;
    }

    /// <summary>
    /// ИД.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Наименование.
    /// </summary>
    /// <example>МТЗ</example>
    public string Title { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return this.Title;
    }
}