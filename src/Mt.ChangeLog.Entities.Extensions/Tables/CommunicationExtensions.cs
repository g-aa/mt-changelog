using Mt.ChangeLog.Entities.Tables;
using Mt.ChangeLog.TransferObjects.Communication;
using Mt.Utilities;
using System.Linq;

namespace Mt.ChangeLog.Entities.Extensions.Tables
{
    /// <summary>
    /// Методы расширения для класса <see cref="Communication"/>.
    /// </summary>
    public static class CommunicationExtensions
    {
        /// <summary>
        /// Преобразовать сущность <see cref="Communication"/> в модель <see cref="CommunicationShortModel"/>.
        /// </summary>
        /// <param name="entity">Сущность.</param>
        /// <returns>Модель.</returns>
        public static CommunicationShortModel ToShortModel(this Communication entity)
        {
            Check.NotNull(entity, nameof(entity));
            var result = new CommunicationShortModel()
            {
                Id = entity.Id,
                Title = entity.Title,
            };
            return result;
        }

        /// <summary>
        /// Преобразовать сущность <see cref="Communication"/> в модель <see cref="CommunicationTableModel"/>.
        /// </summary>
        /// <param name="entity">Сущность.</param>
        /// <returns>Модель.</returns>
        public static CommunicationTableModel ToTableModel(this Communication entity)
        {
            Check.NotNull(entity, nameof(entity));
            var result = new CommunicationTableModel()
            {
                Id = entity.Id,
                Title = entity.Title,
                Description = entity.Description,
                Protocols = entity.Protocols.Any() ? string.Join(", ", entity.Protocols.OrderBy(e => e.Title).Select(e => e.Title)) : ""
            };
            return result;
        }

        /// <summary>
        /// Преобразовать сущность <see cref="Communication"/> в модель <see cref="CommunicationModel"/>.
        /// </summary>
        /// <param name="entity">Сущность.</param>
        /// <returns>Модель.</returns>
        public static CommunicationModel ToModel(this Communication entity)
        {
            Check.NotNull(entity, nameof(entity));
            var result = new CommunicationModel()
            {
                Id = entity.Id,
                Title = entity.Title,
                Protocols = entity.Protocols.OrderBy(e => e.Title).Select(e => e.ToShortModel()),
                Description = entity.Description
            };
            return result;
        }

        /// <summary>
        /// Получить строитель.
        /// </summary>
        /// <param name="entity">Сущность.</param>
        /// <returns>Строитель.</returns>
        public static CommunicationBuilder GetBuilder(this Communication entity)
        {
            return new CommunicationBuilder(entity);
        }
    }
}