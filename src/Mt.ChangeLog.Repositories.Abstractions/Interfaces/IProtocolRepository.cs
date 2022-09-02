using Mt.ChangeLog.TransferObjects.Protocol;
using System;

namespace Mt.ChangeLog.Repositories.Abstractions.Interfaces
{
    /// <summary>
    /// Репозиторий для работы с протоколами коммуникационнго модуля.
    /// </summary>
    public interface IProtocolRepository :
        IDisposable,
        IWritingRepository<ProtocolModel>,
        IReadingRepository<ProtocolModel, ProtocolShortModel, ProtocolTableModel>
    {
    }
}