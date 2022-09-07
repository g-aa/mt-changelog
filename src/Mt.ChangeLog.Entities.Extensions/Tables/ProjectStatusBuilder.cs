using Mt.ChangeLog.Entities.Tables;
using Mt.ChangeLog.TransferObjects.ProjectStatus;
using Mt.Utilities;
using System;

namespace Mt.ChangeLog.Entities.Extensions.Tables
{
    /// <summary>
    /// Строитель <see cref="ProjectStatus"/>.
    /// </summary>
    public class ProjectStatusBuilder
    {
        private readonly ProjectStatus entity;

        private string title;
        private string description;

        /// <summary>
        /// Инициализация экземпляра класса <see cref="ProjectStatusBuilder"/>.
        /// </summary>
        /// <param name="entity">Сущность.</param>
        /// <exception cref="ArgumentNullException">Срабатывает если entity равно null.</exception>
        public ProjectStatusBuilder(ProjectStatus entity) 
        {
            this.entity = Check.NotNull(entity, nameof(entity));
            this.title = entity.Title;
            this.description = entity.Description;
        }

        /// <summary>
        /// Добавить атрибуты.
        /// </summary>
        /// <param name="model">Модель.</param>
        /// <returns>Строитель.</returns>
        /// <exception cref="ArgumentNullException">Срабатывает если model равно null.</exception>
        public ProjectStatusBuilder SetAttributes(ProjectStatusModel model)
        {
            Check.NotNull(model, nameof(model));
            this.title = model.Title;
            this.description = model.Description;
            return this;
        }

        /// <summary>
        /// Построить сущность.
        /// </summary>
        /// <returns>Сущность.</returns>
        public ProjectStatus Build()
        {
            // атрибуты:
            //this.entity.Id - не обновляется!
            this.entity.Title = title;
            this.entity.Description = description;
            // реляционные связи:
            //this.entity.ProjectVersions - не обновляется!
            return entity;
        }

        /// <summary>
        /// Получить строитель.
        /// </summary>
        /// <returns>Строитель.</returns>
        public static ProjectStatusBuilder GetBuilder()
        {
            return new ProjectStatusBuilder(new ProjectStatus());
        }
    }
}