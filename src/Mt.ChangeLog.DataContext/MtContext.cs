using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using Mt.ChangeLog.DataContext.Configurations;
using Mt.ChangeLog.Entities.Tables;
using Mt.ChangeLog.Entities.Views;

namespace Mt.ChangeLog.DataContext;

/// <summary>
/// Контекст данных приложения.
/// </summary>
public sealed class MtContext : DbContext, IMtContext
{
    /// <summary>
    /// Схема базы данных: ChangeLog.
    /// </summary>
    public const string Schema = "changelog";

    /// <summary>
    /// Журнал логирования.
    /// </summary>
    private readonly ILogger<MtContext> _logger;

    /// <summary>
    /// Инициализация экземпляра класса <see cref="MtContext"/>.
    /// </summary>
    /// <param name="options">Настройки.</param>
    /// <param name="logger">Журнал логирования.</param>
    public MtContext(DbContextOptions<MtContext> options, ILogger<MtContext> logger)
        : base(options)
    {
        _logger = logger;
    }

    #region [ ProjectVersionEntities ]

    /// <inheritdoc />
    public DbSet<PlatformEntity> Platforms { get; set; } = null!;

    /// <inheritdoc />
    public DbSet<AnalogModuleEntity> AnalogModules { get; set; } = null!;

    /// <inheritdoc />
    public DbSet<ProjectStatusEntity> ProjectStatuses { get; set; } = null!;

    /// <inheritdoc />
    public DbSet<ProjectVersionEntity> ProjectVersions { get; set; } = null!;

    #endregion

    #region [ ProjectRevisionEntities ]

    /// <inheritdoc />
    public DbSet<ProjectRevisionEntity> ProjectRevisions { get; set; } = null!;

    /// <inheritdoc />
    public DbSet<ArmEditEntity> ArmEdits { get; set; } = null!;

    /// <inheritdoc />
    public DbSet<AuthorEntity> Authors { get; set; } = null!;

    /// <inheritdoc />
    public DbSet<ProtocolEntity> Protocols { get; set; } = null!;

    /// <inheritdoc />
    public DbSet<CommunicationEntity> Communications { get; set; } = null!;

    /// <inheritdoc />
    public DbSet<RelayAlgorithmEntity> RelayAlgorithms { get; set; } = null!;

    #endregion

    #region [ Views ]

    /// <inheritdoc />
    public DbSet<LastProjectRevisionView> LastProjectRevisions { get; set; } = null!;

    /// <inheritdoc />
    public DbSet<AuthorContributionView> AuthorContributions { get; set; } = null!;

    /// <inheritdoc />
    public DbSet<AuthorProjectContributionView> AuthorProjectContributions { get; set; } = null!;

    /// <inheritdoc />
    public DbSet<ProjectHistoryRecordView> ProjectHistoryRecords { get; set; } = null!;

    #endregion

    /// <inheritdoc />
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

#pragma warning disable CA2254 // Template should be a static expression
        optionsBuilder.LogTo(
            message => _logger.LogInformation(message),
            LogLevel.Trace,
            DbContextLoggerOptions.DefaultWithUtcTime);
#pragma warning restore CA2254 // Template should be a static expression
    }

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        if (Database.IsNpgsql())
        {
            modelBuilder.HasDefaultSchema(Schema);
        }

        new AnalogModuleConfiguration().Configure(modelBuilder.Entity<AnalogModuleEntity>());
        new ArmEditConfiguration().Configure(modelBuilder.Entity<ArmEditEntity>());
        new AuthorConfiguration().Configure(modelBuilder.Entity<AuthorEntity>());
        new ProtocolConfiguration().Configure(modelBuilder.Entity<ProtocolEntity>());
        new CommunicationConfiguration().Configure(modelBuilder.Entity<CommunicationEntity>());
        new PlatformConfiguration().Configure(modelBuilder.Entity<PlatformEntity>());
        new RelayAlgorithmConfiguration().Configure(modelBuilder.Entity<RelayAlgorithmEntity>());
        new ProjectVersionConfiguration().Configure(modelBuilder.Entity<ProjectVersionEntity>());
        new ProjectStatusConfiguration().Configure(modelBuilder.Entity<ProjectStatusEntity>());
        new ProjectRevisionConfiguration().Configure(modelBuilder.Entity<ProjectRevisionEntity>());
        new LastProjectRevisionConfiguration().Configure(modelBuilder.Entity<LastProjectRevisionView>());
        new AuthorContributionConfiguration().Configure(modelBuilder.Entity<AuthorContributionView>());
        new AuthorProjectContributionConfiguration().Configure(modelBuilder.Entity<AuthorProjectContributionView>());
        new ProjectHistoryRecordConfiguration().Configure(modelBuilder.Entity<ProjectHistoryRecordView>());
    }
}