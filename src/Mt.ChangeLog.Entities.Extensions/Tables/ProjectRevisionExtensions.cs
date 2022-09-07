using Mt.ChangeLog.Entities.Tables;
using Mt.ChangeLog.TransferObjects.Historical;
using Mt.ChangeLog.TransferObjects.ProjectRevision;
using Mt.Utilities;
using System.Linq;

namespace Mt.ChangeLog.Entities.Extensions.Tables
{
    /// <summary>
    /// Методы расширения для класса <see cref="ProjectRevision"/>.
    /// </summary>
    public static class ProjectRevisionExtensions
    {
        /// <summary>
        /// Преобразовать сущность <see cref="ProjectRevision"/> в модель <see cref="ProjectRevisionShortModel"/>.
        /// </summary>
        /// <param name="entity">Сущность.</param>
        /// <returns>Модель.</returns>
        public static ProjectRevisionShortModel ToShortModel(this ProjectRevision entity)
        {
            Check.NotNull(entity, nameof(entity));
            var result = new ProjectRevisionShortModel()
            {
                Id = entity.Id,
                Prefix = entity.ProjectVersion.Prefix,
                Title = entity.ProjectVersion.Title,
                Version = entity.ProjectVersion.Version,
                Revision = entity.Revision
            };
            return result;
        }

        /// <summary>
        /// Преобразовать сущность <see cref="ProjectRevision"/> в модель <see cref="ProjectRevisionTableModel"/>.
        /// </summary>
        /// <param name="entity">Сущность.</param>
        /// <returns>Модель.</returns>
        public static ProjectRevisionTableModel ToTableModel(this ProjectRevision entity)
        {
            Check.NotNull(entity, nameof(entity));
            var result = new ProjectRevisionTableModel()
            {
                Id = entity.Id,
                Prefix = entity.ProjectVersion.Prefix,
                Title = entity.ProjectVersion.Title,
                Version = entity.ProjectVersion.Version,
                Revision = entity.Revision,
                Date = entity.Date,
                ArmEdit = entity.ArmEdit.Version,
                Reason = entity.Reason
            };
            return result;
        }

        /// <summary>
        /// Преобразовать сущность <see cref="ProjectRevision"/> в модель <see cref="ProjectRevisionTreeModel"/>.
        /// </summary>
        /// <param name="entity">Сущность.</param>
        /// <returns>Модель.</returns>
        public static ProjectRevisionTreeModel ToTreeModel(this ProjectRevision entity)
        {
            Check.NotNull(entity, nameof(entity));
            var result = new ProjectRevisionTreeModel()
            {
                Id = entity.Id,
                ParentId = entity.ParentRevisionId,
                Prefix = entity.ProjectVersion.Prefix,
                Title = entity.ProjectVersion.Title,
                Version = entity.ProjectVersion.Version,
                Revision = entity.Revision,
                Date = entity.Date.ToString("yyyy-MM-dd"),
                ArmEdit = entity.ArmEdit.Version,
                Platform = entity.ProjectVersion.Platform.Title
            };
            return result;
        }

        /// <summary>
        /// Преобразовать сущность <see cref="ProjectRevision"/> в модель <see cref="ProjectRevisionHistoryShortModel"/>.
        /// </summary>
        /// <param name="entity">Сущность.</param>
        /// <returns>Модель.</returns>
        public static ProjectRevisionHistoryShortModel ToHistoryShortModel(this ProjectRevision entity)
        {
            Check.NotNull(entity, nameof(entity));
            var result = new ProjectRevisionHistoryShortModel()
            {
                Id = entity.Id,
                Date = entity.Date,
                Title = $"{entity.ProjectVersion.Prefix}-{entity.ProjectVersion.Title}-{entity.ProjectVersion.Version}_{entity.Revision}",
                Platform = entity.ProjectVersion.Platform.Title
            };
            return result;
        }

        /// <summary>
        /// Преобразовать сущность <see cref="ProjectRevision"/> в модель <see cref="ProjectRevisionHistoryModel"/>.
        /// </summary>
        /// <param name="entity">Сущность.</param>
        /// <returns>Модель.</returns>
        public static ProjectRevisionHistoryModel ToHistoryModel(this ProjectRevision entity)
        {
            Check.NotNull(entity, nameof(entity));
            var result = new ProjectRevisionHistoryModel()
            {
                Id = entity.Id,
                ArmEdit = entity.ArmEdit.Version,
                Authors = entity.Authors.Select(a => $"{a.FirstName} {a.LastName}"),
                RelayAlgorithms = entity.RelayAlgorithms.Select(ra => ra.Title),
                Communication = string.Join(", ", entity.Communication.Protocols.OrderBy(e => e.Title).Select(e => e.Title)),
                Date = entity.Date,
                Description = entity.Description,
                Platform = entity.ProjectVersion.Platform.Title,
                Reason = entity.Reason,
                Title = $"{entity.ProjectVersion.Prefix}-{entity.ProjectVersion.Title}-{entity.ProjectVersion.Version}_{entity.Revision}"
            };
            return result;
        }

        /// <summary>
        /// Преобразовать сущность <see cref="ProjectRevision"/> в модель <see cref="ProjectRevisionModel"/>.
        /// </summary>
        /// <param name="entity">Сущность.</param>
        /// <returns>Модель.</returns>
        public static ProjectRevisionModel ToModel(this ProjectRevision entity)
        {
            Check.NotNull(entity, nameof(entity));
            var result = new ProjectRevisionModel()
            {
                Id = entity.Id,
                Date = entity.Date,
                Revision = entity.Revision,
                Reason = entity.Reason,
                Description = entity.Description,
                ParentRevision = entity.ParentRevision?.ToShortModel(),
                ProjectVersion = entity.ProjectVersion.ToShortModel(),
                ArmEdit = entity.ArmEdit.ToShortModel(),
                Communication = entity.Communication.ToShortModel(),
                Authors = entity.Authors.Select(author => author.ToShortModel()),
                RelayAlgorithms = entity.RelayAlgorithms.Select(alg => alg.ToShortModel()),
            };
            return result;
        }

        /// <summary>
        /// Получить строитель.
        /// </summary>
        /// <param name="entity">Сущность.</param>
        /// <returns>Строитель.</returns>
        public static ProjectRevisionBuilder GetBuilder(this ProjectRevision entity)
        {
            return new ProjectRevisionBuilder(entity);
        }
    }
}