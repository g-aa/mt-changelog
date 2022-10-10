using Mt.ChangeLog.Entities.Tables;
using Mt.ChangeLog.TransferObjects.ProjectVersion;
using Mt.Utilities;
using System;

namespace Mt.ChangeLog.Entities.Extensions.Tables
{
    /// <summary>
    /// Строитель <see cref="ProjectVersion"/>.
    /// </summary>
    public class ProjectVersionBuilder
    {
        private readonly ProjectVersion entity;

        private string divg;
        private string prefix;
        private string title;
        private string version;
        private string description;
        private Platform platform;
        private AnalogModule module;
        private ProjectStatus status;

        /// <summary>
        /// Инициализация экземпляра класса <see cref="ProjectVersionBuilder"/>.
        /// </summary>
        /// <param name="entity">Сущность.</param>
        /// <exception cref="ArgumentNullException">Срабатывает если entity равно null.</exception>
        public ProjectVersionBuilder(ProjectVersion entity) 
        {
            this.entity = Check.NotNull(entity, nameof(entity));
            this.divg = entity.DIVG;
            this.prefix = entity.Prefix;
            this.title = entity.Title;
            this.version = entity.Version;
            this.description = entity.Description;
            this.platform = entity.Platform;
            this.module = entity.AnalogModule;
            this.status = entity.ProjectStatus;
        }

        /// <summary>
        /// Добавить атрибуты.
        /// </summary>
        /// <param name="model">Модель.</param>
        /// <returns>Строитель.</returns>
        /// <exception cref="ArgumentNullException">Срабатывает если model равно null.</exception>
        public ProjectVersionBuilder SetAttributes(ProjectVersionModel model)
        {
            Check.NotNull(model, nameof(model));
            this.divg = model.DIVG;
            this.prefix = model.Prefix;
            this.title = model.Title;
            this.version = model.Version;
            this.description = model.Description;
            return this;
        }

        /// <summary>
        /// Добавить платформу.
        /// </summary>
        /// <param name="platform">Платформа.</param>
        /// <returns>Строитель.</returns>
        /// <exception cref="ArgumentNullException">Срабатывает если platform равно null.</exception>
        public ProjectVersionBuilder SetPlatform(Platform platform) 
        {
            this.platform = Check.NotNull(platform, nameof(platform));
            return this;
        }

        /// <summary>
        /// Добавить платформу.
        /// </summary>
        /// <param name="module">Модуль.</param>
        /// <returns>Строитель.</returns>
        /// <exception cref="ArgumentNullException">Срабатывает если module равно null.</exception>
        public ProjectVersionBuilder SetAnalogModule(AnalogModule module) 
        {
            this.module = Check.NotNull(module, nameof(module));
            return this;
        }

        /// <summary>
        /// Добавить платформу.
        /// </summary>
        /// <param name="status">Статус.</param>
        /// <returns>Строитель.</returns>
        /// <exception cref="ArgumentNullException">Срабатывает если status равно null.</exception>
        public ProjectVersionBuilder SetProjectStatus(ProjectStatus status) 
        {
            this.status = Check.NotNull(status, nameof(status));
            return this;
        }

        /// <summary>
        /// Построить сущность.
        /// </summary>
        /// <returns>Сущность.</returns>
        public ProjectVersion Build()
        {
            // атрибуты:
            // this.entity.Id - не обновляется!
            this.entity.DIVG = divg;
            this.entity.Prefix = string.IsNullOrEmpty(this.prefix) ? this.module.Title.Replace("БМРЗ", "БФПО") : this.prefix;
            this.entity.Title = title;
            this.entity.Version = version;
            this.entity.Description = description;
            // реляционные связи:
            this.entity.AnalogModule = module;
            this.entity.Platform = platform;
            this.entity.ProjectStatus = status;
            // this.entity.ProjectRevisions - не обновляется!
            return this.entity;
        }

        /// <summary>
        /// Получить строитель.
        /// </summary>
        /// <returns>Строитель.</returns>
        public static ProjectVersionBuilder GetBuilder() 
        {
            return new ProjectVersionBuilder(new ProjectVersion());
        }
    }
}