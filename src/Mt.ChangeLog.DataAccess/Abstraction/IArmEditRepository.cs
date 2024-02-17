using Mt.ChangeLog.TransferObjects.ArmEdit;

namespace Mt.ChangeLog.DataAccess.Abstraction;

/// <summary>
/// Репозиторий для работы с ArmEdit.
/// </summary>
public interface IArmEditRepository :
    IReadingRepository<ArmEditModel, ArmEditShortModel, ArmEditTableModel>
{
    /// <summary>
    /// Получить актуальную версию ArmEdit.
    /// </summary>
    /// <returns>Актуальная версия ArmEdit.</returns>
    Task<ArmEditModel> GetActualAsync();
}