using System.ComponentModel.DataAnnotations;

using Mt.ChangeLog.TransferObjects.ProjectVersion;
using Mt.Utilities;

namespace Mt.ChangeLog.TransferObjects.ProjectRevision;

/// <summary>
/// Краткая модель редакции проекта.
/// </summary>
public class ProjectRevisionShortModel : ProjectVersionShortModel
{
    /// <summary>
    /// Инициализация экземпляра <see cref="ProjectRevisionShortModel"/>.
    /// </summary>
    public ProjectRevisionShortModel()
        : base()
    {
        Revision = DefaultString.Revision;
    }

    /// <summary>
    /// Редакция.
    /// </summary>
    /// <example>00</example>
    [Required]
    [RegularExpression("^[0-9]{2}$")]
    public string Revision { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return $"{base.ToString()}_{Revision}";
    }
}