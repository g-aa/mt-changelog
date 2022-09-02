using Mt.ChangeLog.TransferObjects.RelayAlgorithm;
using System;

namespace Mt.ChangeLog.Repositories.Abstractions.Interfaces
{
    /// <summary>
    /// Репозиторий для работы с алгоритмами РЗА.
    /// </summary>
    public interface IRelayAlgorithmRepository :
        IDisposable,
        IWritingRepository<RelayAlgorithmModel>,
        IReadingRepository<RelayAlgorithmModel, RelayAlgorithmShortModel, RelayAlgorithmTableModel>
    {
    }
}