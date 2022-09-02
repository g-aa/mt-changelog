using Mt.Utilities;
using System;

namespace Mt.ChangeLog.TransferObjects.ProjectVersion
{
    /// <summary>
    /// Краткая модель версии проекта.
    /// </summary>
    public class ProjectVersionShortModel
    {
        /// <summary>
        /// ИД.
        /// </summary>
        public Guid Id { get; set; }
        
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
        /// Инициализация экземпляра <see cref="ProjectVersionShortModel"/>
        /// </summary>
        public ProjectVersionShortModel()
        {
            this.Id = Guid.NewGuid();
            this.Prefix = DefaultString.Prefix;
            this.Title = "ПМК";
            this.Version = "00";
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{this.Prefix}-{this.Title}-{this.Version}";
        }
    }
}