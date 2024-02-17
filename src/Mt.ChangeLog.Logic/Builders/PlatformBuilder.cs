using Mt.ChangeLog.Entities.Tables;
using Mt.ChangeLog.TransferObjects.Platform;

namespace Mt.ChangeLog.Logic.Builders;

/// <summary>
/// Строитель <see cref="PlatformEntity"/>.
/// </summary>
public sealed class PlatformBuilder
{
    private readonly PlatformEntity _entity;

    private string _title;

    private string _description;

    private IQueryable<AnalogModuleEntity> _modules;

    /// <summary>
    /// Инициализация экземпляра класса <see cref="PlatformBuilder"/>.
    /// </summary>
    /// <param name="entity">Сущность.</param>
    public PlatformBuilder(PlatformEntity entity)
    {
        _entity = entity;
        _title = entity.Title;
        _description = entity.Description;
        _modules = entity.AnalogModules.AsQueryable();
    }

    /// <summary>
    /// Добавить атрибуты.
    /// </summary>
    /// <param name="model">Модель.</param>
    /// <returns>Строитель.</returns>
    public PlatformBuilder SetAttributes(PlatformModel model)
    {
        _title = model.Title;
        _description = model.Description;
        return this;
    }

    /// <summary>
    /// Добавить аналоговые модули.
    /// </summary>
    /// <param name="modules">Перечень аналоговых модулей.</param>
    /// <returns>Строитель.</returns>
    public PlatformBuilder SetAnalogModules(IQueryable<AnalogModuleEntity> modules)
    {
        _modules = modules;
        return this;
    }

    /// <summary>
    /// Построить сущность.
    /// </summary>
    /// <returns>Сущность.</returns>
    /// <exception cref="ArgumentException">Ошибка в логике обработки связей.</exception>
    public PlatformEntity Build()
    {
        var prohibModules = _entity.AnalogModules.Except(_modules).Where(e => e.Projects.Intersect(_entity.Projects).Any()).Select(e => e.Title);
        if (prohibModules.Any())
        {
            throw new ArgumentException($"Следующие аналоговые модули: \"{string.Join(",", prohibModules)}\" используются в проектах (БФПО) и не могут быть исключены из состава программных платформ \"{_entity}\"");
        }

        // атрибуты:
        // _entity.Id - не обновляется!
        _entity.Title = _title;
        _entity.Description = _description;

        // реляционные связи:
        _entity.AnalogModules = _modules.ToHashSet();

        // this.entity.Projects - не обновляется!
        return _entity;
    }
}