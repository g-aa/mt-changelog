using Mt.ChangeLog.TransferObjects.Platform;
using System;

namespace Mt.ChangeLog.Repositories.Abstractions.Interfaces
{
    /// <summary>
    /// Репозиторий для работы с платформами БМРЗ.
    /// </summary>
    public interface IPlatformRepository :
        IDisposable,
        IWritingRepository<PlatformModel>,
        IReadingRepository<PlatformModel, PlatformShortModel, PlatformTableModel>
    {
    }
}