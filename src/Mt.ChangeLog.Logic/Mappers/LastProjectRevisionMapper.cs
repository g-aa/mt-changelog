using Mt.ChangeLog.Entities.Views;
using Mt.ChangeLog.TransferObjects.Historical;
using Mt.ChangeLog.TransferObjects.ProjectRevision;
using Mt.ChangeLog.TransferObjects.ProjectVersion;

namespace Mt.ChangeLog.Logic.Mappers;

/// <summary>
/// Методы расширения для класса <see cref="LastProjectRevisionView"/>.
/// </summary>
public static class LastProjectRevisionMapper
{
    /// <summary>
    /// Преобразовать сущность <see cref="LastProjectRevisionView"/> в модель <see cref="LastProjectRevisionModel"/>.
    /// </summary>
    /// <param name="entity">Сущность.</param>
    /// <returns>Модель.</returns>
    public static LastProjectRevisionModel ToModel(this LastProjectRevisionView entity)
    {
        return new LastProjectRevisionModel
        {
            Prefix = entity.Prefix,
            Title = entity.Title,
            Version = entity.Version,
            Revision = entity.Revision,
            Platform = entity.Platform,
            ArmEdit = entity.ArmEdit,
            Date = entity.Date,
        };
    }

    /// <summary>
    /// Преобразовать сущность <see cref="LastProjectRevisionView"/> в краткую модель истории редакции <see cref="ProjectRevisionHistoryShortModel"/>.
    /// </summary>
    /// <param name="entity">Сущность.</param>
    /// <returns>Модель.</returns>
    public static ProjectRevisionHistoryShortModel ToHistoryShortModel(this LastProjectRevisionView entity)
    {
        return new ProjectRevisionHistoryShortModel
        {
            Id = entity.ProjectRevisionId,
            Date = entity.Date,
            Platform = entity.Platform,
            Title = entity.ToString(),
        };
    }

    /// <summary>
    /// Преобразовать сущность <see cref="LastProjectRevisionView"/> в краткую модель истории версии <see cref="ProjectVersionShortModel"/>.
    /// </summary>
    /// <param name="entity">Сущность.</param>
    /// <returns>Модель.</returns>
    public static ProjectVersionShortModel ToProjectVersionShortModel(this LastProjectRevisionView entity)
    {
        return new ProjectVersionShortModel
        {
            Id = entity.ProjectVersionId,
            Prefix = entity.Prefix,
            Title = entity.Title,
            Version = entity.Version,
        };
    }
}