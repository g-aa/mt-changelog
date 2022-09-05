using Mt.Entities.Abstractions.Interfaces;
using Mt.Utilities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Mt.ChangeLog.Entities.Tables
{
    /// <summary>
    /// Сущность версии проекта.
    /// </summary>
    public class ProjectVersion : IEntity, IEqualityPredicate<ProjectVersion>
    {
        /// <inheritdoc />
        public Guid Id { get; set; }

        /// <summary>
        /// ДИВГ.
        /// </summary>
        public string DIVG { get; set; }

        /// <summary>
        /// Префикс.
        /// </summary>
        public string Prefix { get; set; }

        /// <summary>
        /// Наименование.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Версия.
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// Описание.
        /// </summary>
        public string Description { get; set; }

        #region [ Relationships ]

        /// <summary>
        /// ИД платформы.
        /// </summary>
        public Guid PlatformId { get; set; }

        /// <summary>
        /// Платформа.
        /// </summary>
        public Platform Platform { get; set; }

        /// <summary>
        /// ИД аналогового модуля.
        /// </summary>
        public Guid AnalogModuleId { get; set; }

        /// <summary>
        /// Аналоговый модуль.
        /// </summary>
        public AnalogModule AnalogModule { get; set; }

        /// <summary>
        /// ИД статуса проекта.
        /// </summary>
        public Guid ProjectStatusId { get; set; }

        /// <summary>
        /// Статус проекта.
        /// </summary>
        public ProjectStatus ProjectStatus { get; set; } 

        /// <summary>
        /// Перечень редакций проектов.
        /// </summary>
        public ICollection<ProjectRevision> ProjectRevisions { get; set; }
        #endregion

        /// <summary>
        /// Инициализация экземпляра <see cref="ProjectVersion"/>.
        /// </summary>
        public ProjectVersion()
        {
            this.Id = Guid.NewGuid();
            this.DIVG = DefaultString.DIVG;
            this.Prefix = DefaultString.Prefix;
            this.Title = DefaultString.Project;
            this.Version = DefaultString.Revision;
            this.Description = DefaultString.Description;
            this.ProjectRevisions = new HashSet<ProjectRevision>();
        }

        /// <inheritdoc />
        public Expression<Func<ProjectVersion, bool>> GetEqualityPredicate()
        {
            return (ProjectVersion e) => e.Id == this.Id 
            || e.DIVG == this.DIVG 
            || e.Prefix == this.Prefix && e.Title == this.Title && e.Version == this.Version;
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return HashCode.Combine(this.DIVG, this.Prefix, this.Title, this.Version);
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"ID: {this.Id}, {this.DIVG}, {this.Prefix}-{this.Title}-{this.Version}";
        }
    }
}