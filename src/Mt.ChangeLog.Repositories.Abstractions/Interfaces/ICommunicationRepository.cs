using Mt.ChangeLog.TransferObjects.Communication;
using System;

namespace Mt.ChangeLog.Repositories.Abstractions.Interfaces
{
    /// <summary>
    /// Репозиторий для работы с коммуникационными модулями.
    /// </summary>
    public interface ICommunicationRepository :
        IDisposable,
        IWritingRepository<CommunicationModel>,
        IReadingRepository<CommunicationModel, CommunicationShortModel, CommunicationTableModel>
    {
    }
}