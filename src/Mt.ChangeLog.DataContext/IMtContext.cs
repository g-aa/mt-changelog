using Microsoft.EntityFrameworkCore;
using Mt.ChangeLog.Entities.Tables;
using Mt.ChangeLog.Entities.Views;

namespace Mt.ChangeLog.DataContext;

/// <summary>
/// Интерфейс контекста данных приложения.
/// </summary>
public interface IMtContext
{
    /// <summary>
    /// Схема базы данных: ChangeLog.
    /// </summary>
    public const string Schema = "changelog";

    #region [ ProjectVersionEntities ]

    /// <summary>
    /// Перечень <see cref="PlatformEntity"/>.
    /// </summary>
    public DbSet<PlatformEntity> Platforms { get; set; }

    /// <summary>
    /// Перечень <see cref="AnalogModuleEntity"/>.
    /// </summary>
    public DbSet<AnalogModuleEntity> AnalogModules { get; set; }

    /// <summary>
    /// Перечень <see cref="ProjectStatusEntity"/>.
    /// </summary>
    public DbSet<ProjectStatusEntity> ProjectStatuses { get; set; }

    /// <summary>
    /// Перечень <see cref="ProjectVersionEntity"/>.
    /// </summary>
    public DbSet<ProjectVersionEntity> ProjectVersions { get; set; }

    #endregion

    #region [ ProjectRevisionEntities ]

    /// <summary>
    /// Перечень <see cref="ProjectRevisionEntity"/>.
    /// </summary>
    public DbSet<ProjectRevisionEntity> ProjectRevisions { get; set; }

    /// <summary>
    /// Перечень <see cref="ArmEditEntity"/>.
    /// </summary>
    public DbSet<ArmEditEntity> ArmEdits { get; set; }

    /// <summary>
    /// Перечень <see cref="AuthorEntity"/>.
    /// </summary>
    public DbSet<AuthorEntity> Authors { get; set; }

    /// <summary>
    /// Перечень <see cref="ProtocolEntity"/>.
    /// </summary>
    public DbSet<ProtocolEntity> Protocols { get; set; }

    /// <summary>
    /// Перечень <see cref="CommunicationEntity"/>.
    /// </summary>
    public DbSet<CommunicationEntity> Communications { get; set; }

    /// <summary>
    /// Перечень <see cref="RelayAlgorithmEntity"/>.
    /// </summary>
    public DbSet<RelayAlgorithmEntity> RelayAlgorithms { get; set; }

    #endregion

    #region [ Views ]

    /// <summary>
    /// Перечень <see cref="LastProjectRevisionView"/>.
    /// </summary>
    public DbSet<LastProjectRevisionView> LastProjectRevisions { get; set; }

    /// <summary>
    /// Перечень <see cref="AuthorContributionView"/>.
    /// </summary>
    public DbSet<AuthorContributionView> AuthorContributions { get; set; }

    /// <summary>
    /// Перечень <see cref="AuthorProjectContributionView"/>.
    /// </summary>
    public DbSet<AuthorProjectContributionView> AuthorProjectContributions { get; set; }

    /// <summary>
    /// Перечень <see cref="ProjectHistoryRecordView"/>.
    /// </summary>
    public DbSet<ProjectHistoryRecordView> ProjectHistoryRecords { get; set; }

    #endregion
}