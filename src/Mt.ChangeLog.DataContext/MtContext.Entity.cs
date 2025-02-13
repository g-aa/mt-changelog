using Microsoft.EntityFrameworkCore;

using Mt.ChangeLog.Entities.Tables;
using Mt.ChangeLog.Entities.Views;

namespace Mt.ChangeLog.DataContext;

/// <summary>
/// Контекст данных приложения.
/// </summary>
public sealed partial class MtContext
{
    #region [ ProjectVersionEntities ]

    /// <summary>
    /// Перечень <see cref="PlatformEntity"/>.
    /// </summary>
    public DbSet<PlatformEntity> Platforms { get; set; } = null!;

    /// <summary>
    /// Перечень <see cref="AnalogModuleEntity"/>.
    /// </summary>
    public DbSet<AnalogModuleEntity> AnalogModules { get; set; } = null!;

    /// <summary>
    /// Перечень <see cref="ProjectStatusEntity"/>.
    /// </summary>
    public DbSet<ProjectStatusEntity> ProjectStatuses { get; set; } = null!;

    /// <summary>
    /// Перечень <see cref="ProjectVersionEntity"/>.
    /// </summary>
    public DbSet<ProjectVersionEntity> ProjectVersions { get; set; } = null!;

    #endregion

    #region [ ProjectRevisionEntities ]

    /// <summary>
    /// Перечень <see cref="ProjectRevisionEntity"/>.
    /// </summary>
    public DbSet<ProjectRevisionEntity> ProjectRevisions { get; set; } = null!;

    /// <summary>
    /// Перечень <see cref="ArmEditEntity"/>.
    /// </summary>
    public DbSet<ArmEditEntity> ArmEdits { get; set; } = null!;

    /// <summary>
    /// Перечень <see cref="AuthorEntity"/>.
    /// </summary>
    public DbSet<AuthorEntity> Authors { get; set; } = null!;

    /// <summary>
    /// Перечень <see cref="ProtocolEntity"/>.
    /// </summary>
    public DbSet<ProtocolEntity> Protocols { get; set; } = null!;

    /// <summary>
    /// Перечень <see cref="CommunicationEntity"/>.
    /// </summary>
    public DbSet<CommunicationEntity> Communications { get; set; } = null!;

    /// <summary>
    /// Перечень <see cref="RelayAlgorithmEntity"/>.
    /// </summary>
    public DbSet<RelayAlgorithmEntity> RelayAlgorithms { get; set; } = null!;

    #endregion

    #region [ Views ]

    /// <summary>
    /// Перечень <see cref="LastProjectRevisionView"/>.
    /// </summary>
    public DbSet<LastProjectRevisionView> LastProjectRevisions { get; set; } = null!;

    /// <summary>
    /// Перечень <see cref="AuthorContributionView"/>.
    /// </summary>
    public DbSet<AuthorContributionView> AuthorContributions { get; set; } = null!;

    /// <summary>
    /// Перечень <see cref="AuthorProjectContributionView"/>.
    /// </summary>
    public DbSet<AuthorProjectContributionView> AuthorProjectContributions { get; set; } = null!;

    /// <summary>
    /// Перечень <see cref="ProjectHistoryRecordView"/>.
    /// </summary>
    public DbSet<ProjectHistoryRecordView> ProjectHistoryRecords { get; set; } = null!;

    #endregion
}