using Mt.ChangeLog.TransferObjects.Platform;

namespace Mt.ChangeLog.DataAccess.Abstraction;

/// <summary>
/// Репозиторий для работы с платформами БМРЗ.
/// </summary>
public interface IPlatformRepository :
    IReadingRepository<PlatformModel, PlatformShortModel, PlatformTableModel>
{
}