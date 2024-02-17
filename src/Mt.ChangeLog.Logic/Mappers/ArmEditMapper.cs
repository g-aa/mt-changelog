using Mt.ChangeLog.Entities.Tables;
using Mt.ChangeLog.TransferObjects.ArmEdit;

namespace Mt.ChangeLog.Logic.Mappers;

/// <summary>
/// Методы расширения для класса <see cref="ArmEditEntity"/>.
/// </summary>
public static class ArmEditMapper
{
    /// <summary>
    /// Преобразовать сущность <see cref="ArmEditEntity"/> в модель <see cref="ArmEditShortModel"/>.
    /// </summary>
    /// <param name="entity">Сущность.</param>
    /// <returns>Модель.</returns>
    public static ArmEditShortModel ToShortModel(this ArmEditEntity entity)
    {
        return new ArmEditShortModel
        {
            Id = entity.Id,
            Version = entity.Version,
        };
    }

    /// <summary>
    /// Преобразовать сущность <see cref="ArmEditEntity"/> в модель <see cref="ArmEditModel"/>.
    /// </summary>
    /// <param name="entity">Сущность.</param>
    /// <returns>Модель.</returns>
    public static ArmEditModel ToModel(this ArmEditEntity entity)
    {
        return new ArmEditModel
        {
            Id = entity.Id,
            Date = entity.Date,
            DIVG = entity.DIVG,
            Version = entity.Version,
            Description = entity.Description,
        };
    }
}