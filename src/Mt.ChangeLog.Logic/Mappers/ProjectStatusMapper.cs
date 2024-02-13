using Mt.ChangeLog.Entities.Tables;
using Mt.ChangeLog.TransferObjects.ProjectStatus;

namespace Mt.ChangeLog.Logic.Mappers;

/// <summary>
/// Методы расширения для класса <see cref="ProjectStatusEntity"/>.
/// </summary>
public static class ProjectStatusMapper
{
    /// <summary>
    /// Преобразовать сущность <see cref="ProjectStatusEntity"/> в модель <see cref="ProjectStatusShortModel"/>.
    /// </summary>
    /// <param name="entity">Сущность.</param>
    /// <returns>Модель.</returns>
    public static ProjectStatusShortModel ToShortModel(this ProjectStatusEntity entity)
    {
        return new ProjectStatusShortModel
        {
            Id = entity.Id,
            Title = entity.Title,
        };
    }

    /// <summary>
    /// Преобразовать сущность <see cref="ProjectStatusEntity"/> в модель <see cref="ProjectStatusModel"/>.
    /// </summary>
    /// <param name="entity">Сущность.</param>
    /// <returns>Модель.</returns>
    public static ProjectStatusModel ToModel(this ProjectStatusEntity entity)
    {
        return new ProjectStatusModel
        {
            Id = entity.Id,
            Title = entity.Title,
            Description = entity.Description,
        };
    }
}