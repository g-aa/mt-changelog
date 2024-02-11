using System.ComponentModel.DataAnnotations;

using Mt.Utilities;

namespace Mt.ChangeLog.TransferObjects.RelayAlgorithm;

/// <summary>
/// Модель алгоритма РЗиА для таблиц.
/// </summary>
public class RelayAlgorithmTableModel : RelayAlgorithmShortModel
{
    /// <summary>
    /// Инициализация экземпляра <see cref="RelayAlgorithmTableModel"/>.
    /// </summary>
    public RelayAlgorithmTableModel()
        : base()
    {
        Group = DefaultString.AlgorithmGroup;
        ANSI = DefaultString.AlgorithmANSI;
        LogicalNode = DefaultString.LogicalNode;
        Description = DefaultString.Description;
    }

    /// <summary>
    /// Наименование группы.
    /// </summary>
    /// <example>МТЗ</example>
    [Required]
    [StringLength(32, MinimumLength = 0)]
    public string Group { get; set; }

    /// <summary>
    /// Код ANSI.
    /// </summary>
    /// <example>51</example>
    [Required]
    [RegularExpression("^[0-9 A-Z -/]{1,32}$")]
    public string ANSI { get; set; }

    /// <summary>
    /// Логический узел IEC-61850.
    /// </summary>
    /// <example>PTOC</example>
    [Required]
    [RegularExpression("^[0-9 A-Z -/]{1,32}$")]
    public string LogicalNode { get; set; }

    /// <summary>
    /// Описание.
    /// </summary>
    /// <example>Описание...</example>
    [Required]
    [StringLength(500, MinimumLength = 0)]
    public string Description { get; set; }
}