using Mt.ChangeLog.Entities.Tables;
using Mt.ChangeLog.Logic.Builders;
using Mt.ChangeLog.TransferObjects.Protocol;

namespace Mt.ChangeLog.Logic.Mappers;

/// <summary>
/// Методы расширения для класса <see cref="ProtocolEntity"/>.
/// </summary>
public static class ProtocolMapper
{
    /// <summary>
    /// Преобразовать сущность <see cref="ProtocolEntity"/> в модель <see cref="ProtocolShortModel"/>.
    /// </summary>
    /// <param name="entity">Сущность.</param>
    /// <returns>Модель.</returns>
    public static ProtocolShortModel ToShortModel(this ProtocolEntity entity)
    {
        return new ProtocolShortModel
        {
            Id = entity.Id,
            Title = entity.Title,
        };
    }

    /// <summary>
    /// Преобразовать сущность <see cref="ProtocolEntity"/> в модель <see cref="ProtocolTableModel"/>.
    /// </summary>
    /// <param name="entity">Сущность.</param>
    /// <returns>Модель.</returns>
    public static ProtocolTableModel ToTableModel(this ProtocolEntity entity)
    {
        return new ProtocolTableModel
        {
            Id = entity.Id,
            Title = entity.Title,
            Description = entity.Description,
        };
    }

    /// <summary>
    /// Преобразовать сущность <see cref="ProtocolEntity"/> в модель <see cref="ProtocolModel"/>.
    /// </summary>
    /// <param name="entity">Сущность.</param>
    /// <returns>Модель.</returns>
    public static ProtocolModel ToModel(this ProtocolEntity entity)
    {
        return new ProtocolModel
        {
            Id = entity.Id,
            Title = entity.Title,
            Description = entity.Description,
            Communications = entity.Communications.OrderBy(e => e.Title).Select(e => e.ToShortModel()).ToList(),
        };
    }

    /// <summary>
    /// Получить строитель.
    /// </summary>
    /// <param name="entity">Сущность.</param>
    /// <returns>Строитель.</returns>
    public static ProtocolBuilder GetBuilder(this ProtocolEntity entity)
    {
        return new ProtocolBuilder(entity);
    }
}