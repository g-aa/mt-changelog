using System.Collections.Generic;

namespace Mt.ChangeLog.TransferObjects.Historical
{
    public class ProjectVersionHistoryModel
    {
        /// <summary>
        /// наименование проекта, комбинация: "ProjectVersion.Prefix"-"ProjectVersion.Title"-"ProjectVersion.Version"
        /// </summary>
        public string Title { get; set; }
        public ICollection<ProjectRevisionHistoryModel> History { get; private set; }

        public ProjectVersionHistoryModel()
        {
            this.Title = "БМРЗ";
            this.History = new List<ProjectRevisionHistoryModel>();
        }
    }
}
