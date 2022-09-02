using System.Collections.Generic;

namespace Mt.ChangeLog.TransferObjects.Historical
{
    /// <summary>
    /// Модель истории версии проекта.
    /// </summary>
    public class ProjectVersionHistoryModel
    {
        /// <summary>
        /// Наименование проекта, комбинация: "ProjectVersion.Prefix"-"ProjectVersion.Title"-"ProjectVersion.Version"
        /// </summary>
        public string Title { get; set; }
        
        /// <summary>
        /// Перечень моделей истории редакции проекта.
        /// </summary>
        public ICollection<ProjectRevisionHistoryModel> History { get; private set; }

        /// <summary>
        /// Инициализация экземпляра <see cref="ProjectVersionHistoryModel"/>
        /// </summary>
        public ProjectVersionHistoryModel()
        {
            this.Title = "БМРЗ";
            this.History = new List<ProjectRevisionHistoryModel>();
        }
    }
}