using System.ComponentModel.DataAnnotations;

using Mt.Utilities;

namespace Mt.ChangeLog.TransferObjects.Historical;

/// <summary>
/// Модель истории версии проекта.
/// </summary>
public class ProjectVersionHistoryModel
{
    /// <summary>
    /// Инициализация экземпляра <see cref="ProjectVersionHistoryModel"/>.
    /// </summary>
    public ProjectVersionHistoryModel()
    {
        Title = "БМРЗ";
        History = new List<ProjectRevisionHistoryModel>();
    }

    /// <summary>
    /// Наименование проекта, комбинация: "ProjectVersion.Prefix"-"ProjectVersion.Title"-"ProjectVersion.Version".
    /// </summary>
    /// <example>БФПО-000-ПМК-00</example>
    [Required]
    [RegularExpression(StringFormat.BFPO)]
    public string Title { get; set; }

    /// <summary>
    /// Перечень моделей истории редакции проекта.
    /// </summary>
    [Required]
    public ICollection<ProjectRevisionHistoryModel> History { get; private set; }
}