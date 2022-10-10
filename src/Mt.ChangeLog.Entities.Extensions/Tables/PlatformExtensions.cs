using Mt.ChangeLog.Entities.Tables;
using Mt.ChangeLog.TransferObjects.Platform;
using Mt.Utilities;
using System.Linq;

namespace Mt.ChangeLog.Entities.Extensions.Tables
{
    /// <summary>
    /// Методы расширения для класса <see cref="Platform"/>.
    /// </summary>
    public static class PlatformExtensions
    {
        /// <summary>
        /// Преобразовать сущность <see cref="Platform"/> в модель <see cref="PlatformShortModel"/>.
        /// </summary>
        /// <param name="entity">Сущность.</param>
        /// <returns>Модель.</returns>
        public static PlatformShortModel ToShortModel(this Platform entity)
        {
            Check.NotNull(entity, nameof(entity));
            var result = new PlatformShortModel()
            {
                Id = entity.Id,
                Title = entity.Title
            };
            return result;
        }

        /// <summary>
        /// Преобразовать сущность <see cref="Platform"/> в модель <see cref="PlatformTableModel"/>.
        /// </summary>
        /// <param name="entity">Сущность.</param>
        /// <returns>Модель.</returns>
        public static PlatformTableModel ToTableModel(this Platform entity)
        {
            Check.NotNull(entity, nameof(entity));
            var result = new PlatformTableModel()
            {
                Id = entity.Id,
                Title = entity.Title,
                Description = entity.Description
            };
            return result;
        }

        /// <summary>
        /// Преобразовать сущность <see cref="Platform"/> в модель <see cref="PlatformModel"/>.
        /// </summary>
        /// <param name="entity">Сущность.</param>
        /// <returns>Модель.</returns>
        public static PlatformModel ToModel(this Platform entity)
        {
            Check.NotNull(entity, nameof(entity));
            var result = new PlatformModel()
            {
                Id = entity.Id,
                Title = entity.Title,
                Description = entity.Description,
                AnalogModules = Check.NotNull(entity.AnalogModules, nameof(entity.AnalogModules)).Select(module => module.ToShortModel())
            };
            return result;
        }

        /// <summary>
        /// Получить строитель.
        /// </summary>
        /// <param name="entity">Сущность.</param>
        /// <returns>Строитель.</returns>
        public static PlatformBuilder GetBuilder(this Platform entity)
        {
            return new PlatformBuilder(entity);
        }
    }
}