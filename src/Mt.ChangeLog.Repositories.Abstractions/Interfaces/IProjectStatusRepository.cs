using Mt.ChangeLog.TransferObjects.ProjectStatus;
using System;

namespace Mt.ChangeLog.Repositories.Abstractions.Interfaces
{
    /// <summary>
    /// Репозиторий для работы с статусами проектов.
    /// </summary>
    public interface IProjectStatusRepository :
        IDisposable,
        IWritingRepository<ProjectStatusModel>,
        IReadingRepository<ProjectStatusModel, ProjectStatusShortModel, ProjectStatusTableModel>
    {
    }
}