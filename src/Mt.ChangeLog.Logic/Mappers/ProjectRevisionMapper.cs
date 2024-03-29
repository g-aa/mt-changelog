using System.Globalization;

using Mt.ChangeLog.Entities.Tables;
using Mt.ChangeLog.TransferObjects.Historical;
using Mt.ChangeLog.TransferObjects.ProjectRevision;

namespace Mt.ChangeLog.Logic.Mappers;

/// <summary>
/// Методы расширения для класса <see cref="ProjectRevisionEntity"/>.
/// </summary>
public static class ProjectRevisionMapper
{
    /// <summary>
    /// Преобразовать сущность <see cref="ProjectRevisionEntity"/> в модель <see cref="ProjectRevisionShortModel"/>.
    /// </summary>
    /// <param name="entity">Сущность.</param>
    /// <returns>Модель.</returns>
    public static ProjectRevisionShortModel ToShortModel(this ProjectRevisionEntity entity)
    {
        return new ProjectRevisionShortModel
        {
            Id = entity.Id,
            Prefix = entity.ProjectVersion!.Prefix,
            Title = entity.ProjectVersion!.Title,
            Version = entity.ProjectVersion!.Version,
            Revision = entity.Revision,
        };
    }

    /// <summary>
    /// Преобразовать сущность <see cref="ProjectRevisionEntity"/> в модель <see cref="ProjectRevisionTableModel"/>.
    /// </summary>
    /// <param name="entity">Сущность.</param>
    /// <returns>Модель.</returns>
    public static ProjectRevisionTableModel ToTableModel(this ProjectRevisionEntity entity)
    {
        return new ProjectRevisionTableModel
        {
            Id = entity.Id,
            Prefix = entity.ProjectVersion!.Prefix,
            Title = entity.ProjectVersion!.Title,
            Version = entity.ProjectVersion!.Version,
            Revision = entity.Revision,
            Date = entity.Date,
            ArmEdit = entity.ArmEdit!.Version,
            Reason = entity.Reason,
        };
    }

    /// <summary>
    /// Преобразовать сущность <see cref="ProjectRevisionEntity"/> в модель <see cref="ProjectRevisionTreeModel"/>.
    /// </summary>
    /// <param name="entity">Сущность.</param>
    /// <returns>Модель.</returns>
    public static ProjectRevisionTreeModel ToTreeModel(this ProjectRevisionEntity entity)
    {
        return new ProjectRevisionTreeModel
        {
            Id = entity.Id,
            ParentId = entity.ParentRevisionId,
            Prefix = entity.ProjectVersion!.Prefix,
            Title = entity.ProjectVersion!.Title,
            Version = entity.ProjectVersion!.Version,
            Revision = entity.Revision,
            Date = entity.Date.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
            ArmEdit = entity.ArmEdit!.Version,
            Platform = entity.ProjectVersion!.Platform!.Title,
        };
    }

    /// <summary>
    /// Преобразовать сущность <see cref="ProjectRevisionEntity"/> в модель <see cref="ProjectRevisionHistoryShortModel"/>.
    /// </summary>
    /// <param name="entity">Сущность.</param>
    /// <returns>Модель.</returns>
    public static ProjectRevisionHistoryShortModel ToHistoryShortModel(this ProjectRevisionEntity entity)
    {
        return new ProjectRevisionHistoryShortModel
        {
            Id = entity.Id,
            Date = entity.Date,
            Title = $"{entity.ProjectVersion!.Prefix}-{entity.ProjectVersion!.Title}-{entity.ProjectVersion!.Version}_{entity.Revision}",
            Platform = entity.ProjectVersion!.Platform!.Title,
        };
    }

    /// <summary>
    /// Преобразовать сущность <see cref="ProjectRevisionEntity"/> в модель <see cref="ProjectRevisionHistoryModel"/>.
    /// </summary>
    /// <param name="entity">Сущность.</param>
    /// <returns>Модель.</returns>
    public static ProjectRevisionHistoryModel ToHistoryModel(this ProjectRevisionEntity entity)
    {
        return new ProjectRevisionHistoryModel
        {
            Id = entity.Id,
            ArmEdit = entity.ArmEdit!.Version,
            Authors = entity.Authors.Select(a => $"{a.FirstName} {a.LastName}").ToList(),
            RelayAlgorithms = entity.RelayAlgorithms.Select(ra => ra.Title).ToList(),
            Communication = string.Join(", ", entity.Communication!.Protocols.OrderBy(e => e.Title).Select(e => e.Title)),
            Date = entity.Date,
            Description = entity.Description,
            Platform = entity.ProjectVersion!.Platform!.Title,
            Reason = entity.Reason,
            Title = $"{entity.ProjectVersion!.Prefix}-{entity.ProjectVersion!.Title}-{entity.ProjectVersion!.Version}_{entity.Revision}",
        };
    }

    /// <summary>
    /// Преобразовать сущность <see cref="ProjectRevisionEntity"/> в модель <see cref="ProjectRevisionModel"/>.
    /// </summary>
    /// <param name="entity">Сущность.</param>
    /// <returns>Модель.</returns>
    public static ProjectRevisionModel ToModel(this ProjectRevisionEntity entity)
    {
        return new ProjectRevisionModel
        {
            Id = entity.Id,
            Date = entity.Date,
            Revision = entity.Revision,
            Reason = entity.Reason,
            Description = entity.Description,
            ParentRevision = entity.ParentRevision?.ToShortModel(),
            ProjectVersion = entity.ProjectVersion!.ToShortModel(),
            ArmEdit = entity.ArmEdit!.ToShortModel(),
            Communication = entity.Communication!.ToShortModel(),
            Authors = entity.Authors.Select(author => author.ToShortModel()).ToList(),
            RelayAlgorithms = entity.RelayAlgorithms.Select(alg => alg.ToShortModel()).ToList(),
        };
    }
}