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
        this.ArmEdit = DefaultString.Version;
        this.Communication = DefaultString.Communication;
        this.Authors = new HashSet<string>();
        this.RelayAlgorithms = new HashSet<string>();
        this.Reason = DefaultString.Reason;
        this.Description = DefaultString.Description;
    }

    /// <summary>
    /// Версия ArmEdit.
    /// </summary>
    /// <example>v0.00.00.00</example>
    public string ArmEdit { get; set; }

    /// <summary>
    /// Коммуникационный модуль.
    /// </summary>
    /// <example>АК Virtual</example>
    public string Communication { get; set; }

    /// <summary>
    /// Перечень авторов.
    /// </summary>
    public IEnumerable<string> Authors { get; set; }

    /// <summary>
    /// Перечень алгоритмов.
    /// </summary>
    public IEnumerable<string> RelayAlgorithms { get; set; }

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
            .Append("Протоколы инф. обмена:\t").AppendLine(this.Communication)
            .Append("Алгоритмы:\t\t").AppendLine(string.Join(", ", this.RelayAlgorithms)).AppendLine()
            .Append("Причина изменения:\t").AppendLine(this.Reason).AppendLine()
            .AppendLine(this.Description);
        return sb.ToString();
    }
}