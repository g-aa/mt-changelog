using Mt.ChangeLog.TransferObjects.Protocol;

namespace Mt.ChangeLog.DataAccess.Abstraction;

/// <summary>
/// Репозиторий для работы с протоколами коммуникационного модуля.
/// </summary>
public interface IProtocolRepository :
    IReadingRepository<ProtocolModel, ProtocolShortModel, ProtocolTableModel>
{
}