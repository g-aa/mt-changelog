using Mt.ChangeLog.TransferObjects.RelayAlgorithm;

namespace Mt.ChangeLog.DataAccess.Abstraction;

/// <summary>
/// Репозиторий для работы с алгоритмами РЗА.
/// </summary>
public interface IRelayAlgorithmRepository :
    IReadingRepository<RelayAlgorithmModel, RelayAlgorithmShortModel, RelayAlgorithmTableModel>
{
}