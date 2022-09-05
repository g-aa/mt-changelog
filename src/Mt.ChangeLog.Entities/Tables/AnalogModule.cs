using Mt.Entities.Abstractions.Interfaces;
using Mt.Utilities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Mt.ChangeLog.Entities.Tables
{
    /// <summary>
    /// Сущность аналогового модуля.
    /// </summary>
    public class AnalogModule : IDefaultable, IEntity, IEqualityPredicate<AnalogModule>, IRemovable
    {
        /// <inheritdoc />
        public Guid Id { get; set; }

        /// <summary>
        /// ДИВГ.
        /// </summary>
        public string DIVG { get; set; }

        /// <summary>
        /// Наименование.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Номинальный ток.
        /// </summary>
        public string Current { get; set; }

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
        /// Перечень версий проектов.
        /// </summary>
        public ICollection<ProjectVersion> Projects { get; set; }

        /// <summary>
        /// Перечень платформ.
        /// </summary>
        public ICollection<Platform> Platforms { get; set; }
        #endregion

        /// <summary>
        /// Инициализация экземпляра <see cref="AnalogModule"/>.
        /// </summary>
        public AnalogModule()
        {
            this.Id = Guid.NewGuid();
            this.DIVG = DefaultString.DIVG;
            this.Title = DefaultString.AnalogModule;
            this.Current = DefaultString.Current;
            this.Description = DefaultString.Description;
            this.Default = false;
            this.Removable = true;
            this.Projects = new HashSet<ProjectVersion>();
            this.Platforms = new HashSet<Platform>();
        }

        /// <inheritdoc />
        public Expression<Func<AnalogModule, bool>> GetEqualityPredicate()
        {
            /*
             * пока нет данных по ДИВГ-ам
             * return (AnalogModule e) => e.Id == this.Id || e.DIVG == this.DIVG || e.Title == this.Title;
             */
            return (AnalogModule e) => e.Id == this.Id || e.Title == this.Title;
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return HashCode.Combine(this.DIVG, this.Title, this.Current);
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"ID: {this.Id}, модуль: {this.Title ?? ""}";
        }
    }
}