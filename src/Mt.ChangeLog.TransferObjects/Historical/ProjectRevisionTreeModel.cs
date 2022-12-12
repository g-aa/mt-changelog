using System;

using Mt.Utilities;

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
        /// <example>БФПО-000</example>
        public string Prefix { get; set; }

        /// <summary>
        /// Наименование.
        /// </summary>
        /// <example>ПМК</example>
        public string Title { get; set; }
        
        /// <summary>
        /// Версия проекта.
        /// </summary>
        /// <example>00</example>
        public string Version { get; set; }

        /// <summary>
        /// Редакция проекта.
        /// </summary>
        /// <example>00</example>
        public string Revision { get; set; }

        /// <summary>
        /// Версия ArmEdit.
        /// </summary>
        /// <example>v0.00.00.00</example>
        public string ArmEdit { get; set; }

        /// <summary>
        /// Дата компиляции.
        /// </summary>
        /// <example>2000-01-01</example>
        public string Date { get; set; }

        /// <summary>
        /// Платформа.
        /// </summary>
        /// <example>БМРЗ-000</example>
        public string Platform { get; set; }
    }
}