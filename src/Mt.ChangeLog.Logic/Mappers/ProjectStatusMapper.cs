using Mt.ChangeLog.Entities.Tables;
using Mt.ChangeLog.Logic.Builders;
using Mt.ChangeLog.TransferObjects.ProjectStatus;
using Mt.Utilities;

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
        Check.NotNull(entity, nameof(entity));
        var result = new ProjectStatusShortModel
        {
            Id = entity.Id,
            Title = entity.Title,
        };
        return result;
    }

    /// <summary>
    /// Преобразовать сущность <see cref="ProjectStatusEntity"/> в модель <see cref="ProjectStatusModel"/>.
    /// </summary>
    /// <param name="entity">Сущность.</param>
    /// <returns>Модель.</returns>
    public static ProjectStatusModel ToModel(this ProjectStatusEntity entity)
    {
        Check.NotNull(entity, nameof(entity));
        var result = new ProjectStatusModel
        {
            Id = entity.Id,
            Title = entity.Title,
            Description = entity.Description,
        };
        return result;
    }

    /// <summary>
    /// Получить строитель.
    /// </summary>
    /// <param name="entity">Сущность.</param>
    /// <returns>Строитель.</returns>
    public static ProjectStatusBuilder GetBuilder(this ProjectStatusEntity entity)
    {
        return new ProjectStatusBuilder(entity);
    }
}