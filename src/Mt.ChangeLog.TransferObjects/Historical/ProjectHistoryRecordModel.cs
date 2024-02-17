using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text;

using Mt.Utilities;

namespace Mt.ChangeLog.TransferObjects.Historical;

/// <summary>
/// Полная модель записи истории проекта.
/// </summary>
public class ProjectHistoryRecordModel : ProjectHistoryRecordShortModel
{
    /// <summary>
    /// Инициализация экземпляра <see cref="ProjectHistoryRecordModel"/>.
    /// </summary>
    public ProjectHistoryRecordModel()
        : base()
    {
        ProjectVersionId = Guid.Empty;
        ParentRevisionId = Guid.Empty;
        ArmEdit = DefaultString.Version;
        Algorithms = DefaultString.Algorithm;
        Authors = "Иванов Иван";
        Protocols = DefaultString.Protocol;
        Reason = DefaultString.Reason;
        Description = DefaultString.Description;
    }

    /// <summary>
    /// ИД версии проекта.
    /// </summary>
    [Required]
    public Guid ProjectVersionId { get; set; }

    /// <summary>
    /// ИД родительской редакции проекта.
    /// </summary>
    [Required]
    public Guid ParentRevisionId { get; set; }

    /// <summary>
    /// Версия ArmEdit.
    /// </summary>
    /// <example>v0.00.00.00</example>
    [Required]
    [StringLength(11, MinimumLength = 11)]
    [RegularExpression(StringFormat.Version)]
    public string ArmEdit { get; set; }

    /// <summary>
    /// Перечень алгоритмов.
    /// </summary>
    /// <example>ТО, МТЗ, УРОВ, АПВ</example>
    [Required]
    [MinLength(1)]
    public string Algorithms { get; set; }

    /// <summary>
    /// Перечень авторов.
    /// </summary>
    /// <example>Иванов Иван</example>
    [Required]
    [StringLength(65, MinimumLength = 3)]
    public string Authors { get; set; }

    /// <summary>
    /// Перечень протоколов.
    /// </summary>
    /// <example>ModBus-RTU, Modbus-MT, Modbus-TCP</example>
    [Required]
    [MinLength(1)]
    public string Protocols { get; set; }

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
            .Append("Протоколы инф. обмена:\t").AppendLine(Protocols)
            .Append("Алгоритмы:\t\t").AppendLine(Algorithms).AppendLine()
            .Append("Причина изменения:\t").AppendLine(Reason).AppendLine()
            .AppendLine(Description)
            .ToString();
    }
}