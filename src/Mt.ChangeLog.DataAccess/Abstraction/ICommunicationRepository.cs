using Mt.ChangeLog.TransferObjects.Communication;

namespace Mt.ChangeLog.DataAccess.Abstraction;

/// <summary>
/// Репозиторий для работы с коммуникационными модулями.
/// </summary>
public interface ICommunicationRepository :
    IReadingRepository<CommunicationModel, CommunicationShortModel, CommunicationTableModel>
{
}