using Mt.ChangeLog.Entities.Tables;
using Mt.ChangeLog.Logic.Builders;
using Mt.ChangeLog.TransferObjects.AnalogModule;

namespace Mt.ChangeLog.Logic.Mappers;

/// <summary>
/// Методы расширения для класса <see cref="AnalogModuleEntity"/>.
/// </summary>
public static class AnalogModuleMapper
{
    /// <summary>
    /// Преобразовать сущность <see cref="AnalogModuleEntity"/> в модель <see cref="AnalogModuleShortModel"/>.
    /// </summary>
    /// <param name="entity">Сущность.</param>
    /// <returns>Модель.</returns>
    public static AnalogModuleShortModel ToShortModel(this AnalogModuleEntity entity)
    {
        return new AnalogModuleShortModel
        {
            Id = entity.Id,
            Title = entity.Title,
        };
    }

    /// <summary>
    /// Преобразовать сущность <see cref="AnalogModuleEntity"/> в модель <see cref="AnalogModuleTableModel"/>.
    /// </summary>
    /// <param name="entity">Сущность.</param>
    /// <returns>Модель.</returns>
    public static AnalogModuleTableModel ToTableModel(this AnalogModuleEntity entity)
    {
        return new AnalogModuleTableModel
        {
            Id = entity.Id,
            Title = entity.Title,
            Current = entity.Current,
            DIVG = entity.DIVG,
            Description = entity.Description,
        };
    }

    /// <summary>
    /// Преобразовать сущность <see cref="AnalogModuleEntity"/> в модель <see cref="AnalogModuleModel"/>.
    /// </summary>
    /// <param name="entity">Сущность.</param>
    /// <returns>Модель.</returns>
    public static AnalogModuleModel ToModel(this AnalogModuleEntity entity)
    {
        return new AnalogModuleModel
        {
            Id = entity.Id,
            Title = entity.Title,
            DIVG = entity.DIVG,
            Current = entity.Current,
            Description = entity.Description,
            Platforms = entity.Platforms.Select(platforms => platforms.ToShortModel()).ToList(),
        };
    }

    /// <summary>
    /// Получить строитель.
    /// </summary>
    /// <param name="entity">Сущность.</param>
    /// <returns>Строитель.</returns>
    public static AnalogModuleBuilder GetBuilder(this AnalogModuleEntity entity)
    {
        return new AnalogModuleBuilder(entity);
    }
}