using Mt.ChangeLog.Entities.Tables;
using Mt.ChangeLog.Logic.Builders;
using Mt.ChangeLog.TransferObjects.ProjectVersion;
using Mt.Utilities;

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
        Check.NotNull(entity, nameof(entity));
        var result = new ProjectVersionShortModel
        {
            Id = entity.Id,
            Prefix = entity.Prefix,
            Title = entity.Title,
            Version = entity.Version,
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
        var projectStatus = Check.NotNull(entity.ProjectStatus, nameof(entity.ProjectStatus));
        var analogModule = Check.NotNull(entity.AnalogModule, nameof(entity.AnalogModule));
        var platform = Check.NotNull(entity.Platform, nameof(entity.Platform));
        var result = new ProjectVersionTableModel
        {
            Id = entity.Id,
            DIVG = entity.DIVG,
            Prefix = entity.Prefix,
            Title = entity.Title,
            Status = projectStatus.Title,
            Version = entity.Version,
            Description = entity.Description,
            Module = analogModule.Title,
            Platform = platform.Title,
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
        var result = new ProjectVersionModel
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
        return result;
    }

    /// <summary>
    /// Получить строитель.
    /// </summary>
    /// <param name="entity">Сущность.</param>
    /// <returns>Строитель.</returns>
    public static ProjectVersionBuilder GetBuilder(this ProjectVersionEntity entity)
    {
        return new ProjectVersionBuilder(entity);
    }
}