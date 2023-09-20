using System.Globalization;

using Mt.ChangeLog.Entities.Tables;
using Mt.ChangeLog.Logic.Builders;
using Mt.ChangeLog.TransferObjects.Historical;
using Mt.ChangeLog.TransferObjects.ProjectRevision;
using Mt.Utilities;

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
        Check.NotNull(entity, nameof(entity));
        var projectVersion = Check.NotNull(entity.ProjectVersion, nameof(entity.ProjectVersion));
        var result = new ProjectRevisionShortModel
        {
            Id = entity.Id,
            Prefix = projectVersion.Prefix,
            Title = projectVersion.Title,
            Version = projectVersion.Version,
            Revision = entity.Revision,
        };
        return result;
    }

    /// <summary>
    /// Преобразовать сущность <see cref="ProjectRevisionEntity"/> в модель <see cref="ProjectRevisionTableModel"/>.
    /// </summary>
    /// <param name="entity">Сущность.</param>
    /// <returns>Модель.</returns>
    public static ProjectRevisionTableModel ToTableModel(this ProjectRevisionEntity entity)
    {
        Check.NotNull(entity, nameof(entity));
        var projectVersion = Check.NotNull(entity.ProjectVersion, nameof(entity.ProjectVersion));
        var armEdit = Check.NotNull(entity.ArmEdit, nameof(entity.ArmEdit));
        var result = new ProjectRevisionTableModel
        {
            Id = entity.Id,
            Prefix = projectVersion.Prefix,
            Title = projectVersion.Title,
            Version = projectVersion.Version,
            Revision = entity.Revision,
            Date = entity.Date,
            ArmEdit = armEdit.Version,
            Reason = entity.Reason,
        };
        return result;
    }

    /// <summary>
    /// Преобразовать сущность <see cref="ProjectRevisionEntity"/> в модель <see cref="ProjectRevisionTreeModel"/>.
    /// </summary>
    /// <param name="entity">Сущность.</param>
    /// <returns>Модель.</returns>
    public static ProjectRevisionTreeModel ToTreeModel(this ProjectRevisionEntity entity)
    {
        Check.NotNull(entity, nameof(entity));
        var projectVersion = Check.NotNull(entity.ProjectVersion, nameof(entity.ProjectVersion));
        var armEdit = Check.NotNull(entity.ArmEdit, nameof(entity.ArmEdit));
        var platform = Check.NotNull(projectVersion.Platform, nameof(projectVersion.Platform));
        var result = new ProjectRevisionTreeModel
        {
            Id = entity.Id,
            ParentId = entity.ParentRevisionId,
            Prefix = projectVersion.Prefix,
            Title = projectVersion.Title,
            Version = projectVersion.Version,
            Revision = entity.Revision,
            Date = entity.Date.ToString("YYYY-MM-dd", CultureInfo.InvariantCulture),
            ArmEdit = armEdit.Version,
            Platform = platform.Title,
        };
        return result;
    }

    /// <summary>
    /// Преобразовать сущность <see cref="ProjectRevisionEntity"/> в модель <see cref="ProjectRevisionHistoryShortModel"/>.
    /// </summary>
    /// <param name="entity">Сущность.</param>
    /// <returns>Модель.</returns>
    public static ProjectRevisionHistoryShortModel ToHistoryShortModel(this ProjectRevisionEntity entity)
    {
        Check.NotNull(entity, nameof(entity));
        var projectVersion = Check.NotNull(entity.ProjectVersion, nameof(entity.ProjectVersion));
        var platform = Check.NotNull(projectVersion.Platform, nameof(projectVersion.Platform));
        var result = new ProjectRevisionHistoryShortModel
        {
            Id = entity.Id,
            Date = entity.Date,
            Title = $"{projectVersion.Prefix}-{projectVersion.Title}-{projectVersion.Version}_{entity.Revision}",
            Platform = platform.Title,
        };
        return result;
    }

    /// <summary>
    /// Преобразовать сущность <see cref="ProjectRevisionEntity"/> в модель <see cref="ProjectRevisionHistoryModel"/>.
    /// </summary>
    /// <param name="entity">Сущность.</param>
    /// <returns>Модель.</returns>
    public static ProjectRevisionHistoryModel ToHistoryModel(this ProjectRevisionEntity entity)
    {
        Check.NotNull(entity, nameof(entity));
        var projectVersion = Check.NotNull(entity.ProjectVersion, nameof(entity.ProjectVersion));
        var armEdit = Check.NotNull(entity.ArmEdit, nameof(entity.ArmEdit));
        var platform = Check.NotNull(projectVersion.Platform, nameof(projectVersion.Platform));
        var communication = Check.NotNull(entity.Communication, nameof(entity.Communication));
        var result = new ProjectRevisionHistoryModel
        {
            Id = entity.Id,
            ArmEdit = armEdit.Version,
            Authors = entity.Authors.Select(a => $"{a.FirstName} {a.LastName}"),
            RelayAlgorithms = entity.RelayAlgorithms.Select(ra => ra.Title),
            Communication = string.Join(", ", communication.Protocols.OrderBy(e => e.Title).Select(e => e.Title)),
            Date = entity.Date,
            Description = entity.Description,
            Platform = platform.Title,
            Reason = entity.Reason,
            Title = $"{projectVersion.Prefix}-{projectVersion.Title}-{projectVersion.Version}_{entity.Revision}",
        };
        return result;
    }

    /// <summary>
    /// Преобразовать сущность <see cref="ProjectRevisionEntity"/> в модель <see cref="ProjectRevisionModel"/>.
    /// </summary>
    /// <param name="entity">Сущность.</param>
    /// <returns>Модель.</returns>
    public static ProjectRevisionModel ToModel(this ProjectRevisionEntity entity)
    {
        Check.NotNull(entity, nameof(entity));
        var result = new ProjectRevisionModel
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
    public static ProjectRevisionBuilder GetBuilder(this ProjectRevisionEntity entity)
    {
        return new ProjectRevisionBuilder(entity);
    }
}