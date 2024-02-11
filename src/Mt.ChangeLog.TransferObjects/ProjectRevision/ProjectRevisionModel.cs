using System.ComponentModel.DataAnnotations;

using Mt.ChangeLog.TransferObjects.ArmEdit;
using Mt.ChangeLog.TransferObjects.Author;
using Mt.ChangeLog.TransferObjects.Communication;
using Mt.ChangeLog.TransferObjects.ProjectVersion;
using Mt.ChangeLog.TransferObjects.RelayAlgorithm;
using Mt.Utilities;

namespace Mt.ChangeLog.TransferObjects.ProjectRevision;

/// <summary>
/// Полная модель редакции проекта.
/// </summary>
public class ProjectRevisionModel
{
    /// <summary>
    /// Инициализация экземпляра <see cref="ProjectRevisionModel"/>.
    /// </summary>
    public ProjectRevisionModel()
    {
        Id = Guid.NewGuid();
        Date = DateTime.Now;
        Revision = DefaultString.Revision;
        Reason = DefaultString.Reason;
        Description = DefaultString.Description;
        ProjectVersion = new ProjectVersionShortModel();
        ParentRevision = new ProjectRevisionShortModel();
        Communication = new CommunicationShortModel();
        ArmEdit = new ArmEditShortModel();
        Authors = new HashSet<AuthorShortModel>();
        RelayAlgorithms = new HashSet<RelayAlgorithmShortModel>();
    }

    /// <summary>
    /// ИД.
    /// </summary>
    [Required]
    public Guid Id { get; set; }

    /// <summary>
    /// Дата компиляции.
    /// </summary>
    [Required]
    public DateTime Date { get; set; }

    /// <summary>
    /// Редакция.
    /// </summary>
    /// <example>00</example>
    [Required]
    [RegularExpression("^[0-9][2]$")]
    public string Revision { get; set; }

    /// <summary>
    /// Причина изменений.
    /// </summary>
    /// <example>Причина изменения...</example>
    [Required]
    [StringLength(500, MinimumLength = 0)]
    public string Reason { get; set; }

    /// <summary>
    /// Описание.
    /// </summary>
    /// <example>Описание...</example>
    [Required]
    [StringLength(5000, MinimumLength = 0)]
    public string Description { get; set; }

    /// <summary>
    /// Версия проекта.
    /// </summary>
    [Required]
    public ProjectVersionShortModel ProjectVersion { get; set; }

    /// <summary>
    /// Родительская редакция.
    /// </summary>
    public ProjectRevisionShortModel? ParentRevision { get; set; }

    /// <summary>
    /// Коммуникационный модуль.
    /// </summary>
    [Required]
    public CommunicationShortModel Communication { get; set; }

    /// <summary>
    /// Версия ArmEdit.
    /// </summary>
    [Required]
    public ArmEditShortModel ArmEdit { get; set; }

    /// <summary>
    /// Перечень авторов.
    /// </summary>
    [Required]
    public IReadOnlyCollection<AuthorShortModel> Authors { get; set; }

    /// <summary>
    /// Перечень алгоритмов.
    /// </summary>
    [Required]
    public IReadOnlyCollection<RelayAlgorithmShortModel> RelayAlgorithms { get; set; }
}