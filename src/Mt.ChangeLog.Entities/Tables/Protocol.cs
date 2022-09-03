using Mt.Entities.Abstractions.Interfaces;
using Mt.Utilities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Mt.ChangeLog.Entities.Tables
{
    /// <summary>
    /// Сущность протокола информационнго обмена.
    /// </summary>
    public class Protocol : IDefaultable, IEntity, IEqualityPredicate<Protocol>, IRemovable
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
        /// Перечень коммуникационных модулей.
        /// </summary>
        public ICollection<Communication> Communications { get; set; }
        #endregion

        /// <summary>
        /// Инициализация экземпляра <see cref="Protocol"/>
        /// </summary>
        public Protocol() 
        {
            this.Id = Guid.NewGuid();
            this.Title = DefaultString.Protocol;
            this.Description = DefaultString.Description;
            this.Default = false;
            this.Removable = true;
            this.Communications = new HashSet<Communication>();
        }

        /// <inheritdoc />
        public Expression<Func<Protocol, bool>> GetEqualityPredicate()
        {
            return (Protocol e) => e.Id == this.Id || e.Title == this.Title;
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return HashCode.Combine(this.Title);
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"ID: {this.Id}, протокол: {this.Title}";
        }
    }
}