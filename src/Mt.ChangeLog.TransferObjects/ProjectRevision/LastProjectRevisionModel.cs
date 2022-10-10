using System;

namespace Mt.ChangeLog.TransferObjects.ProjectRevision
{
    /// <summary>
    /// Последняя редакции проекта.
    /// </summary>
    public class LastProjectRevisionModel
    {
        /// <summary>
        /// Префикс.
        /// </summary>
        public string Prefix { get; set; }
        
        /// <summary>
        /// наименование.
        /// </summary>
        public string Title { get; set; }
        
        /// <summary>
        /// Версия.
        /// </summary>
        public string Version { get; set; }
        
        /// <summary>
        /// Редакция.
        /// </summary>
        public string Revision { get; set; }
        
        /// <summary>
        /// Платформа.
        /// </summary>
        public string Platform { get; set; }
        
        /// <summary>
        /// Версия ArmEdit.
        /// </summary>
        public string ArmEdit { get; set; }
        
        /// <summary>
        /// Дата компиляции.
        /// </summary>
        public DateTime Date { get; set; }
    }
}