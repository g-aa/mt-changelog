using Mt.ChangeLog.TransferObjects.Historical;
using Mt.ChangeLog.TransferObjects.ProjectRevision;

namespace Mt.ChangeLog.DataAccess.Abstraction;

/// <summary>
/// Репозиторий для работы с редакциями проектов.
/// </summary>
public interface IProjectRevisionRepository :
    IReadingRepository<ProjectRevisionModel, ProjectRevisionShortModel, ProjectRevisionTableModel>
{
    /// <summary>
    /// Получить подготовленный шаблон редакции на основании ИД версии проекта.
    /// </summary>
    /// <param name="guid">Идентификатор версии проекта.</param>
    /// <returns>Шаблон редакции проекта.</returns>
    Task<ProjectRevisionModel> GetTemplateAsync(Guid guid);

    /// <summary>
    /// Получить информацию об изменениях редакции проекта.
    /// </summary>
    /// <param name="guid">Идентификатор редакции проекта.</param>
    /// <returns>Информация об изменениях редакции проекта.</returns>
    Task<ProjectRevisionHistoryModel> GetHistoryAsync(Guid guid);

    /// <summary>
    /// Получить перечень последних редакций проектов.
    /// </summary>
    /// <returns>Перечень последних редакций проектов.</returns>
    Task<IReadOnlyCollection<LastProjectRevisionModel>> GetLastsAsync();

    /// <summary>
    /// Получить выборку в количестве n-штук последних измененных проектов.
    /// </summary>
    /// <param name="count">Количество элементов в выборке.</param>
    /// <returns>N-штук последних измененных проектов.</returns>
    Task<IReadOnlyCollection<ProjectRevisionHistoryShortModel>> GetNLastModifiedProjectsAsync(ushort count);

    /// <summary>
    /// Получить выборку в количестве n-штук часто редактируемых проектов.
    /// </summary>
    /// <param name="count">Количество элементов в выборке.</param>
    /// <returns>N-штук часто редактируемых проектов.</returns>
    Task<IReadOnlyCollection<ProjectRevisionHistoryShortModel>> GetNMostChangingProjectsAsync(ushort count);
}