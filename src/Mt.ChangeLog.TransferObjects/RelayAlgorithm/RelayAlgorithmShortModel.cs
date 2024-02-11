using System.ComponentModel.DataAnnotations;

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
        Id = Guid.NewGuid();
        Title = DefaultString.Algorithm;
    }

    /// <summary>
    /// ИД.
    /// </summary>
    [Required]
    public Guid Id { get; set; }

    /// <summary>
    /// Наименование.
    /// </summary>
    /// <example>МТЗ</example>
    [Required]
    [StringLength(32, MinimumLength = 1)]
    public string Title { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return Title;
    }
}