using Mt.ChangeLog.TransferObjects.ArmEdit;
using System;
using System.Threading.Tasks;

namespace Mt.ChangeLog.Repositories.Abstractions.Interfaces
{
    /// <summary>
    /// Репозиторий для работы с ArmEdit.
    /// </summary>
    public interface IArmEditRepository :
        IDisposable,
        IWritingRepository<ArmEditModel>,
        IReadingRepository<ArmEditModel, ArmEditShortModel, ArmEditTableModel>
    {
        /// <summary>
        /// Получить актуальныю версию ArmEdit.
        /// </summary>
        /// <returns>Актуальная версия ArmEdit.</returns>
        Task<ArmEditModel> GetActualAsync();
    }
}