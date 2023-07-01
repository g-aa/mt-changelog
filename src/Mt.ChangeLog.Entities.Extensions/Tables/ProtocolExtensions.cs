using Mt.ChangeLog.Entities.Tables;
using Mt.ChangeLog.TransferObjects.Protocol;
using Mt.Utilities;

namespace Mt.ChangeLog.Entities.Extensions.Tables
{
    /// <summary>
    /// Методы расширения для класса <see cref="ProtocolEntity"/>.
    /// </summary>
    public static class ProtocolExtensions
    {
        /// <summary>
        /// Преобразовать сущность <see cref="ProtocolEntity"/> в модель <see cref="ProtocolShortModel"/>.
        /// </summary>
        /// <param name="entity">Сущность.</param>
        /// <returns>Модель.</returns>
        public static ProtocolShortModel ToShortModel(this ProtocolEntity entity)
        {
            Check.NotNull(entity, nameof(entity));
            var result = new ProtocolShortModel()
            {
                Id = entity.Id,
                Title = entity.Title,
            };
            return result;
        }

        /// <summary>
        /// Преобразовать сущность <see cref="ProtocolEntity"/> в модель <see cref="ProtocolTableModel"/>.
        /// </summary>
        /// <param name="entity">Сущность.</param>
        /// <returns>Модель.</returns>
        public static ProtocolTableModel ToTableModel(this ProtocolEntity entity)
        {
            Check.NotNull(entity, nameof(entity));
            var result = new ProtocolTableModel()
            {
                Id = entity.Id,
                Title = entity.Title,
                Description = entity.Description,
            };
            return result;
        }

        /// <summary>
        /// Преобразовать сущность <see cref="ProtocolEntity"/> в модель <see cref="ProtocolModel"/>.
        /// </summary>
        /// <param name="entity">Сущность.</param>
        /// <returns>Модель.</returns>
        public static ProtocolModel ToModel(this ProtocolEntity entity)
        {
            Check.NotNull(entity, nameof(entity));
            var result = new ProtocolModel()
            {
                Id = entity.Id,
                Title = entity.Title,
                Description = entity.Description,
                Communications = entity.Communications.OrderBy(e => e.Title).Select(e => e.ToShortModel()),
            };
            return result;
        }

        /// <summary>
        /// Получить строитель.
        /// </summary>
        /// <param name="entity">Сущность.</param>
        /// <returns>Строитель.</returns>
        public static ProtocolBuilder GetBuilder(this ProtocolEntity entity)
        {
            return new ProtocolBuilder(entity);
        }
    }
}