using System.Linq.Expressions;

using Mt.Entities.Abstractions.Interfaces;
using Mt.Utilities;

namespace Mt.ChangeLog.Entities.Tables;

/// <summary>
/// Сущность редакции проекта.
/// </summary>
public class ProjectRevisionEntity : IEntity, IEqualityPredicate<ProjectRevisionEntity>
{
    /// <summary>
    /// Инициализация экземпляра <see cref="ProjectRevisionEntity"/>.
    /// </summary>
    public ProjectRevisionEntity()
    {
        Id = Guid.NewGuid();
        Date = DateTime.Now;
        Revision = DefaultString.Revision;
        Reason = DefaultString.Reason;
        Description = DefaultString.Description;
        Authors = new HashSet<AuthorEntity>();
        RelayAlgorithms = new HashSet<RelayAlgorithmEntity>();
    }

    /// <inheritdoc />
    public Guid Id { get; set; }

    /// <summary>
    /// Дата компиляции.
    /// </summary>
    public DateTime Date { get; set; }

    /// <summary>
    /// Редакция.
    /// </summary>
    public string Revision { get; set; }

    /// <summary>
    /// Причина изменений.
    /// </summary>
    public string Reason { get; set; }

    /// <summary>
    /// Описание.
    /// </summary>
    public string Description { get; set; }

    #region [ Relationships ]

    /// <summary>
    /// ИД версии проекта.
    /// </summary>
    public Guid ProjectVersionId { get; set; }

    /// <summary>
    /// Версия проекта.
    /// </summary>
    public ProjectVersionEntity? ProjectVersion { get; set; }

    /// <summary>
    /// ИД родительской редакции проекта.
    /// </summary>
    public Guid ParentRevisionId { get; set; }

    /// <summary>
    /// Родительская редакция проекта.
    /// </summary>
    public ProjectRevisionEntity? ParentRevision { get; set; }

    /// <summary>
    /// ИД версии ArmEdit.
    /// </summary>
    public Guid ArmEditId { get; set; }

    /// <summary>
    /// Версия ArmEdit.
    /// </summary>
    public ArmEditEntity? ArmEdit { get; set; }

    /// <summary>
    /// ИД коммуникационного модуля.
    /// </summary>
    public Guid CommunicationId { get; set; }

    /// <summary>
    /// Коммуникационный модуль.
    /// </summary>
    public CommunicationEntity? Communication { get; set; }

    /// <summary>
    /// Перечень авторов.
    /// </summary>
    public ICollection<AuthorEntity> Authors { get; set; }

    /// <summary>
    /// Перечень алгоритмов.
    /// </summary>
    public ICollection<RelayAlgorithmEntity> RelayAlgorithms { get; set; }
    #endregion

    /// <inheritdoc />
    public Expression<Func<ProjectRevisionEntity, bool>> GetEqualityPredicate()
    {
        return (ProjectRevisionEntity e) => e.Id == Id || ((e.ProjectVersionId == ProjectVersionId) && e.Revision == Revision);
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return obj is ProjectRevisionEntity e && (Id.Equals(e.Id) || (ProjectVersionId.Equals(e.ProjectVersionId) && Revision == e.Revision));
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        /*
         * при определении уникальности картежа нужно учитывать и версию проекта к которой он привязан !!!
         * ПС чисто теоретически даты и время компиляции должны отличаться, но так происходит не всегда
         */
        return HashCode.Combine(ProjectVersionId, Date, Revision);
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return $"ID: {Id}, {ProjectVersion?.Prefix}-{ProjectVersion?.Title}-{ProjectVersion?.Version}_{Revision}, дата изменения: {Date}";
    }
}