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
        this.ProjectVersionId = Guid.Empty;
        this.ParentRevisionId = Guid.Empty;
        this.ArmEdit = DefaultString.Version;
        this.Algorithms = DefaultString.Algorithm;
        this.Authors = "Иванов Иван";
        this.Protocols = DefaultString.Protocol;
        this.Reason = DefaultString.Reason;
        this.Description = DefaultString.Description;
    }

    /// <summary>
    /// ИД версии проекта.
    /// </summary>
    public Guid ProjectVersionId { get; set; }

    /// <summary>
    /// ИД родительской редакции проекта.
    /// </summary>
    public Guid ParentRevisionId { get; set; }

    /// <summary>
    /// Версия ArmEdit.
    /// </summary>
    /// <example>v0.00.00.00</example>
    public string ArmEdit { get; set; }

    /// <summary>
    /// Перечень алгоритмов.
    /// </summary>
    /// <example>ТО, МТЗ, УРОВ, АПВ</example>
    public string Algorithms { get; set; }

    /// <summary>
    /// Перечень авторов.
    /// </summary>
    /// <example>Иванов Иван</example>
    public string Authors { get; set; }

    /// <summary>
    /// Перечень протоколов.
    /// </summary>
    /// <example>ModBus-RTU, Modbus-MT, Modbus-TCP</example>
    public string Protocols { get; set; }

    /// <summary>
    /// Причина изменений.
    /// </summary>
    /// <example>Причина изменения...</example>
    public string Reason { get; set; }

    /// <summary>
    /// Описание.
    /// </summary>
    /// <example>Описание...</example>
    public string Description { get; set; }

    /// <summary>
    /// Преобразовать в текстовый формат.
    /// </summary>
    /// <returns>Строка с текстом.</returns>
    public string ToText()
    {
        var sb = new StringBuilder();
        sb.AppendLine(new string('=', 100))
            .Append("Разработка ПрО:\t\t").Append(this.Title)
            .Append(" от ").Append(this.Date.ToString("yyyy-MM-dd"))
            .Append(" (").Append(this.Platform).Append(')')
            .Append(", авторы: ").Append(string.Join(", ", this.Authors)).AppendLine()
            .Append("Версия ArmEdit/CFG-Mt:\t").AppendLine(this.ArmEdit).AppendLine()
            .Append("Протоколы инф. обмена:\t").AppendLine(this.Protocols)
            .Append("Алгоритмы:\t\t").AppendLine(this.Algorithms).AppendLine()
            .Append("Причина изменения:\t").AppendLine(this.Reason).AppendLine()
            .AppendLine(this.Description);
        return sb.ToString();
    }
}