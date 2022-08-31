using System;

namespace Mt.ChangeLog.TransferObjects.Historical
{
    public class ProjectRevisionHistoryShortModel
    {
        public Guid Id { get; set; }
        /// <summary>
        /// наименование проекта, комбинация: "ProjectVersion.Prefix"-"ProjectVersion.Title"-"ProjectVersion.Version"_"ProjectRevision.Revision"
        /// </summary>
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string Platform { get; set; }
    }
}
