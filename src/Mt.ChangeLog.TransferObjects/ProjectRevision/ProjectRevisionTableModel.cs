using Mt.Utilities;
using System;

namespace Mt.ChangeLog.TransferObjects.ProjectRevision
{
    /// <summary>
    /// Модель редакции проекта для таблиц.
    /// </summary>
    public class ProjectRevisionTableModel : ProjectRevisionShortModel
    {
        /// <summary>
        /// Дата компиляции.
        /// </summary>
        public DateTime Date { get; set; }
        
        /// <summary>
        /// Версия ArmEdit.
        /// </summary>
        public string ArmEdit { get; set; }

        /// <summary>
        /// Причина изменений.
        /// </summary>
        public string Reason { get; set; }

        /// <summary>
        /// Инициализация экземпляра <see cref="ProjectRevisionTableModel"/>
        /// </summary>
        public ProjectRevisionTableModel() : base()
        {
            this.Date = DateTime.Now;
            this.ArmEdit = DefaultString.Version;
            this.Reason = "Укажите причину изменения...";
        }
    }
}