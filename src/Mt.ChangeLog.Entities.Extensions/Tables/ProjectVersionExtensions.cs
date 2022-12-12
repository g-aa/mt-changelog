using Mt.ChangeLog.Entities.Tables;
using Mt.ChangeLog.TransferObjects.ProjectVersion;
using Mt.Utilities;

namespace Mt.ChangeLog.Entities.Extensions.Tables
{
    /// <summary>
    /// Методы расширения для класса <see cref="ProjectVersionEntity"/>.
    /// </summary>
    public static class ProjectVersionExtensions
    {
        /// <summary>
        /// Преобразовать сущность <see cref="ProjectVersionEntity"/> в модель <see cref="ProjectVersionShortModel"/>.
        /// </summary>
        /// <param name="entity">Сущность.</param>
        /// <returns>Модель.</returns>
        public static ProjectVersionShortModel ToShortModel(this ProjectVersionEntity entity)
        {
            Check.NotNull(entity, nameof(entity));
            var result = new ProjectVersionShortModel()
            {
                Id = entity.Id,
                Prefix = entity.Prefix,
                Title = entity.Title,
                Version = entity.Version
            };
            return result;
        }

        /// <summary>
        /// Преобразовать сущность <see cref="ProjectVersionEntity"/> в модель <see cref="ProjectVersionTableModel"/>.
        /// </summary>
        /// <param name="entity">Сущность.</param>
        /// <returns>Модель.</returns>
        public static ProjectVersionTableModel ToTableModel(this ProjectVersionEntity entity)
        {
            Check.NotNull(entity, nameof(entity));
            var result = new ProjectVersionTableModel()
            {
                Id = entity.Id,
                DIVG = entity.DIVG,
                Prefix = entity.Prefix,
                Title = entity.Title,
                Status = entity.ProjectStatus.Title,
                Version = entity.Version,
                Description = entity.Description,
                Module = entity.AnalogModule.Title,
                Platform = entity.Platform.Title
            };
            return result;
        }

        /// <summary>
        /// Преобразовать сущность <see cref="ProjectVersionEntity"/> в модель <see cref="ProjectVersionModel"/>.
        /// </summary>
        /// <param name="entity">Сущность.</param>
        /// <returns>Модель.</returns>
        public static ProjectVersionModel ToModel(this ProjectVersionEntity entity)
        {
            Check.NotNull(entity, nameof(entity));
            var result = new ProjectVersionModel()
            {
                Id = entity.Id,
                DIVG = entity.DIVG,
                Prefix = entity.Prefix,
                Title = entity.Title,
                ProjectStatus = entity.ProjectStatus.ToShortModel(),
                Version = entity.Version,
                Description = entity.Description,
                AnalogModule = entity.AnalogModule.ToShortModel(),
                Platform = entity.Platform.ToShortModel()
            };
            return result;
        }

        /// <summary>
        /// Получить строитель.
        /// </summary>
        /// <returns>Строитель.</returns>
        public static ProjectVersionBuilder GetBuilder(this ProjectVersionEntity project)
        {
            return new ProjectVersionBuilder(project);
        }
    }
}