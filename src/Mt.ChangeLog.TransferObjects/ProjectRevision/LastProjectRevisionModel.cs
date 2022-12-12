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
        /// <example>БФПО-000</example>
        public string Prefix { get; set; }
        
        /// <summary>
        /// наименование.
        /// </summary>
        /// <example>ПМК</example>
        public string Title { get; set; }
        
        /// <summary>
        /// Версия.
        /// </summary>
        /// <example>00</example>
        public string Version { get; set; }

        /// <summary>
        /// Редакция.
        /// </summary>
        /// <example>00</example>
        public string Revision { get; set; }

        /// <summary>
        /// Платформа.
        /// </summary>
        /// <example>БМРЗ-000</example>
        public string Platform { get; set; }

        /// <summary>
        /// Версия ArmEdit.
        /// </summary>
        /// <example>v0.00.00.00</example>
        public string ArmEdit { get; set; }
        
        /// <summary>
        /// Дата компиляции.
        /// </summary>
        public DateTime Date { get; set; }
    }
}