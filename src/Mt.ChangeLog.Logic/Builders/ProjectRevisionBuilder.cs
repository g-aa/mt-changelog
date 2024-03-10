using Mt.ChangeLog.Entities.Tables;
using Mt.ChangeLog.TransferObjects.ProjectRevision;

namespace Mt.ChangeLog.Logic.Builders;

/// <summary>
/// Строитель <see cref="ProjectRevisionEntity"/>.
/// </summary>
public class ProjectRevisionBuilder
{
    private readonly ProjectRevisionEntity _entity;

    private DateTime? _date;

    private string _revision;

    private string _reason;

    private string _description;

    private ProjectVersionEntity? _project;

    private ProjectRevisionEntity? _parent;

    private ArmEditEntity? _armEdit;

    private CommunicationEntity? _communication;

    private IQueryable<AuthorEntity> _authors;

    private IQueryable<RelayAlgorithmEntity> _algorithms;

    /// <summary>
    /// Инициализация экземпляра класса <see cref="ProjectRevisionBuilder"/>.
    /// </summary>
    /// <param name="entity">Сущность.</param>
    public ProjectRevisionBuilder(ProjectRevisionEntity entity)
    {
        _entity = entity;
        _date = entity.Date;
        _revision = entity.Revision;
        _reason = entity.Reason;
        _description = entity.Description;
        _project = entity.ProjectVersion;
        _parent = entity.ParentRevision;
        _armEdit = entity.ArmEdit;
        _communication = entity.Communication;
        _authors = entity.Authors.AsQueryable();
        _algorithms = entity.RelayAlgorithms.AsQueryable();
    }

    /// <summary>
    /// Добавить атрибуты.
    /// </summary>
    /// <param name="model">Модель.</param>
    /// <returns>Строитель.</returns>
    public ProjectRevisionBuilder SetAttributes(ProjectRevisionModel model)
    {
        _date = model.Date;
        _revision = model.Revision;
        _reason = model.Reason;
        _description = model.Description;
        return this;
    }

    /// <summary>
    /// Добавить версию проекта.
    /// </summary>
    /// <param name="project">Версия проекта.</param>
    /// <returns>Строитель.</returns>
    public ProjectRevisionBuilder SetProjectVersion(ProjectVersionEntity project)
    {
        _project = project;
        return this;
    }

    /// <summary>
    /// Добавить родительскую редакцию.
    /// </summary>
    /// <param name="parent">Родительская редакция.</param>
    /// <returns>Строитель.</returns>
    public ProjectRevisionBuilder SetParentRevision(ProjectRevisionEntity? parent)
    {
        _parent = parent;
        return this;
    }

    /// <summary>
    /// Добавить ArmEdit.
    /// </summary>
    /// <param name="armEdit">ArmEdit.</param>
    /// <returns>Строитель.</returns>
    public ProjectRevisionBuilder SetArmEdit(ArmEditEntity armEdit)
    {
        _armEdit = armEdit;
        return this;
    }

    /// <summary>
    /// Добавить АК.
    /// </summary>
    /// <param name="communication">АК.</param>
    /// <returns>Строитель.</returns>
    public ProjectRevisionBuilder SetCommunication(CommunicationEntity communication)
    {
        _communication = communication;
        return this;
    }

    /// <summary>
    /// Добавить перечень авторов.
    /// </summary>
    /// <param name="authors">Перечень авторов.</param>
    /// <returns>Строитель.</returns>
    public ProjectRevisionBuilder SetAuthors(IQueryable<AuthorEntity> authors)
    {
        _authors = authors;
        return this;
    }

    /// <summary>
    /// Добавить перечень алгоритмов.
    /// </summary>
    /// <param name="algorithms">Перечень алгоритмов.</param>
    /// <returns>Строитель.</returns>
    public ProjectRevisionBuilder SetAlgorithms(IQueryable<RelayAlgorithmEntity> algorithms)
    {
        _algorithms = algorithms;
        return this;
    }

    /// <summary>
    /// Построить сущность.
    /// </summary>
    /// <returns>Сущность.</returns>
    public ProjectRevisionEntity Build()
    {
        // атрибуты:
        // _entity.Id - не обновляется!
        _entity.Date = _date != null ? _date.Value : DateTime.Now;

        // - не обновляется!
        if (string.IsNullOrEmpty(_entity.Revision))
        {
            _entity.Revision = _revision;
        }

        _entity.Reason = _reason;
        _entity.Description = _description;

        // реляционные связи:
        // - не обновляется!
        if (_entity.ProjectVersion is null)
        {
            _entity.ProjectVersion = _project;
        }

        _entity.ParentRevision = _parent;
        _entity.ArmEdit = _armEdit;
        _entity.Communication = _communication;
        _entity.Authors = _authors.ToHashSet();
        _entity.RelayAlgorithms = _algorithms.ToHashSet();
        return _entity;
    }
}