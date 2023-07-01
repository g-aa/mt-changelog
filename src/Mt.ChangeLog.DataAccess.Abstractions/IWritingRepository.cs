namespace Mt.ChangeLog.DataAccess.Abstractions
{
    /// <summary>
    /// Пишущий репозиторий.
    /// </summary>
    /// <typeparam name="TEntity">Полная модель сущности.</typeparam>
    public interface IWritingRepository<in TEntity>
    {
        /// <summary>
        /// Добавить сущность в систему.
        /// </summary>
        /// <param name="entity">Сущность.</param>
        /// <returns>Задача.</returns>
        Task AddEntityAsync(TEntity entity);

        /// <summary>
        /// Обновить сущность в системе.
        /// </summary>
        /// <param name="entity">Сущность.</param>
        /// <returns>Задача.</returns>
        Task UpdateEntityAsync(TEntity entity);

        /// <summary>
        /// Удалить сущность из системы.
        /// </summary>
        /// <param name="guid">Идентификатор сущности.</param>
        /// <returns>Задача.</returns>
        Task DeleteEntityAsync(Guid guid);
    }
}