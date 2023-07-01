using Mt.ChangeLog.TransferObjects.Historical;
using Mt.ChangeLog.TransferObjects.ProjectVersion;

namespace Mt.ChangeLog.DataAccess.Abstractions
{
    /// <summary>
    /// Репозиторий для работы с версиями проектов.
    /// </summary>
    public interface IProjectVersionRepository :
        IReadingRepository<ProjectVersionModel, ProjectVersionShortModel, ProjectVersionTableModel>
    {
        /// <summary>
        /// Получить полную историю изменений проекта.
        /// </summary>
        /// <param name="guid">Идентификатор версии проекта.</param>
        /// <returns>Полная история изменений проекта.</returns>
        Task<ProjectVersionHistoryModel> GetProjectVersionHistoryAsync(Guid guid);
    }
}