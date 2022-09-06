﻿using System;

namespace Mt.ChangeLog.Entities.Views
{
    /// <summary>
    /// Сущность последняя редакции проекта.
    /// </summary>
    public class LastProjectRevision
    {
        /// <summary>
        /// ИД версии проекта.
        /// </summary>
        public Guid ProjectVersionId { get; set; }
        
        /// <summary>
        /// ИД редакции проекта.
        /// </summary>
        public Guid ProjectRevisionId { get; set; }

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

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{this.Prefix}-{this.Title}-{this.Version}_{this.Revision}";
        }
    }
}