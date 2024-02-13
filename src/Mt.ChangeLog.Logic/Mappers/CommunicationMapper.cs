using Mt.ChangeLog.Entities.Tables;
using Mt.ChangeLog.TransferObjects.Communication;

namespace Mt.ChangeLog.Logic.Mappers;

/// <summary>
/// Методы расширения для класса <see cref="CommunicationEntity"/>.
/// </summary>
public static class CommunicationMapper
{
    /// <summary>
    /// Преобразовать сущность <see cref="CommunicationEntity"/> в модель <see cref="CommunicationShortModel"/>.
    /// </summary>
    /// <param name="entity">Сущность.</param>
    /// <returns>Модель.</returns>
    public static CommunicationShortModel ToShortModel(this CommunicationEntity entity)
    {
        return new CommunicationShortModel
        {
            Id = entity.Id,
            Title = entity.Title,
        };
    }

    /// <summary>
    /// Преобразовать сущность <see cref="CommunicationEntity"/> в модель <see cref="CommunicationTableModel"/>.
    /// </summary>
    /// <param name="entity">Сущность.</param>
    /// <returns>Модель.</returns>
    public static CommunicationTableModel ToTableModel(this CommunicationEntity entity)
    {
        return new CommunicationTableModel
        {
            Id = entity.Id,
            Title = entity.Title,
            Description = entity.Description,
            Protocols = entity.Protocols.Count != 0 ? string.Join(", ", entity.Protocols.OrderBy(e => e.Title).Select(e => e.Title)) : string.Empty,
        };
    }

    /// <summary>
    /// Преобразовать сущность <see cref="CommunicationEntity"/> в модель <see cref="CommunicationModel"/>.
    /// </summary>
    /// <param name="entity">Сущность.</param>
    /// <returns>Модель.</returns>
    public static CommunicationModel ToModel(this CommunicationEntity entity)
    {
        return new CommunicationModel
        {
            Id = entity.Id,
            Title = entity.Title,
            Protocols = entity.Protocols.OrderBy(e => e.Title).Select(e => e.ToShortModel()).ToList(),
            Description = entity.Description,
        };
    }
}