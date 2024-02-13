using Mt.ChangeLog.Entities.Tables;
using Mt.ChangeLog.TransferObjects.Platform;

namespace Mt.ChangeLog.Logic.Mappers;

/// <summary>
/// Методы расширения для класса <see cref="PlatformEntity"/>.
/// </summary>
public static class PlatformMapper
{
    /// <summary>
    /// Преобразовать сущность <see cref="PlatformEntity"/> в модель <see cref="PlatformShortModel"/>.
    /// </summary>
    /// <param name="entity">Сущность.</param>
    /// <returns>Модель.</returns>
    public static PlatformShortModel ToShortModel(this PlatformEntity entity)
    {
        return new PlatformShortModel
        {
            Id = entity.Id,
            Title = entity.Title,
        };
    }

    /// <summary>
    /// Преобразовать сущность <see cref="PlatformEntity"/> в модель <see cref="PlatformTableModel"/>.
    /// </summary>
    /// <param name="entity">Сущность.</param>
    /// <returns>Модель.</returns>
    public static PlatformTableModel ToTableModel(this PlatformEntity entity)
    {
        return new PlatformTableModel
        {
            Id = entity.Id,
            Title = entity.Title,
            Description = entity.Description,
        };
    }

    /// <summary>
    /// Преобразовать сущность <see cref="PlatformEntity"/> в модель <see cref="PlatformModel"/>.
    /// </summary>
    /// <param name="entity">Сущность.</param>
    /// <returns>Модель.</returns>
    public static PlatformModel ToModel(this PlatformEntity entity)
    {
        return new PlatformModel
        {
            Id = entity.Id,
            Title = entity.Title,
            Description = entity.Description,
            AnalogModules = entity.AnalogModules.Select(module => module.ToShortModel()).ToList(),
        };
    }
}