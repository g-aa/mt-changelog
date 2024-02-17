namespace Mt.ChangeLog.DataAccess.Abstraction;

/// <summary>
/// Читающий репозиторий.
/// </summary>
/// <typeparam name="TEntity">Полная модель сущности.</typeparam>
/// <typeparam name="TShortEntity">Краткая модель сущности.</typeparam>
/// <typeparam name="TTableEntity">Модель сущности для таблиц.</typeparam>
public interface IReadingRepository<TEntity, TShortEntity, TTableEntity>
{
    /// <summary>
    /// Получить сущность.
    /// </summary>
    /// <param name="guid">Идентификатор сущности.</param>
    /// <returns>Полная модель сущности.</returns>
    Task<TEntity> GetEntityAsync(Guid guid);

    /// <summary>
    /// Получить полный перечень кратких сущностей.
    /// </summary>
    /// <returns>Перечень кратких моделей.</returns>
    Task<IReadOnlyCollection<TShortEntity>> GetShortEntitiesAsync();

    /// <summary>
    /// Получить полный перечень сущностей для таблиц.
    /// </summary>
    /// <returns>Перечень моделей для таблиц.</returns>
    Task<IReadOnlyCollection<TTableEntity>> GetTableEntitiesAsync();
}