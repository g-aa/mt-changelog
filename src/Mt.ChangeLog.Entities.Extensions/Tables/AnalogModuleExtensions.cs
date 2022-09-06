using Mt.ChangeLog.Entities.Tables;
using Mt.ChangeLog.TransferObjects.AnalogModule;
using Mt.Utilities;
using System.Linq;

namespace Mt.ChangeLog.Entities.Extensions.Tables
{
    /// <summary>
    /// Методы расширения для класса <see cref="AnalogModule"/>.
    /// </summary>
    public static class AnalogModuleExtensions
    {
        /// <summary>
        /// Преобразовать сущность <see cref="AnalogModule"/> в модель <see cref="AnalogModuleShortModel"/>.
        /// </summary>
        /// <param name="entity">Сущность.</param>
        /// <returns>Модель.</returns>
        public static AnalogModuleShortModel ToShortModel(this AnalogModule entity) 
        {
            Check.NotNull(entity, nameof(entity));
            var result = new AnalogModuleShortModel()
            {
                Id = entity.Id,
                Title = entity.Title
            };
            return result;
        }

        /// <summary>
        /// Преобразовать сущность <see cref="AnalogModule"/> в модель <see cref="AnalogModuleTableModel"/>.
        /// </summary>
        /// <param name="entity">Сущность.</param>
        /// <returns>Модель.</returns>
        public static AnalogModuleTableModel ToTableModel(this AnalogModule entity)
        {
            Check.NotNull(entity, nameof(entity));
            var result = new AnalogModuleTableModel()
            {
                Id = entity.Id,
                Title = entity.Title,
                Current = entity.Current,
                DIVG = entity.DIVG,
                Description = entity.Description
            };
            return result;
        }

        /// <summary>
        /// Преобразовать сущность <see cref="AnalogModule"/> в модель <see cref="AnalogModuleModel"/>.
        /// </summary>
        /// <param name="entity">Сущность.</param>
        /// <returns>Модель.</returns>
        public static AnalogModuleModel ToModel(this AnalogModule entity)
        {
            Check.NotNull(entity, nameof(entity));
            var result = new AnalogModuleModel()
            {
                Id = entity.Id,
                Title = entity.Title,
                DIVG = entity.DIVG,
                Current = entity.Current,
                Description = entity.Description,
                Platforms = Check.NotNull(entity.Platforms, nameof(entity.Platforms)).Select(platforms => platforms.ToShortModel())
            };
            return result;
        }

        /// <summary>
        /// Получить строитель.
        /// </summary>
        /// <param name="entity">Сущность.</param>
        /// <returns>Строитель.</returns>
        public static AnalogModuleBuilder GetBuilder(this AnalogModule entity)
        {
            return new AnalogModuleBuilder(Check.NotNull(entity, nameof(entity)));
        }
    }
}