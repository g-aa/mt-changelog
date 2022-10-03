using Mt.Entities.Abstractions.Interfaces;
using Mt.Utilities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Mt.ChangeLog.Entities.Tables
{
    /// <summary>
    /// Сущность автора.
    /// </summary>
    public class Author : IEntity, IDefaultable, IEqualityPredicate<Author>, IRemovable
    {
        /// <inheritdoc />
        public Guid Id { get; set; }

        /// <summary>
        /// Имя.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Фамилия.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Должность.
        /// </summary>
        public string Position { get; set; }
        
        /// <inheritdoc />
        public bool Default { get; set; }
        
        /// <inheritdoc />
        public bool Removable { get; set; }

        #region [ Relationships ]
        
        /// <summary>
        /// Перечень редакций проектов.
        /// </summary>
        public ICollection<ProjectRevision> ProjectRevisions { get; set; }
        #endregion

        /// <summary>
        /// Инициализация экземпляра <see cref="Author"/>.
        /// </summary>
        public Author()
        {
            this.Id = Guid.NewGuid();
            this.FirstName = DefaultString.FirstName;
            this.LastName = DefaultString.LastName;
            this.Position = DefaultString.Position;
            this.Default = false;
            this.Removable = true;
            this.ProjectRevisions = new HashSet<ProjectRevision>();
        }

        /// <inheritdoc />
        public Expression<Func<Author, bool>> GetEqualityPredicate()
        {
            return (Author e) => e.Id == this.Id || e.FirstName == this.FirstName && e.LastName == this.LastName;
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            return obj is Author e && ( this.Id.Equals(e.Id) || this.FirstName == e.FirstName && this.LastName == e.LastName );
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return HashCode.Combine(this.FirstName, this.LastName);
        }
        
        /// <inheritdoc />
        public override string ToString()
        {
            return $"ID: {this.Id}, {this.LastName} {this.FirstName}";
        }
    }
}