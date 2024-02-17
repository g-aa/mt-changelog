using Mt.ChangeLog.TransferObjects.ProjectStatus;

namespace Mt.ChangeLog.DataAccess.Abstraction;

/// <summary>
/// Репозиторий для работы с статусами проектов.
/// </summary>
public interface IProjectStatusRepository :
    IReadingRepository<ProjectStatusModel, ProjectStatusShortModel, ProjectStatusTableModel>
{
}