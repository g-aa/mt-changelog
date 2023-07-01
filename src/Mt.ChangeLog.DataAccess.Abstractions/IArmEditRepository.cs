using Mt.ChangeLog.TransferObjects.ArmEdit;

namespace Mt.ChangeLog.DataAccess.Abstractions
{
    /// <summary>
    /// Репозиторий для работы с ArmEdit.
    /// </summary>
    public interface IArmEditRepository :
        IReadingRepository<ArmEditModel, ArmEditShortModel, ArmEditTableModel>
    {
        /// <summary>
        /// Получить актуальныю версию ArmEdit.
        /// </summary>
        /// <returns>Актуальная версия ArmEdit.</returns>
        Task<ArmEditModel> GetActualAsync();
    }
}