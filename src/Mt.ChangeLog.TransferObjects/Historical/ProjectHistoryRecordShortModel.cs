using System;

namespace Mt.ChangeLog.TransferObjects.Historical
{
    /// <summary>
    /// Краткая модель записи истории проекта.
    /// </summary>
    public class ProjectHistoryRecordShortModel
    {
        /// <summary>
        /// ИД редакции проекта.
        /// </summary>
        public Guid ProjectRevisionId { get; set; }
        
        /// <summary>
        /// Наименование проекта, комбинация: "ProjectVersion.Prefix"-"ProjectVersion.Title"-"ProjectVersion.Version"_"ProjectRevision.Revision".
        /// </summary>
        public string Title { get; set; }
        
        /// <summary>
        /// Дата компиляции.
        /// </summary>
        public DateTime Date { get; set; }
        
        /// <summary>
        /// Платформа.
        /// </summary>
        public string Platform { get; set; }
    }
}