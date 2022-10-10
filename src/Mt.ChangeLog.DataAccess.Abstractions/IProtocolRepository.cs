using Mt.ChangeLog.TransferObjects.Protocol;

namespace Mt.ChangeLog.DataAccess.Abstractions
{
    /// <summary>
    /// Репозиторий для работы с протоколами коммуникационнго модуля.
    /// </summary>
    public interface IProtocolRepository :
        IReadingRepository<ProtocolModel, ProtocolShortModel, ProtocolTableModel>
    {
    }
}