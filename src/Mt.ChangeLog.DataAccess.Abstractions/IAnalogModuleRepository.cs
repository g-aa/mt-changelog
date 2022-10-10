using Mt.ChangeLog.TransferObjects.AnalogModule;

namespace Mt.ChangeLog.DataAccess.Abstractions
{
    /// <summary>
    /// Репозиторий для работы с аналоговоыми модулями.
    /// </summary>
    public interface IAnalogModuleRepository :
        IReadingRepository<AnalogModuleModel, AnalogModuleShortModel, AnalogModuleTableModel>
    {
    }
}