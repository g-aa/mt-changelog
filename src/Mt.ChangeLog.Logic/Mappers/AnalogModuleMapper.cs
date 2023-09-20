using Mt.ChangeLog.Entities.Tables;
using Mt.ChangeLog.Logic.Builders;
using Mt.ChangeLog.TransferObjects.AnalogModule;
using Mt.Utilities;

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
        Check.NotNull(entity, nameof(entity));
        var result = new AnalogModuleShortModel
        {
            Id = entity.Id,
            Title = entity.Title,
        };
        return result;
    }

    /// <summary>
    /// Преобразовать сущность <see cref="AnalogModuleEntity"/> в модель <see cref="AnalogModuleTableModel"/>.
    /// </summary>
    /// <param name="entity">Сущность.</param>
    /// <returns>Модель.</returns>
    public static AnalogModuleTableModel ToTableModel(this AnalogModuleEntity entity)
    {
        Check.NotNull(entity, nameof(entity));
        var result = new AnalogModuleTableModel
        {
            Id = entity.Id,
            Title = entity.Title,
            Current = entity.Current,
            DIVG = entity.DIVG,
            Description = entity.Description,
        };
        return result;
    }

    /// <summary>
    /// Преобразовать сущность <see cref="AnalogModuleEntity"/> в модель <see cref="AnalogModuleModel"/>.
    /// </summary>
    /// <param name="entity">Сущность.</param>
    /// <returns>Модель.</returns>
    public static AnalogModuleModel ToModel(this AnalogModuleEntity entity)
    {
        Check.NotNull(entity, nameof(entity));
        var result = new AnalogModuleModel
        {
            Id = entity.Id,
            Title = entity.Title,
            DIVG = entity.DIVG,
            Current = entity.Current,
            Description = entity.Description,
            Platforms = Check.NotNull(entity.Platforms, nameof(entity.Platforms)).Select(platforms => platforms.ToShortModel()),
        };
        return result;
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