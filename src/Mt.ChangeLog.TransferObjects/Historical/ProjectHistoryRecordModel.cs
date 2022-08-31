using System;
using System.Text;

namespace Mt.ChangeLog.TransferObjects.Historical
{
    public class ProjectHistoryRecordModel : ProjectHistoryRecordShortModel
    {
        public Guid ProjectVersionId { get; set; }
        public Guid ParentRevisionId { get; set; }

        public string ArmEdit { get; set; }
        public string Algorithms { get; set; }
        public string Authors { get; set; }
        public string Protocols { get; set; }
        public string Reason { get; set; }
        public string Description { get; set; }

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
