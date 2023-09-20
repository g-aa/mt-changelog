using Mt.ChangeLog.Entities.Tables;
using Mt.ChangeLog.TransferObjects.Platform;

namespace Mt.ChangeLog.Logic.Builders;

/// <summary>
/// Строитель <see cref="PlatformEntity"/>.
/// </summary>
public sealed class PlatformBuilder
{
    private readonly PlatformEntity entity;

    private string title;

    private string description;

    private IQueryable<AnalogModuleEntity> modules;

    /// <summary>
    /// Инициализация экземпляра класса <see cref="PlatformBuilder"/>.
    /// </summary>
    /// <param name="entity">Сущность.</param>
    public PlatformBuilder(PlatformEntity entity)
    {
        this.entity = entity;
        this.title = entity.Title;
        this.description = entity.Description;
        this.modules = entity.AnalogModules.AsQueryable();
    }

    /// <summary>
    /// Добавить атрибуты.
    /// </summary>
    /// <param name="model">Модель.</param>
    /// <returns>Строитель.</returns>
    public PlatformBuilder SetAttributes(PlatformModel model)
    {
        this.title = model.Title;
        this.description = model.Description;
        return this;
    }

    /// <summary>
    /// Добавить аналоговые модули.
    /// </summary>
    /// <param name="modules">Перечень аналоговых модулей.</param>
    /// <returns>Строитель.</returns>
    public PlatformBuilder SetAnalogModules(IQueryable<AnalogModuleEntity> modules)
    {
        this.modules = modules;
        return this;
    }

    /// <summary>
    /// Построить сущность.
    /// </summary>
    /// <returns>Сущность.</returns>
    /// <exception cref="ArgumentException">Ошибка в логике обработки связей.</exception>
    public PlatformEntity Build()
    {
        var prohibModules = this.entity.AnalogModules.Except(this.modules).Where(e => e.Projects.Intersect(this.entity.Projects).Any()).Select(e => e.Title);
        if (prohibModules.Any())
        {
            throw new ArgumentException($"Следующие аналоговые модули: \"{string.Join(",", prohibModules)}\" используются в проектах (БФПО) и не могут быть исключены из состава программных платформ \"{this.entity}\"");
        }

        // атрибуты:
        // this.entity.Id - не обновляется!
        this.entity.Title = this.title;
        this.entity.Description = this.description;

        // реляционные связи:
        this.entity.AnalogModules = this.modules.ToHashSet();

        // this.entity.Projects - не обновляется!
        return this.entity;
    }
}