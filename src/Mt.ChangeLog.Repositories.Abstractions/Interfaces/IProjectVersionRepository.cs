using Mt.ChangeLog.TransferObjects.Historical;
using Mt.ChangeLog.TransferObjects.ProjectVersion;
using System;
using System.Threading.Tasks;

namespace Mt.ChangeLog.Repositories.Abstractions.Interfaces
{
    /// <summary>
    /// Репозиторий для работы с версиями проектов.
    /// </summary>
    public interface IProjectVersionRepository :
        IDisposable,
        IWritingRepository<ProjectVersionModel>,
        IReadingRepository<ProjectVersionModel, ProjectVersionShortModel, ProjectVersionTableModel>
    {
        /// <summary>
        /// Получить полную историю изменений проекта.
        /// </summary>
        /// <param name="guid">ИД версии проекта.</param>
        /// <returns>Полная история изменений проекта.</returns>
        Task<ProjectVersionHistoryModel> GetProjectVersionHistoryAsync(Guid guid);
    }
}