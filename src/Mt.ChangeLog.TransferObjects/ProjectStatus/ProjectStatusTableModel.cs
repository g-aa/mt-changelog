using System.ComponentModel.DataAnnotations;

using Mt.Utilities;

namespace Mt.ChangeLog.TransferObjects.ProjectStatus;

/// <summary>
/// Модель статуса проекта для таблиц.
/// </summary>
public class ProjectStatusTableModel : ProjectStatusShortModel
{
    /// <summary>
    /// Инициализация экземпляра <see cref="ProjectStatusTableModel"/>.
    /// </summary>
    public ProjectStatusTableModel()
        : base()
    {
        Description = DefaultString.Description;
    }

    /// <summary>
    /// Описание.
    /// </summary>
    /// <example>Описание...</example>
    [Required]
    [StringLength(500, MinimumLength = 0)]
    public string Description { get; set; }
}