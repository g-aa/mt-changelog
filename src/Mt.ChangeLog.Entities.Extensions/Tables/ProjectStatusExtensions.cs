using Mt.ChangeLog.Entities.Tables;
using Mt.ChangeLog.TransferObjects.ProjectStatus;
using Mt.Utilities;

namespace Mt.ChangeLog.Entities.Extensions.Tables
{
    /// <summary>
    /// Методы расширения для класса <see cref="ProjectStatus"/>.
    /// </summary>
    public static class ProjectStatusExtensions
    {
        /// <summary>
        /// Преобразовать сущность <see cref="ProjectStatus"/> в модель <see cref="ProjectStatusShortModel"/>.
        /// </summary>
        /// <param name="entity">Сущность.</param>
        /// <returns>Модель.</returns>
        public static ProjectStatusShortModel ToShortModel(this ProjectStatus entity)
        {
            Check.NotNull(entity, nameof(entity));
            var result = new ProjectStatusShortModel()
            {
                Id = entity.Id,
                Title = entity.Title
            };
            return result;
        }

        /// <summary>
        /// Преобразовать сущность <see cref="ProjectStatus"/> в модель <see cref="ProjectStatusModel"/>.
        /// </summary>
        /// <param name="entity">Сущность.</param>
        /// <returns>Модель.</returns>
        public static ProjectStatusModel ToModel(this ProjectStatus entity)
        {
            Check.NotNull(entity, nameof(entity));
            var result = new ProjectStatusModel()
            {
                Id = entity.Id,
                Title = entity.Title,
                Description = entity.Description
            };
            return result;
        }

        /// <summary>
        /// Получить строитель.
        /// </summary>
        /// <param name="entity">Сущность.</param>
        /// <returns>Строитель.</returns>
        public static ProjectStatusBuilder GetBuilder(this ProjectStatus entity)
        {
            return new ProjectStatusBuilder(entity);
        }
    }
}