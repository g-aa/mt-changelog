using Mt.ChangeLog.Entities.Tables;
using Mt.ChangeLog.TransferObjects.ProjectRevision;

namespace Mt.ChangeLog.Logic.Builders;

/// <summary>
/// Строитель <see cref="ProjectRevisionEntity"/>.
/// </summary>
public class ProjectRevisionBuilder
{
    private readonly ProjectRevisionEntity entity;

    private DateTime? date;

    private string revision;

    private string reason;

    private string description;

    private ProjectVersionEntity? project;

    private ProjectRevisionEntity? parent;

    private ArmEditEntity? armEdit;

    private CommunicationEntity? communication;

    private IQueryable<AuthorEntity> authors;

    private IQueryable<RelayAlgorithmEntity> algorithms;

    /// <summary>
    /// Инициализация экземпляра класса <see cref="ProjectRevisionBuilder"/>.
    /// </summary>
    /// <param name="entity">Сущность.</param>
    public ProjectRevisionBuilder(ProjectRevisionEntity entity)
    {
        this.entity = entity;
        this.date = entity.Date;
        this.revision = entity.Revision;
        this.reason = entity.Reason;
        this.description = entity.Description;
        this.project = entity.ProjectVersion;
        this.parent = entity.ParentRevision;
        this.armEdit = entity.ArmEdit;
        this.communication = entity.Communication;
        this.authors = entity.Authors.AsQueryable();
        this.algorithms = entity.RelayAlgorithms.AsQueryable();
    }

    /// <summary>
    /// Добавить атрибуты.
    /// </summary>
    /// <param name="model">Модель.</param>
    /// <returns>Строитель.</returns>
    public ProjectRevisionBuilder SetAttributes(ProjectRevisionModel model)
    {
        this.date = model.Date;
        this.revision = model.Revision;
        this.reason = model.Reason;
        this.description = model.Description;
        return this;
    }

    /// <summary>
    /// Добавить версию проекта.
    /// </summary>
    /// <param name="project">Версия проекта.</param>
    /// <returns>Строитель.</returns>
    public ProjectRevisionBuilder SetProjectVersion(ProjectVersionEntity project)
    {
        this.project = project;
        return this;
    }

    /// <summary>
    /// Добавить родительскую редакцию.
    /// </summary>
    /// <param name="parent">Родительская редакция.</param>
    /// <returns>Строитель.</returns>
    public ProjectRevisionBuilder SetParentRevision(ProjectRevisionEntity parent)
    {
        this.parent = parent;
        return this;
    }

    /// <summary>
    /// Добавить ArmEdit.
    /// </summary>
    /// <param name="armEdit">ArmEdit.</param>
    /// <returns>Строитель.</returns>
    public ProjectRevisionBuilder SetArmEdit(ArmEditEntity armEdit)
    {
        this.armEdit = armEdit;
        return this;
    }

    /// <summary>
    /// Добавить АК.
    /// </summary>
    /// <param name="communication">АК.</param>
    /// <returns>Строитель.</returns>
    public ProjectRevisionBuilder SetCommunication(CommunicationEntity communication)
    {
        this.communication = communication;
        return this;
    }

    /// <summary>
    /// Добавить перечень авторов.
    /// </summary>
    /// <param name="authors">Перечень авторов.</param>
    /// <returns>Строитель.</returns>
    public ProjectRevisionBuilder SetAuthors(IQueryable<AuthorEntity> authors)
    {
        this.authors = authors;
        return this;
    }

    /// <summary>
    /// Добавить перечень алгоритмов.
    /// </summary>
    /// <param name="algorithms">Перечень алгоритмов.</param>
    /// <returns>Строитель.</returns>
    public ProjectRevisionBuilder SetAlgorithms(IQueryable<RelayAlgorithmEntity> algorithms)
    {
        this.algorithms = algorithms;
        return this;
    }

    /// <summary>
    /// Построить сущность.
    /// </summary>
    /// <returns>Сущность.</returns>
    public ProjectRevisionEntity Build()
    {
        // атрибуты:
        // this.entity.Id - не обновляется!
        this.entity.Date = this.date != null ? this.date.Value : DateTime.Now;

        // - не обновляется!
        if (string.IsNullOrEmpty(this.entity.Revision))
        {
            this.entity.Revision = this.revision;
        }

        this.entity.Reason = this.reason;
        this.entity.Description = this.description;

        // реляционные связи:
        // - не обновляется!
        if (this.entity.ProjectVersion is null)
        {
            this.entity.ProjectVersion = this.project;
        }

        this.entity.ParentRevision = this.parent;
        this.entity.ArmEdit = this.armEdit;
        this.entity.Communication = this.communication;
        this.entity.Authors = this.authors.ToHashSet();
        this.entity.RelayAlgorithms = this.algorithms.ToHashSet();
        return this.entity;
    }
}