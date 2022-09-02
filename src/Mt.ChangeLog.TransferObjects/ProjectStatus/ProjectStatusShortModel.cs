using Mt.Utilities;
using System;

namespace Mt.ChangeLog.TransferObjects.ProjectStatus
{
    /// <summary>
    /// Краткая модель статуса проекта.
    /// </summary>
    public class ProjectStatusShortModel
    {
        /// <summary>
        /// ИД.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Наименование.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Инициализация экземпляра <see cref="ProjectStatusShortModel"/>
        /// </summary>
        public ProjectStatusShortModel()
        {
            this.Id = Guid.NewGuid();
            this.Title = DefaultString.Status;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return this.Title;
        }
    }
}