using System;

namespace Mt.ChangeLog.TransferObjects.Historical
{
    /// <summary>
    /// Модель ревизии проекта для дерева версий.
    /// </summary>
    public class ProjectRevisionTreeModel
    {
        /// <summary>
        /// ИД родительской ревизии.
        /// </summary>
        public Guid ParentId { get; set; }
        
        /// <summary>
        /// ИД.
        /// </summary>
        public Guid Id { get; set; }
        
        /// <summary>
        /// Префикс.
        /// </summary>
        public string Prefix { get; set; }
        
        /// <summary>
        /// Наименование.
        /// </summary>
        public string Title { get; set; }
        
        /// <summary>
        /// Версия проекта.
        /// </summary>
        public string Version { get; set; }
        
        /// <summary>
        /// Редакция проекта.
        /// </summary>
        public string Revision { get; set; }
        
        /// <summary>
        /// Версия ArmEdit.
        /// </summary>
        public string ArmEdit { get; set; }
        
        /// <summary>
        /// Дата компиляции.
        /// </summary>
        public string Date { get; set; }
        
        /// <summary>
        /// Платформа.
        /// </summary>
        public string Platform { get; set; }
    }
}