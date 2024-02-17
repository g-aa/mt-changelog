using System.ComponentModel.DataAnnotations;

namespace Mt.ChangeLog.TransferObjects.ProjectStatus;

/// <summary>
/// Краткая модель статуса проекта.
/// </summary>
public class ProjectStatusShortModel
{
    /// <summary>
    /// Инициализация экземпляра <see cref="ProjectStatusShortModel"/>.
    /// </summary>
    public ProjectStatusShortModel()
    {
        Id = Guid.NewGuid();
        Title = "Внутренний";
    }

    /// <summary>
    /// ИД.
    /// </summary>
    /// <example>00000000-0000-0000-0000-000000000000</example>
    [Required]
    public Guid Id { get; set; }

    /// <summary>
    /// Наименование.
    /// </summary>
    /// <example>Внутренний</example>
    [Required]
    [StringLength(32, MinimumLength = 1)]
    public string Title { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return Title;
    }
}