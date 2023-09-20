using Mt.ChangeLog.Entities.Tables;
using Mt.ChangeLog.TransferObjects.ProjectVersion;

namespace Mt.ChangeLog.Logic.Builders;

/// <summary>
/// Строитель <see cref="ProjectVersionEntity"/>.
/// </summary>
public class ProjectVersionBuilder
{
    private readonly ProjectVersionEntity entity;

    private string divg;

    private string prefix;

    private string title;

    private string version;

    private string description;

    private PlatformEntity? platform;

    private AnalogModuleEntity? module;

    private ProjectStatusEntity? status;

    /// <summary>
    /// Инициализация экземпляра класса <see cref="ProjectVersionBuilder"/>.
    /// </summary>
    /// <param name="entity">Сущность.</param>
    public ProjectVersionBuilder(ProjectVersionEntity entity)
    {
        this.entity = entity;
        this.divg = entity.DIVG;
        this.prefix = entity.Prefix;
        this.title = entity.Title;
        this.version = entity.Version;
        this.description = entity.Description;
        this.platform = entity.Platform;
        this.module = entity.AnalogModule;
        this.status = entity.ProjectStatus;
    }

    /// <summary>
    /// Добавить атрибуты.
    /// </summary>
    /// <param name="model">Модель.</param>
    /// <returns>Строитель.</returns>
    public ProjectVersionBuilder SetAttributes(ProjectVersionModel model)
    {
        this.divg = model.DIVG;
        this.prefix = model.Prefix;
        this.title = model.Title;
        this.version = model.Version;
        this.description = model.Description;
        return this;
    }

    /// <summary>
    /// Добавить платформу.
    /// </summary>
    /// <param name="platform">Платформа.</param>
    /// <returns>Строитель.</returns>
    public ProjectVersionBuilder SetPlatform(PlatformEntity platform)
    {
        this.platform = platform;
        return this;
    }

    /// <summary>
    /// Добавить платформу.
    /// </summary>
    /// <param name="module">Модуль.</param>
    /// <returns>Строитель.</returns>
    public ProjectVersionBuilder SetAnalogModule(AnalogModuleEntity module)
    {
        this.module = module;
        return this;
    }

    /// <summary>
    /// Добавить платформу.
    /// </summary>
    /// <param name="status">Статус.</param>
    /// <returns>Строитель.</returns>
    public ProjectVersionBuilder SetProjectStatus(ProjectStatusEntity status)
    {
        this.status = status;
        return this;
    }

    /// <summary>
    /// Построить сущность.
    /// </summary>
    /// <returns>Сущность.</returns>
    public ProjectVersionEntity Build()
    {
        // атрибуты:
        // this.entity.Id - не обновляется!
        this.entity.DIVG = this.divg;
        if (!string.IsNullOrEmpty(this.prefix))
        {
            this.entity.Prefix = this.prefix;
        }
        else if (this.module != null)
        {
            this.entity.Prefix = this.module.Title.Replace("БМРЗ", "БФПО", StringComparison.OrdinalIgnoreCase);
        }
        else
        {
            this.entity.Prefix = "БФПО";
        }

        this.entity.Title = this.title;
        this.entity.Version = this.version;
        this.entity.Description = this.description;

        // реляционные связи:
        this.entity.AnalogModule = this.module;
        this.entity.Platform = this.platform;
        this.entity.ProjectStatus = this.status;

        // this.entity.ProjectRevisions - не обновляется!
        return this.entity;
    }
}