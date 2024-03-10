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
public sealed partial class MtContext : DbContext
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

        modelBuilder.HasDefaultSchema(Schema);
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