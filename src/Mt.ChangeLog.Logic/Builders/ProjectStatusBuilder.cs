using Mt.ChangeLog.Entities.Tables;
using Mt.ChangeLog.TransferObjects.ProjectStatus;

namespace Mt.ChangeLog.Logic.Builders;

/// <summary>
/// Строитель <see cref="ProjectStatusEntity"/>.
/// </summary>
public class ProjectStatusBuilder
{
    private readonly ProjectStatusEntity _entity;

    private string _title;

    private string _description;

    /// <summary>
    /// Инициализация экземпляра класса <see cref="ProjectStatusBuilder"/>.
    /// </summary>
    /// <param name="entity">Сущность.</param>
    public ProjectStatusBuilder(ProjectStatusEntity entity)
    {
        _entity = entity;
        _title = entity.Title;
        _description = entity.Description;
    }

    /// <summary>
    /// Добавить атрибуты.
    /// </summary>
    /// <param name="model">Модель.</param>
    /// <returns>Строитель.</returns>
    public ProjectStatusBuilder SetAttributes(ProjectStatusModel model)
    {
        _title = model.Title;
        _description = model.Description;
        return this;
    }

    /// <summary>
    /// Построить сущность.
    /// </summary>
    /// <returns>Сущность.</returns>
    public ProjectStatusEntity Build()
    {
        // атрибуты:
        // _entity.Id - не обновляется!
        _entity.Title = _title;
        _entity.Description = _description;

        // реляционные связи:
        // _entity.ProjectVersions - не обновляется!
        return _entity;
    }
}