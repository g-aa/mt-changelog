using Mt.ChangeLog.TransferObjects.AnalogModule;

namespace Mt.ChangeLog.DataAccess.Abstraction;

/// <summary>
/// Репозиторий для работы с аналоговыми модулями.
/// </summary>
public interface IAnalogModuleRepository :
    IReadingRepository<AnalogModuleModel, AnalogModuleShortModel, AnalogModuleTableModel>
{
}