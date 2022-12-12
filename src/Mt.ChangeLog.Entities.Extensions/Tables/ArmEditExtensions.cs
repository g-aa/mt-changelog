using Mt.ChangeLog.Entities.Tables;
using Mt.ChangeLog.TransferObjects.ArmEdit;
using Mt.Utilities;

namespace Mt.ChangeLog.Entities.Extensions.Tables
{
    /// <summary>
    /// Методы расширения для класса <see cref="ArmEditEntity"/>.
    /// </summary>
    public static class ArmEditExtensions
    {
        /// <summary>
        /// Преобразовать сущность <see cref="ArmEditEntity"/> в модель <see cref="ArmEditShortModel"/>.
        /// </summary>
        /// <param name="entity">Сущность.</param>
        /// <returns>Модель.</returns>
        public static ArmEditShortModel ToShortModel(this ArmEditEntity entity)
        {
            Check.NotNull(entity, nameof(entity));
            var result = new ArmEditShortModel()
            {
                Id = entity.Id,
                Version = entity.Version
            };
            return result;
        }

        /// <summary>
        /// Преобразовать сущность <see cref="ArmEditEntity"/> в модель <see cref="ArmEditModel"/>.
        /// </summary>
        /// <param name="entity">Сущность.</param>
        /// <returns>Модель.</returns>
        public static ArmEditModel ToModel(this ArmEditEntity entity)
        {
            Check.NotNull(entity, nameof(entity));
            var result = new ArmEditModel()
            {
                Id = entity.Id,
                Date = entity.Date,
                DIVG = entity.DIVG,
                Version = entity.Version,
                Description = entity.Description
            };
            return result;
        }

        /// <summary>
        /// Получить строитель.
        /// </summary>
        /// <param name="entity">Сущность.</param>
        /// <returns>Строитель.</returns>
        public static ArmEditBuilder GetBuilder(this ArmEditEntity entity)
        {
            return new ArmEditBuilder(entity);
        }
    }
}