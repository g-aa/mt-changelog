using Mt.ChangeLog.Entities.Tables;
using Mt.ChangeLog.TransferObjects.Platform;
using Mt.Utilities;
using System;
using System.Linq;

namespace Mt.ChangeLog.Entities.Extensions.Tables
{
    /// <summary>
    /// Строитель <see cref="Platform"/>.
    /// </summary>
    public sealed class PlatformBuilder
    {
        private readonly Platform entity;

        private string title;
        private string description;
        private IQueryable<AnalogModule> modules;

        /// <summary>
        /// Инициализация экземпляра класса <see cref="PlatformBuilder"/>.
        /// </summary>
        /// <param name="entity">Сущность.</param>
        /// <exception cref="ArgumentNullException">Срабатывает если entity равно null.</exception>
        public PlatformBuilder(Platform entity) 
        {
            this.entity = Check.NotNull(entity, nameof(entity));
            this.title = entity.Title;
            this.description = entity.Description;
            this.modules = entity.AnalogModules.AsQueryable();
        }

        /// <summary>
        /// Добавить атрибуты.
        /// </summary>
        /// <param name="model">Модель.</param>
        /// <returns>Строитель.</returns>
        /// <exception cref="ArgumentNullException">Срабатывает если model равно null.</exception>
        public PlatformBuilder SetAttributes(PlatformModel model)
        {
            Check.NotNull(model, nameof(model));
            this.title = model.Title;
            this.description = model.Description;
            return this;
        }

        /// <summary>
        /// Добавить аналоговые модули.
        /// </summary>
        /// <param name="modules">Перечень аналоговых модулей.</param>
        /// <returns>Строитель.</returns>
        /// <exception cref="ArgumentNullException">Срабатывает если modules равно null.</exception>
        public PlatformBuilder SetAnalogModules(IQueryable<AnalogModule> modules)
        {
            this.modules = Check.NotNull(modules, nameof(modules));
            return this;
        }

        /// <summary>
        /// Построить сущность.
        /// </summary>
        /// <returns>Сущность.</returns>
        /// <exception cref="ArgumentException">Ошибка в логике обработки связей.</exception>
        public Platform Build()
        { 
            var prohibModules = this.entity.AnalogModules.Except(modules).Where(e => e.Projects.Intersect(this.entity.Projects).Any()).Select(e => e.Title);
            if (prohibModules.Any())
            {
                throw new ArgumentException($"Следующие аналоговые модули: \"{string.Join(",", prohibModules)}\" используются в проектах (БФПО) и не могут быть исключены из состава программных платформ \"{this}\"");
            }
            // атрибуты:
            // this.entity.Id - не обновляется!
            this.entity.Title = this.title;
            this.entity.Description = this.description;
            // реляционные связи:
            this.entity.AnalogModules = modules.ToHashSet();
            // this.entity.Projects - не обновляется!
            return this.entity;
        }

        /// <summary>
        /// Получить строитель.
        /// </summary>
        /// <returns>Строитель.</returns>
        public static PlatformBuilder GetBuilder()
        {
            return new PlatformBuilder(new Platform());
        }
    }
}