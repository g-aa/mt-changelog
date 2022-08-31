using System.Collections.Generic;
using System.Text;

namespace Mt.ChangeLog.TransferObjects.Historical
{
    public class ProjectRevisionHistoryModel : ProjectRevisionHistoryShortModel
    {
        public string ArmEdit { get; set; }
        public string Communication { get; set; }

        public IEnumerable<string> Authors { get; set; }
        public IEnumerable<string> RelayAlgorithms { get; set; }

        public string Reason { get; set; }
        public string Description { get; set; }

        public ProjectRevisionHistoryModel()
        {
            this.Authors = new HashSet<string>();
            this.RelayAlgorithms = new HashSet<string>();
        }

        public string ToText()
        {
            StringBuilder sb = new StringBuilder();
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
}
