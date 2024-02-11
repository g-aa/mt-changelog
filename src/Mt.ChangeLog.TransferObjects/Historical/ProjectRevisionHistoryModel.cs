using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text;

using Mt.Utilities;

namespace Mt.ChangeLog.TransferObjects.Historical;

/// <summary>
/// Полная модель записи ревизии истории проекта.
/// </summary>
public class ProjectRevisionHistoryModel : ProjectRevisionHistoryShortModel
{
    /// <summary>
    /// Инициализация экземпляра <see cref="ProjectRevisionHistoryModel"/>.
    /// </summary>
    public ProjectRevisionHistoryModel()
    {
        ArmEdit = DefaultString.Version;
        Communication = DefaultString.Communication;
        Authors = new HashSet<string>();
        RelayAlgorithms = new HashSet<string>();
        Reason = DefaultString.Reason;
        Description = DefaultString.Description;
    }

    /// <summary>
    /// Версия ArmEdit.
    /// </summary>
    /// <example>v0.00.00.00</example>
    [Required]
    [StringLength(11, MinimumLength = 11)]
    [RegularExpression(StringFormat.Version)]
    public string ArmEdit { get; set; }

    /// <summary>
    /// Коммуникационный модуль.
    /// </summary>
    /// <example>АК Virtual</example>
    [Required]
    [MinLength(1)]
    public string Communication { get; set; }

    /// <summary>
    /// Перечень авторов.
    /// </summary>
    [Required]
    public IReadOnlyCollection<string> Authors { get; set; }

    /// <summary>
    /// Перечень алгоритмов.
    /// </summary>
    [Required]
    public IReadOnlyCollection<string> RelayAlgorithms { get; set; }

    /// <summary>
    /// Причина изменений.
    /// </summary>
    /// <example>Причина изменения...</example>
    [Required]
    [StringLength(500, MinimumLength = 0)]
    public string Reason { get; set; }

    /// <summary>
    /// Описание.
    /// </summary>
    /// <example>Описание...</example>
    [Required]
    [StringLength(5000, MinimumLength = 0)]
    public string Description { get; set; }

    /// <summary>
    /// Преобразовать в текстовый формат.
    /// </summary>
    /// <returns>Строка с текстом.</returns>
    public string ToText()
    {
        return new StringBuilder()
            .AppendLine(new string('=', 100))
            .Append("Разработка ПрО:\t\t").Append(Title)
            .Append(" от ").Append(Date.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture))
            .Append(" (").Append(Platform).Append(')')
            .Append(", авторы: ").Append(string.Join(", ", Authors)).AppendLine()
            .Append("Версия ArmEdit/CFG-Mt:\t").AppendLine(ArmEdit).AppendLine()
            .Append("Протоколы инф. обмена:\t").AppendLine(Communication)
            .Append("Алгоритмы:\t\t").AppendLine(string.Join(", ", RelayAlgorithms)).AppendLine()
            .Append("Причина изменения:\t").AppendLine(Reason).AppendLine()
            .AppendLine(Description)
            .ToString();
    }
}