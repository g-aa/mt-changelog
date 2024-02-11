using Mt.ChangeLog.Entities.Tables;
using Mt.ChangeLog.TransferObjects.ProjectVersion;

namespace Mt.ChangeLog.Logic.Builders;

/// <summary>
/// Строитель <see cref="ProjectVersionEntity"/>.
/// </summary>
public class ProjectVersionBuilder
{
    private readonly ProjectVersionEntity _entity;

    private string _divg;

    private string _prefix;

    private string _title;

    private string _version;

    private string _description;

    private PlatformEntity? _platform;

    private AnalogModuleEntity? _module;

    private ProjectStatusEntity? _status;

    /// <summary>
    /// Инициализация экземпляра класса <see cref="ProjectVersionBuilder"/>.
    /// </summary>
    /// <param name="entity">Сущность.</param>
    public ProjectVersionBuilder(ProjectVersionEntity entity)
    {
        _entity = entity;
        _divg = entity.DIVG;
        _prefix = entity.Prefix;
        _title = entity.Title;
        _version = entity.Version;
        _description = entity.Description;
        _platform = entity.Platform;
        _module = entity.AnalogModule;
        _status = entity.ProjectStatus;
    }

    /// <summary>
    /// Добавить атрибуты.
    /// </summary>
    /// <param name="model">Модель.</param>
    /// <returns>Строитель.</returns>
    public ProjectVersionBuilder SetAttributes(ProjectVersionModel model)
    {
        _divg = model.DIVG;
        _prefix = model.Prefix;
        _title = model.Title;
        _version = model.Version;
        _description = model.Description;
        return this;
    }

    /// <summary>
    /// Добавить платформу.
    /// </summary>
    /// <param name="platform">Платформа.</param>
    /// <returns>Строитель.</returns>
    public ProjectVersionBuilder SetPlatform(PlatformEntity platform)
    {
        _platform = platform;
        return this;
    }

    /// <summary>
    /// Добавить платформу.
    /// </summary>
    /// <param name="module">Модуль.</param>
    /// <returns>Строитель.</returns>
    public ProjectVersionBuilder SetAnalogModule(AnalogModuleEntity module)
    {
        _module = module;
        return this;
    }

    /// <summary>
    /// Добавить платформу.
    /// </summary>
    /// <param name="status">Статус.</param>
    /// <returns>Строитель.</returns>
    public ProjectVersionBuilder SetProjectStatus(ProjectStatusEntity status)
    {
        _status = status;
        return this;
    }

    /// <summary>
    /// Построить сущность.
    /// </summary>
    /// <returns>Сущность.</returns>
    public ProjectVersionEntity Build()
    {
        // атрибуты:
        // _entity.Id - не обновляется!
        _entity.DIVG = _divg;
        if (!string.IsNullOrEmpty(_prefix))
        {
            _entity.Prefix = _prefix;
        }
        else if (_module != null)
        {
            _entity.Prefix = _module.Title.Replace("БМРЗ", "БФПО", StringComparison.OrdinalIgnoreCase);
        }
        else
        {
            _entity.Prefix = "БФПО";
        }

        _entity.Title = _title;
        _entity.Version = _version;
        _entity.Description = _description;

        // реляционные связи:
        _entity.AnalogModule = _module;
        _entity.Platform = _platform;
        _entity.ProjectStatus = _status;

        // _entity.ProjectRevisions - не обновляется!
        return _entity;
    }
}