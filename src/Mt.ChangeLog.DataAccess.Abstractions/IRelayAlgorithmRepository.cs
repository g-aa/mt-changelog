using Mt.ChangeLog.TransferObjects.RelayAlgorithm;
using System;

namespace Mt.ChangeLog.DataAccess.Abstractions
{
    /// <summary>
    /// Репозиторий для работы с алгоритмами РЗА.
    /// </summary>
    public interface IRelayAlgorithmRepository :
        IReadingRepository<RelayAlgorithmModel, RelayAlgorithmShortModel, RelayAlgorithmTableModel>
    {
    }
}