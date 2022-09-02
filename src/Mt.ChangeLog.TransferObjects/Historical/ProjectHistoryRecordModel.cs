using System;
using System.Text;

namespace Mt.ChangeLog.TransferObjects.Historical
{
    /// <summary>
    /// Полная модель записи истории проекта.
    /// </summary>
    public class ProjectHistoryRecordModel : ProjectHistoryRecordShortModel
    {
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
        public string ArmEdit { get; set; }
        
        /// <summary>
        /// Перечень алгоритмов.
        /// </summary>
        public string Algorithms { get; set; }
        
        /// <summary>
        /// Перечень авторов.
        /// </summary>
        public string Authors { get; set; }
        
        /// <summary>
        /// Перечень протоколов.
        /// </summary>
        public string Protocols { get; set; }
        
        /// <summary>
        /// Причина изменений.
        /// </summary>
        public string Reason { get; set; }
        
        /// <summary>
        /// Описание.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Преобразовать в текстовый формат.
        /// </summary>
        /// <returns>Строка с текстом.</returns>
        public string ToText()
        {
            StringBuilder sb = new StringBuilder();
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
}