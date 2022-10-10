using Mt.ChangeLog.TransferObjects.Historical;
using Mt.ChangeLog.TransferObjects.ProjectRevision;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mt.ChangeLog.Repositories.Abstractions.Interfaces
{
    /// <summary>
    /// Репозиторий для работы с редакциями проектов.
    /// </summary>
    public interface IProjectRevisionRepository :
        IDisposable,
        IWritingRepository<ProjectRevisionModel>,
        IReadingRepository<ProjectRevisionModel, ProjectRevisionShortModel, ProjectRevisionTableModel>
    {
        /// <summary>
        /// Получить подготовленный шаблон редакции на основании ИД версии проекта.
        /// </summary>
        /// <param name="guid">ИД версии проекта.</param>
        /// <returns>Шаблон редакции проекта.</returns>
        Task<ProjectRevisionModel> GetTemplateAsync(Guid guid);

        /// <summary>
        /// Получить информацию об изменениях редакции проекта.
        /// </summary>
        /// <param name="guid">ИД редакции проекта.</param>
        /// <returns>Информация об изменениях редакции проекта.</returns>
        Task<ProjectRevisionHistoryModel> GetHistoryAsync(Guid guid);

        /// <summary>
        /// Получить перечень последних редакций проектов.
        /// </summary>
        /// <returns>Перечень последних редакций проектов.</returns>
        Task<IEnumerable<LastProjectRevisionModel>> GetLastsAsync();

        /// <summary>
        /// Получить выборку в количестве n-штук последних измененных проектов.
        /// </summary>
        /// <param name="count">Количество элементов в выборке.</param>
        /// <returns>N-штук последних измененных проектов.</returns>
        Task<IEnumerable<ProjectRevisionHistoryShortModel>> GetNLastModifiedProjectsAsync(ushort count);

        /// <summary>
        /// Получить выборку в количестве n-штук часто редактируемых проектов.
        /// </summary>
        /// <param name="count">Количество элементов в выборке.</param>
        /// <returns>N-штук часто редактируемых проектов.</returns>
        Task<IEnumerable<ProjectRevisionHistoryShortModel>> GetNMostChangingProjectsAsync(ushort count);
    }
}