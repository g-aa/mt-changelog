using Mt.ChangeLog.Entities.Tables;
using Mt.ChangeLog.Logic.Builders;
using Mt.ChangeLog.TransferObjects.RelayAlgorithm;

namespace Mt.ChangeLog.Logic.Mappers;

/// <summary>
/// Методы расширения для класса <see cref="RelayAlgorithmEntity"/>.
/// </summary>
public static class RelayAlgorithmMapper
{
    /// <summary>
    /// Преобразовать сущность <see cref="RelayAlgorithmEntity"/> в модель <see cref="RelayAlgorithmShortModel"/>.
    /// </summary>
    /// <param name="entity">Сущность.</param>
    /// <returns>Модель.</returns>
    public static RelayAlgorithmShortModel ToShortModel(this RelayAlgorithmEntity entity)
    {
        return new RelayAlgorithmShortModel
        {
            Id = entity.Id,
            Title = entity.Title,
        };
    }

    /// <summary>
    /// Преобразовать сущность <see cref="RelayAlgorithmEntity"/> в модель <see cref="RelayAlgorithmModel"/>.
    /// </summary>
    /// <param name="entity">Сущность.</param>
    /// <returns>Модель.</returns>
    public static RelayAlgorithmModel ToModel(this RelayAlgorithmEntity entity)
    {
        return new RelayAlgorithmModel
        {
            Id = entity.Id,
            Group = entity.Group,
            Title = entity.Title,
            ANSI = entity.ANSI,
            LogicalNode = entity.LogicalNode,
            Description = entity.Description,
        };
    }

    /// <summary>
    /// Получить строитель.
    /// </summary>
    /// <param name="entity">Сущность.</param>
    /// <returns>Строитель.</returns>
    public static RelayAlgorithmBuilder GetBuilder(this RelayAlgorithmEntity entity)
    {
        return new RelayAlgorithmBuilder(entity);
    }
}