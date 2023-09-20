using Mt.ChangeLog.Entities.Tables;
using Mt.ChangeLog.TransferObjects.AnalogModule;

namespace Mt.ChangeLog.Logic.Builders;

/// <summary>
/// Строитель <see cref="AnalogModuleEntity"/>.
/// </summary>
public sealed class AnalogModuleBuilder
{
    private readonly AnalogModuleEntity entity;

    private string divg;

    private string title;

    private string current;

    private string description;

    private IQueryable<PlatformEntity> platforms;

    /// <summary>
    /// Инициализация экземпляра класса <see cref="AnalogModuleBuilder"/>.
    /// </summary>
    /// <param name="entity">Сущность.</param>
    public AnalogModuleBuilder(AnalogModuleEntity entity)
    {
        this.entity = entity;
        this.divg = entity.DIVG;
        this.title = entity.Title;
        this.current = entity.Current;
        this.description = entity.Description;
        this.platforms = entity.Platforms.AsQueryable();
    }

    /// <summary>
    /// Добавить атрибуты.
    /// </summary>
    /// <param name="model">Модель.</param>
    /// <returns>Строитель.</returns>
    public AnalogModuleBuilder SetAttributes(AnalogModuleModel model)
    {
        this.divg = model.DIVG;
        this.title = model.Title;
        this.current = model.Current;
        this.description = model.Description;
        return this;
    }

    /// <summary>
    /// Добавить перечень платформ.
    /// </summary>
    /// <param name="platforms">Перечень платформ.</param>
    /// <returns>Строитель.</returns>
    public AnalogModuleBuilder SetPlatforms(IQueryable<PlatformEntity> platforms)
    {
        this.platforms = platforms;
        return this;
    }

    /// <summary>
    /// Построить сущность.
    /// </summary>
    /// <returns>Сущность.</returns>
    /// <exception cref="ArgumentException">Ошибка в логике обработки связей.</exception>
    public AnalogModuleEntity Build()
    {
        var prohibPlatforms = this.entity.Platforms.Except(this.platforms).Where(e => e.Projects.Intersect(this.entity.Projects).Any()).Select(e => e.Title);
        if (prohibPlatforms.Any())
        {
            throw new ArgumentException($"Следующие платформы: \"{string.Join(", ", prohibPlatforms)}\" используются в проектах (БФПО) и не могут быть исключены из состава аналогового модуля \"{this.entity}\"");
        }

        // атрибуты:
        // this.entity.Id - не обновляется!
        this.entity.DIVG = this.divg;
        this.entity.Title = this.title;
        this.entity.Current = this.current;
        this.entity.Description = this.description;

        // реляционные связи:
        this.entity.Platforms = this.platforms.ToHashSet();

        // this.entity.ProjectVersion - не обновляется!
        return this.entity;
    }
}