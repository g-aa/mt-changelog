using Mt.Entities.Abstractions.Interfaces;
using Mt.Utilities;
using System.Linq.Expressions;

namespace Mt.ChangeLog.Entities.Tables
{
    /// <summary>
    /// Сущность редакции проекта.
    /// </summary>
    public class ProjectRevisionEntity : IEntity, IEqualityPredicate<ProjectRevisionEntity>
    {
        /// <inheritdoc />
        public Guid Id { get; set; }

        /// <summary>
        /// Дата компиляции.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Редакция.
        /// </summary>
        public string Revision { get; set; }

        /// <summary>
        /// Причина изменений.
        /// </summary>
        public string Reason { get; set; }

        /// <summary>
        /// Описание.
        /// </summary>
        public string Description { get; set; }

        #region [ Relationships ]

        /// <summary>
        /// ИД версии проекта.
        /// </summary>
        public Guid ProjectVersionId { get; set; }

        /// <summary>
        /// Версия проекта.
        /// </summary>
        public ProjectVersionEntity ProjectVersion { get; set; }

        /// <summary>
        /// ИД родительской редакции проекта.
        /// </summary>
        public Guid ParentRevisionId { get; set; }

        /// <summary>
        /// Родительская редакция проекта.
        /// </summary>
        public ProjectRevisionEntity ParentRevision { get; set; }

        /// <summary>
        /// ИД версии ArmEdit.
        /// </summary>
        public Guid ArmEditId { get; set; }

        /// <summary>
        /// Версия ArmEdit.
        /// </summary>
        public ArmEditEntity ArmEdit { get; set; }

        /// <summary>
        /// ИД коммуникационного модуля.
        /// </summary>
        public Guid CommunicationId { get; set; }

        /// <summary>
        /// Коммуникационный модуль.
        /// </summary>
        public CommunicationEntity Communication { get; set; }

        /// <summary>
        /// Перечень авторов.
        /// </summary>
        public ICollection<AuthorEntity> Authors { get; set; }

        /// <summary>
        /// Перечень алгоритмов.
        /// </summary>
        public ICollection<RelayAlgorithmEntity> RelayAlgorithms { get; set; }
        #endregion

        /// <summary>
        /// Инициализация экземпляра <see cref="ProjectRevisionEntity"/>.
        /// </summary>
        public ProjectRevisionEntity()
        {
            this.Id = Guid.NewGuid();
            this.Date = DateTime.Now;
            this.Revision = DefaultString.Revision;
            this.Reason = DefaultString.Reason;
            this.Description = DefaultString.Description;
            this.Authors = new HashSet<AuthorEntity>();
            this.RelayAlgorithms = new HashSet<RelayAlgorithmEntity>();
        }

        /// <inheritdoc />
        public Expression<Func<ProjectRevisionEntity, bool>> GetEqualityPredicate()
        {
            return (ProjectRevisionEntity e) => e.Id == this.Id
            || (e.ProjectVersionId == this.ProjectVersionId || e.ProjectVersion.DIVG == this.ProjectVersion.DIVG) && e.Revision == this.Revision;
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            return obj is ProjectRevisionEntity e && (this.Id.Equals(e.Id) || ProjectVersionId.Equals(e.ProjectVersionId) && Revision == e.Revision);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            /*
             * при определении уникальности картежа нужно учитывать и версию проекта к которой он привязан !!!
             * ПС чисто теоретически даты и время компиляции должны отличасться, но так происходит не всегда
             */
            return HashCode.Combine(this.ProjectVersionId, this.Date, this.Revision);
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"ID: {this.Id}, {this.ProjectVersion?.Prefix}-{this.ProjectVersion?.Title}-{this.ProjectVersion?.Version}_{this.Revision}, дата изменения: {this.Date}";
        }
    }
}