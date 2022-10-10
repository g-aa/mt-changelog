using Mt.ChangeLog.TransferObjects.ProjectStatus;

namespace Mt.ChangeLog.DataAccess.Abstractions
{
    /// <summary>
    /// Репозиторий для работы с статусами проектов.
    /// </summary>
    public interface IProjectStatusRepository :
        IReadingRepository<ProjectStatusModel, ProjectStatusShortModel, ProjectStatusTableModel>
    {
    }
}