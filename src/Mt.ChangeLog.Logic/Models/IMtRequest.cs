namespace Mt.ChangeLog.Logic.Models
{
    /// <summary>
    /// Запрос используемый в логике MT.
    /// </summary>
    public interface IMtRequest
    {
        /// <summary>
        /// Идентификатор запроса.
        /// </summary>
        Guid Guid { get; }

        /// <summary>
        /// Наименование пользователя.
        /// </summary>
        string UserName { get; }
    }
}