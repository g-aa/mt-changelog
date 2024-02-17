using Mt.ChangeLog.Entities.Tables;
using Mt.ChangeLog.TransferObjects.AnalogModule;

namespace Mt.ChangeLog.Logic.Builders;

/// <summary>
/// Строитель <see cref="AnalogModuleEntity"/>.
/// </summary>
public sealed class AnalogModuleBuilder
{
    private readonly AnalogModuleEntity _entity;

    private string _divg;

    private string _title;

    private string _current;

    private string _description;

    private IQueryable<PlatformEntity> _platforms;

    /// <summary>
    /// Инициализация экземпляра класса <see cref="AnalogModuleBuilder"/>.
    /// </summary>
    /// <param name="entity">Сущность.</param>
    public AnalogModuleBuilder(AnalogModuleEntity entity)
    {
        _entity = entity;
        _divg = entity.DIVG;
        _title = entity.Title;
        _current = entity.Current;
        _description = entity.Description;
        _platforms = entity.Platforms.AsQueryable();
    }

    /// <summary>
    /// Добавить атрибуты.
    /// </summary>
    /// <param name="model">Модель.</param>
    /// <returns>Строитель.</returns>
    public AnalogModuleBuilder SetAttributes(AnalogModuleModel model)
    {
        _divg = model.DIVG;
        _title = model.Title;
        _current = model.Current;
        _description = model.Description;
        return this;
    }

    /// <summary>
    /// Добавить перечень платформ.
    /// </summary>
    /// <param name="platforms">Перечень платформ.</param>
    /// <returns>Строитель.</returns>
    public AnalogModuleBuilder SetPlatforms(IQueryable<PlatformEntity> platforms)
    {
        _platforms = platforms;
        return this;
    }

    /// <summary>
    /// Построить сущность.
    /// </summary>
    /// <returns>Сущность.</returns>
    /// <exception cref="ArgumentException">Ошибка в логике обработки связей.</exception>
    public AnalogModuleEntity Build()
    {
        var prohibPlatforms = _entity.Platforms.Except(_platforms).Where(e => e.Projects.Intersect(_entity.Projects).Any()).Select(e => e.Title);
        if (prohibPlatforms.Any())
        {
            throw new ArgumentException($"Следующие платформы: \"{string.Join(", ", prohibPlatforms)}\" используются в проектах (БФПО) и не могут быть исключены из состава аналогового модуля \"{_entity}\"");
        }

        // атрибуты:
        // _entity.Id - не обновляется!
        _entity.DIVG = _divg;
        _entity.Title = _title;
        _entity.Current = _current;
        _entity.Description = _description;

        // реляционные связи:
        _entity.Platforms = _platforms.ToHashSet();

        // _entity.ProjectVersion - не обновляется!
        return _entity;
    }
}