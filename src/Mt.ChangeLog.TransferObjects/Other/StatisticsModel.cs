using System.ComponentModel.DataAnnotations;

using Mt.ChangeLog.TransferObjects.Author;
using Mt.ChangeLog.TransferObjects.Historical;
using Mt.Utilities;

namespace Mt.ChangeLog.TransferObjects.Other;

/// <summary>
/// Статистика ChangeLog.
/// </summary>
public class StatisticsModel
{
    /// <summary>
    /// Инициализация нового экземпляра класса <see cref="StatisticsModel"/>.
    /// </summary>
    public StatisticsModel()
    {
        Date = DateTime.Now;
        ArmEdit = DefaultString.Version;
        ProjectCount = 0;
        ProjectDistributions = new Dictionary<string, int>();
        AuthorContributions = Array.Empty<AuthorContributionModel>();
        LastModifiedProjects = Array.Empty<ProjectRevisionHistoryShortModel>();
    }

    /// <summary>
    /// Дата сбора статистики.
    /// </summary>
    [Required]
    public DateTime Date { get; set; }

    /// <summary>
    /// Актуальная версия ArmEdit.
    /// </summary>
    /// <example>v0.00.00.00</example>
    [Required]
    [StringLength(11, MinimumLength = 11)]
    [RegularExpression(StringFormat.Version)]
    public string ArmEdit { get; set; }

    /// <summary>
    /// Количество проектов.
    /// </summary>
    [Required]
    [Range(0, int.MaxValue)]
    public int ProjectCount { get; set; }

    /// <summary>
    /// Распространение проектов.
    /// </summary>
    [Required]
    public IReadOnlyDictionary<string, int> ProjectDistributions { get; set; }

    /// <summary>
    /// Модель автор общий вклад в проекты.
    /// </summary>
    [Required]
    public IReadOnlyCollection<AuthorContributionModel> AuthorContributions { get; set; }

    /// <summary>
    /// Последние изменения по проектам.
    /// </summary>
    [Required]
    public IReadOnlyCollection<ProjectRevisionHistoryShortModel> LastModifiedProjects { get; set; }
}