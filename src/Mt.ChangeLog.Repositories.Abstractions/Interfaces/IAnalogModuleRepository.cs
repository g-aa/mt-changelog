using Mt.ChangeLog.TransferObjects.AnalogModule;
using System;

namespace Mt.ChangeLog.Repositories.Abstractions.Interfaces
{
    /// <summary>
    /// Репозиторий для работы с аналоговоыми модулями.
    /// </summary>
    public interface IAnalogModuleRepository :
        IDisposable,
        IWritingRepository<AnalogModuleModel>,
        IReadingRepository<AnalogModuleModel, AnalogModuleShortModel, AnalogModuleTableModel> 
    {
    }
}