using Mt.ChangeLog.Entities.Views;
using Mt.ChangeLog.TransferObjects.Historical;
using Mt.ChangeLog.TransferObjects.ProjectRevision;
using Mt.ChangeLog.TransferObjects.ProjectVersion;
using Mt.Utilities;

namespace Mt.ChangeLog.Entities.Extensions.Views
{
    /// <summary>
    /// Методы расширения для класса <see cref="LastProjectRevision"/>.
    /// </summary>
    public static class LastProjectRevisionExtensions
    {
        /// <summary>
        /// Преобразовать сущность <see cref="LastProjectRevision"/> в модель <see cref="LastProjectRevisionModel"/>.
        /// </summary>
        /// <param name="entity">Сущность.</param>
        /// <returns>Модель.</returns>
        public static LastProjectRevisionModel ToModel(this LastProjectRevision entity)
        {
            Check.NotNull(entity, nameof(entity));
            var result = new LastProjectRevisionModel()
            {
                Prefix = entity.Prefix,
                Title = entity.Title,
                Version = entity.Version,
                Revision = entity.Revision,
                Platform = entity.Platform,
                ArmEdit = entity.ArmEdit,
                Date = entity.Date
            };
            return result;
        }

        /// <summary>
        /// Преобразовать сущность <see cref="LastProjectRevision"/> в краткую модель истории редакции <see cref="ProjectRevisionHistoryShortModel"/>.
        /// </summary>
        /// <param name="entity">Сущность.</param>
        /// <returns>Модель.</returns>
        public static ProjectRevisionHistoryShortModel ToHistoryShortModel(this LastProjectRevision entity)
        {
            Check.NotNull(entity, nameof(entity));
            var result = new ProjectRevisionHistoryShortModel()
            {
                Id = entity.ProjectRevisionId,
                Date = entity.Date,
                Platform = entity.Platform,
                Title = entity.ToString()
            };
            return result;
        }

        /// <summary>
        /// Преобразовать сущность <see cref="LastProjectRevision"/> в краткую модель истории версии <see cref="ProjectVersionShortModel"/>.
        /// </summary>
        /// <param name="entity">Сущность.</param>
        /// <returns>Модель.</returns>
        public static ProjectVersionShortModel ToProjectVersionShortModel(this LastProjectRevision entity)
        {
            Check.NotNull(entity, nameof(entity));
            var result = new ProjectVersionShortModel()
            {
                Id = entity.ProjectVersionId,
                Prefix = entity.Prefix,
                Title = entity.Title,
                Version = entity.Version
            };
            return result;
        }
    }
}