using Mt.Entities.Abstractions.Interfaces;
using Mt.Utilities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Mt.ChangeLog.Entities.Tables
{
    /// <summary>
    /// Сущность платформы БМРЗ.
    /// </summary>
    public class PlatformEntity : IDefaultable, IEntity, IEqualityPredicate<PlatformEntity>, IRemovable
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
        /// Перечень аналоговых модулей.
        /// </summary>
        public ICollection<AnalogModuleEntity> AnalogModules { get; set; }
        
        /// <summary>
        /// Перечень версий проектов.
        /// </summary>
        public ICollection<ProjectVersionEntity> Projects { get; set; }
        #endregion

        /// <summary>
        /// Инициализация экземпляра <see cref="PlatformEntity"/>.
        /// </summary>
        public PlatformEntity()
        {
            this.Id = Guid.NewGuid();
            this.Title = DefaultString.Platform;
            this.Description = DefaultString.Description;
            this.Default = false;
            this.Removable = true;
            this.AnalogModules = new HashSet<AnalogModuleEntity>();
            this.Projects = new HashSet<ProjectVersionEntity>();
        }

        /// <inheritdoc />
        public Expression<Func<PlatformEntity, bool>> GetEqualityPredicate()
        {
            return (PlatformEntity e) => e.Id == this.Id || e.Title == this.Title;
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            return obj is PlatformEntity platform && (Id.Equals(platform.Id) || Title == platform.Title);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return HashCode.Combine(this.Title);
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"ID: {this.Id}, платформа: {this.Title}";
        }
    }
}