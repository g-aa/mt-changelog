using System.ComponentModel.DataAnnotations;

using Mt.ChangeLog.TransferObjects.AnalogModule;
using Mt.ChangeLog.TransferObjects.Platform;
using Mt.ChangeLog.TransferObjects.ProjectStatus;
using Mt.Utilities;

namespace Mt.ChangeLog.TransferObjects.ProjectVersion;

/// <summary>
/// Полная модель версии проекта.
/// </summary>
public class ProjectVersionModel : ProjectVersionShortModel
{
    /// <summary>
    /// Инициализация экземпляра <see cref="ProjectVersionModel"/>.
    /// </summary>
    public ProjectVersionModel()
        : base()
    {
        DIVG = DefaultString.DIVG;
        Description = DefaultString.Description;
        ProjectStatus = new ProjectStatusShortModel();
        AnalogModule = new AnalogModuleShortModel();
        Platform = new PlatformShortModel();
    }

    /// <summary>
    /// ДИВГ.
    /// </summary>
    /// <example>ДИВГ.00000-00</example>
    [Required]
    [StringLength(13, MinimumLength = 13)]
    [RegularExpression(StringFormat.DIVG)]
    public string DIVG { get; set; }

    /// <summary>
    /// Описание.
    /// </summary>
    /// <example>Описание...</example>
    [Required]
    [StringLength(500, MinimumLength = 0)]
    public string Description { get; set; }

    /// <summary>
    /// Статус.
    /// </summary>
    [Required]
    public ProjectStatusShortModel ProjectStatus { get; set; }

    /// <summary>
    /// Аналоговый модуль.
    /// </summary>
    [Required]
    public AnalogModuleShortModel AnalogModule { get; set; }

    /// <summary>
    /// Платформа.
    /// </summary>
    [Required]
    public PlatformShortModel Platform { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return base.ToString();
    }
}