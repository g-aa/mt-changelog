using Mt.ChangeLog.Entities.Views;
using Mt.ChangeLog.TransferObjects.Historical;
using Mt.Utilities;

namespace Mt.ChangeLog.Entities.Extensions.Views
{
    /// <summary>
    /// Методы расширения для класса <see cref="ProjectHistoryRecordView"/>.
    /// </summary>
    public static class ProjectHistoryRecordExtensions
    {
        /// <summary>
        /// Преобразовать сущность <see cref="ProjectHistoryRecordView"/> в краткую модель <see cref="ProjectHistoryRecordShortModel"/>.
        /// </summary>
        /// <param name="record">Запись.</param>
        /// <returns>Модель.</returns>
        public static ProjectHistoryRecordShortModel ToShortModel(this ProjectHistoryRecordView record)
        {
            Check.NotNull(record, nameof(record));
            var result = new ProjectHistoryRecordShortModel()
            {
                ProjectRevisionId = record.ProjectRevisionId,
                Title = record.Title,
                Date = record.Date,
                Platform = record.Platform
            };
            return result;
        }

        /// <summary>
        /// Преобразовать сущность <see cref="ProjectHistoryRecordView"/> в модель <see cref="ProjectHistoryRecordModel"/>.
        /// </summary>
        /// <param name="record">Запись.</param>
        /// <returns>Модель.</returns>
        public static ProjectHistoryRecordModel ToModel(this ProjectHistoryRecordView record)
        {
            Check.NotNull(record, nameof(record));
            var result = new ProjectHistoryRecordModel()
            {
                ProjectRevisionId = record.ProjectRevisionId,
                ParentRevisionId = record.ParentRevisionId,
                ProjectVersionId = record.ProjectVersionId,
                Title = record.Title,
                Date = record.Date,
                Platform = record.Platform,
                ArmEdit = record.ArmEdit,
                Authors = record.Authors,
                Protocols = record.Protocols,
                Algorithms = record.Algorithms,
                Reason = record.Reason,
                Description = record.Description
            };
            return result;
        }
    }
}