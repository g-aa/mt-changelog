using System.ComponentModel.DataAnnotations;

using Mt.Utilities;

namespace Mt.ChangeLog.TransferObjects.ProjectVersion;

/// <summary>
/// Модель версии проекта для таблиц.
/// </summary>
public class ProjectVersionTableModel : ProjectVersionShortModel
{
    /// <summary>
    /// Инициализация экземпляра <see cref="ProjectVersionTableModel"/>.
    /// </summary>
    public ProjectVersionTableModel()
        : base()
    {
        DIVG = DefaultString.DIVG;
        Status = "Внутренний";
        Description = DefaultString.Description;
        Platform = DefaultString.Platform;
        Module = DefaultString.AnalogModule;
    }

    /// <summary>
    /// ДИВГ.
    /// </summary>
    /// <example>ДИВГ.00000-00</example>
    [Required]
    [RegularExpression(StringFormat.DIVG)]
    public string DIVG { get; set; }

    /// <summary>
    /// Статус.
    /// </summary>
    /// <example>Внутренний</example>
    [Required]
    [StringLength(32, MinimumLength = 1)]
    public string Status { get; set; }

    /// <summary>
    /// Описание.
    /// </summary>
    /// <example>Описание...</example>
    [Required]
    [StringLength(500, MinimumLength = 0)]
    public string Description { get; set; }

    /// <summary>
    /// Платформа.
    /// </summary>
    /// <example>БМРЗ-000</example>
    [Required]
    [RegularExpression(StringFormat.Platform)]
    public string Platform { get; set; }

    /// <summary>
    /// Аналоговый модуль.
    /// </summary>
    /// <example>БМРЗ-000</example>
    [Required]
    [RegularExpression(StringFormat.AnalogModule)]
    public string Module { get; set; }
}