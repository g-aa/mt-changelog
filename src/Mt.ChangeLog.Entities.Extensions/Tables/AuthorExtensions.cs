using Mt.ChangeLog.Entities.Tables;
using Mt.ChangeLog.TransferObjects.Author;
using Mt.Utilities;

namespace Mt.ChangeLog.Entities.Extensions.Tables
{
    /// <summary>
    /// Методы расширения для класса <see cref="Author"/>.
    /// </summary>
    public static class AuthorExtensions
    {
        /// <summary>
        /// Преобразовать сущность <see cref="Author"/> в модель <see cref="AuthorShortModel"/>.
        /// </summary>
        /// <param name="entity">Сущность.</param>
        /// <returns>Модель.</returns>
        public static AuthorShortModel ToShortModel(this Author entity)
        {
            Check.NotNull(entity, nameof(entity));
            var result = new AuthorShortModel()
            {
                Id = entity.Id,
                FirstName = entity.FirstName,
                LastName = entity.LastName
            };
            return result;
        }

        /// <summary>
        /// Преобразовать сущность <see cref="Author"/> в модель <see cref="AuthorModel"/>.
        /// </summary>
        /// <param name="entity">Сущность.</param>
        /// <returns>Модель.</returns>
        public static AuthorModel ToModel(this Author entity)
        {
            Check.NotNull(entity, nameof(entity));
            var result = new AuthorModel()
            {
                Id = entity.Id,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Position = entity.Position
            };
            return result;
        }

        /// <summary>
        /// Получить строитель.
        /// </summary>
        /// <param name="entity">Сущность.</param>
        /// <returns>Строитель.</returns>
        public static AuthorBuilder GetBuilder(this Author entity)
        {
            return new AuthorBuilder(entity);
        }
    }
}