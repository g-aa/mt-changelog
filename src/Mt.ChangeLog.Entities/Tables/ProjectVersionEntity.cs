using Mt.Entities.Abstractions.Interfaces;
using Mt.Utilities;
using System.Linq.Expressions;

namespace Mt.ChangeLog.Entities.Tables
{
    /// <summary>
    /// Сущность версии проекта.
    /// </summary>
    public class ProjectVersionEntity : IEntity, IEqualityPredicate<ProjectVersionEntity>
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
        public PlatformEntity Platform { get; set; }

        /// <summary>
        /// ИД аналогового модуля.
        /// </summary>
        public Guid AnalogModuleId { get; set; }

        /// <summary>
        /// Аналоговый модуль.
        /// </summary>
        public AnalogModuleEntity AnalogModule { get; set; }

        /// <summary>
        /// ИД статуса проекта.
        /// </summary>
        public Guid ProjectStatusId { get; set; }

        /// <summary>
        /// Статус проекта.
        /// </summary>
        public ProjectStatusEntity ProjectStatus { get; set; }

        /// <summary>
        /// Перечень редакций проектов.
        /// </summary>
        public ICollection<ProjectRevisionEntity> ProjectRevisions { get; set; }
        #endregion

        /// <summary>
        /// Инициализация экземпляра <see cref="ProjectVersionEntity"/>.
        /// </summary>
        public ProjectVersionEntity()
        {
            this.Id = Guid.NewGuid();
            this.DIVG = DefaultString.DIVG;
            this.Prefix = DefaultString.Prefix;
            this.Title = DefaultString.Project;
            this.Version = DefaultString.Revision;
            this.Description = DefaultString.Description;
            this.ProjectRevisions = new HashSet<ProjectRevisionEntity>();
        }

        /// <inheritdoc />
        public Expression<Func<ProjectVersionEntity, bool>> GetEqualityPredicate()
        {
            return (ProjectVersionEntity e) => e.Id == this.Id
            || e.DIVG == this.DIVG
            || e.Prefix == this.Prefix && e.Title == this.Title && e.Version == this.Version;
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            return obj is ProjectVersionEntity e && (this.Id.Equals(e.Id)
                || this.DIVG == e.DIVG
                || this.Prefix == e.Prefix && this.Title == e.Title && this.Version == e.Version);
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