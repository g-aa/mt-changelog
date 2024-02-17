using Mt.ChangeLog.Entities.Views;
using Mt.ChangeLog.TransferObjects.Historical;

namespace Mt.ChangeLog.Logic.Mappers;

/// <summary>
/// Методы расширения для класса <see cref="ProjectHistoryRecordView"/>.
/// </summary>
public static class ProjectHistoryRecordMapper
{
    /// <summary>
    /// Преобразовать сущность <see cref="ProjectHistoryRecordView"/> в краткую модель <see cref="ProjectHistoryRecordShortModel"/>.
    /// </summary>
    /// <param name="record">Запись.</param>
    /// <returns>Модель.</returns>
    public static ProjectHistoryRecordShortModel ToShortModel(this ProjectHistoryRecordView record)
    {
        return new ProjectHistoryRecordShortModel
        {
            ProjectRevisionId = record.ProjectRevisionId,
            Title = record.Title,
            Date = record.Date,
            Platform = record.Platform,
        };
    }

    /// <summary>
    /// Преобразовать сущность <see cref="ProjectHistoryRecordView"/> в модель <see cref="ProjectHistoryRecordModel"/>.
    /// </summary>
    /// <param name="record">Запись.</param>
    /// <returns>Модель.</returns>
    public static ProjectHistoryRecordModel ToModel(this ProjectHistoryRecordView record)
    {
        return new ProjectHistoryRecordModel
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
            Description = record.Description,
        };
    }
}