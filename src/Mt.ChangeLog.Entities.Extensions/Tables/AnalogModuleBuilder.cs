using Mt.ChangeLog.Entities.Tables;
using Mt.ChangeLog.TransferObjects.AnalogModule;
using System;
using System.Linq;

namespace Mt.ChangeLog.Entities.Extensions.Tables
{
    /// <summary>
    /// Строитель <see cref="AnalogModule"/>.
    /// </summary>
    public sealed class AnalogModuleBuilder
    {
        private readonly AnalogModule entity;

        private string divg;
        private string title;
        private string current;
        private string description;
        private IQueryable<Platform> platforms;

        /// <summary>
        /// Инициализация экземпляра класса <see cref="AnalogModuleBuilder"/>.
        /// </summary>
        /// <param name="entity">Сущность.</param>
        public AnalogModuleBuilder(AnalogModule entity) 
        {
            this.entity = entity;
        }

        /// <summary>
        /// Добавить атрибуты.
        /// </summary>
        /// <param name="model"Модель.</param>
        /// <returns>Строитель.</returns>
        public AnalogModuleBuilder SetAttributes(AnalogModuleModel model) 
        {
            this.divg = model?.DIVG;
            this.title = model?.Title;
            this.current = model?.Current;
            this.description = model?.Description;
            return this;
        }

        /// <summary>
        /// Добавить платформы.
        /// </summary>
        /// <param name="platforms">Перечень платформ.</param>
        /// <returns>Строитель.</returns>
        public AnalogModuleBuilder SetPlatforms(IQueryable<Platform> platforms) 
        {
            this.platforms = platforms;
            return this;
        }

        /// <summary>
        /// Построить сущность.
        /// </summary>
        /// <returns>Сущность.</returns>
        /// <exception cref="ArgumentException">Ошибка в логике обработки связей.</exception>
        public AnalogModule Build()
        {
            var prohibPlatforms = this.entity.Platforms.Except(this.platforms).Where(e => e.Projects.Intersect(this.entity.Projects).Any()).Select(e => e.Title);
            if (prohibPlatforms.Any())
            {
                throw new ArgumentException($"Следующие платформы: \"{string.Join(", ", prohibPlatforms)}\" используются в проектах (БФПО) и не могут быть исключены из состава аналогового модуля \"{this.entity}\"");
            }
            // атрибуты:
            // this.entity.Id - не обновляется!
            this.entity.DIVG = this.divg;
            this.entity.Title = this.title;
            this.entity.Current = this.current;
            this.entity.Description = this.description;
            // реляционные связи:
            this.entity.Platforms = platforms.ToHashSet();
            // this.entity.ProjectVersion - не обновляется!
            return this.entity;
        }

        /// <summary>
        /// Получить строитель.
        /// </summary>
        /// <returns>Строитель.</returns>
        public static AnalogModuleBuilder GetBuilder() 
        {
            return new AnalogModuleBuilder(new AnalogModule());
        }
    }
}