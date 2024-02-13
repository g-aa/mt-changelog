using Mt.ChangeLog.Entities.Tables;
using Mt.ChangeLog.TransferObjects.ProjectVersion;

namespace Mt.ChangeLog.Logic.Mappers;

/// <summary>
/// Методы расширения для класса <see cref="ProjectVersionEntity"/>.
/// </summary>
public static class ProjectVersionMapper
{
    /// <summary>
    /// Преобразовать сущность <see cref="ProjectVersionEntity"/> в модель <see cref="ProjectVersionShortModel"/>.
    /// </summary>
    /// <param name="entity">Сущность.</param>
    /// <returns>Модель.</returns>
    public static ProjectVersionShortModel ToShortModel(this ProjectVersionEntity entity)
    {
        return new ProjectVersionShortModel
        {
            Id = entity.Id,
            Prefix = entity.Prefix,
            Title = entity.Title,
            Version = entity.Version,
        };
    }

    /// <summary>
    /// Преобразовать сущность <see cref="ProjectVersionEntity"/> в модель <see cref="ProjectVersionTableModel"/>.
    /// </summary>
    /// <param name="entity">Сущность.</param>
    /// <returns>Модель.</returns>
    public static ProjectVersionTableModel ToTableModel(this ProjectVersionEntity entity)
    {
        return new ProjectVersionTableModel
        {
            Id = entity.Id,
            DIVG = entity.DIVG,
            Prefix = entity.Prefix,
            Title = entity.Title,
            Status = entity.ProjectStatus!.Title,
            Version = entity.Version,
            Description = entity.Description,
            Module = entity.AnalogModule!.Title,
            Platform = entity.Platform!.Title,
        };
    }

    /// <summary>
    /// Преобразовать сущность <see cref="ProjectVersionEntity"/> в модель <see cref="ProjectVersionModel"/>.
    /// </summary>
    /// <param name="entity">Сущность.</param>
    /// <returns>Модель.</returns>
    public static ProjectVersionModel ToModel(this ProjectVersionEntity entity)
    {
        return new ProjectVersionModel
        {
            Id = entity.Id,
            DIVG = entity.DIVG,
            Prefix = entity.Prefix,
            Title = entity.Title,
            ProjectStatus = entity.ProjectStatus!.ToShortModel(),
            Version = entity.Version,
            Description = entity.Description,
            AnalogModule = entity.AnalogModule!.ToShortModel(),
            Platform = entity.Platform!.ToShortModel(),
        };
    }
}