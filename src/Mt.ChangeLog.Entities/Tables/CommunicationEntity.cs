using Mt.Entities.Abstractions.Interfaces;
using Mt.Utilities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Mt.ChangeLog.Entities.Tables
{
    /// <summary>
    /// Сущность коммуникационного модуля.
    /// </summary>
    public class CommunicationEntity : IDefaultable, IEntity, IEqualityPredicate<CommunicationEntity>, IRemovable
    {
        /// <inheritdoc />
        public Guid Id { get; set; }

        /// <summary>
        /// Наименование.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Описание.
        /// </summary>
        public string Description { get; set; }

        /// <inheritdoc />
        public bool Default { get; set; }

        /// <inheritdoc />
        public bool Removable { get; set; }

        #region [ Relationships ]
        
        /// <summary>
        /// Перечень протоколов.
        /// </summary>
        public ICollection<ProtocolEntity> Protocols { get; set; }
        
        /// <summary>
        /// Перечень редакций проектов.
        /// </summary>
        public ICollection<ProjectRevisionEntity> ProjectRevisions { get; set; }
        #endregion

        /// <summary>
        /// Инициализация экземпляра <see cref="CommunicationEntity"/>.
        /// </summary>
        public CommunicationEntity()
        {
            this.Id = Guid.NewGuid();
            this.Title = DefaultString.Communication;
            this.Description = DefaultString.Description;
            this.Default = false;
            this.Removable = true;
            this.Protocols = new HashSet<ProtocolEntity>();
            this.ProjectRevisions = new HashSet<ProjectRevisionEntity>();
        }

        /// <inheritdoc />
        public Expression<Func<CommunicationEntity, bool>> GetEqualityPredicate()
        {
            return (CommunicationEntity e) => e.Id == this.Id || e.Title == this.Title;
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            return obj is CommunicationEntity e && ( this.Id.Equals(e.Id) || this.Title == e.Title );
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return HashCode.Combine(this.Title);
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"ID: {this.Id}, адаптер: {this.Title}";
        }
    }
}