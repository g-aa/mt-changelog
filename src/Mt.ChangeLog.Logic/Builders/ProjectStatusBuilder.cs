using Mt.ChangeLog.Entities.Tables;
using Mt.ChangeLog.TransferObjects.ProjectStatus;

namespace Mt.ChangeLog.Logic.Builders;

/// <summary>
/// Строитель <see cref="ProjectStatusEntity"/>.
/// </summary>
public class ProjectStatusBuilder
{
    private readonly ProjectStatusEntity entity;

    private string title;

    private string description;

    /// <summary>
    /// Инициализация экземпляра класса <see cref="ProjectStatusBuilder"/>.
    /// </summary>
    /// <param name="entity">Сущность.</param>
    public ProjectStatusBuilder(ProjectStatusEntity entity)
    {
        this.entity = entity;
        this.title = entity.Title;
        this.description = entity.Description;
    }

    /// <summary>
    /// Добавить атрибуты.
    /// </summary>
    /// <param name="model">Модель.</param>
    /// <returns>Строитель.</returns>
    public ProjectStatusBuilder SetAttributes(ProjectStatusModel model)
    {
        this.title = model.Title;
        this.description = model.Description;
        return this;
    }

    /// <summary>
    /// Построить сущность.
    /// </summary>
    /// <returns>Сущность.</returns>
    public ProjectStatusEntity Build()
    {
        // атрибуты:
        // this.entity.Id - не обновляется!
        this.entity.Title = this.title;
        this.entity.Description = this.description;

        // реляционные связи:
        // this.entity.ProjectVersions - не обновляется!
        return this.entity;
    }
}