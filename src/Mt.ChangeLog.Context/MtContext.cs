using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Mt.ChangeLog.Entities.Tables;
using Mt.ChangeLog.Entities.Views;
using Mt.ChangeLog.Context.Configurations;

namespace Mt.ChangeLog.Context
{
    /// <summary>
    /// Контекст данных приложения.
    /// </summary>
    public sealed class MtContext : DbContext
    {
        /// <summary>
        /// Схема базы данных: ChangeLog.
        /// </summary>
        public const string Schema = "changelog";

        /// <summary>
        /// Журнал логирования.
        /// </summary>
        private readonly ILogger<MtContext> logger;

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

        /// <summary>
        /// Инициализация экземпляра класса <see cref="MtContext"/>.
        /// </summary>
        /// <param name="options">Настройки.</param>
        /// <param name="logger">Журнал логирования.</param>
        public MtContext(DbContextOptions<MtContext> options, ILogger<MtContext> logger) : base(options)
        {
            this.logger = logger;
        }

        /// <inheritdoc />
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            
            optionsBuilder.LogTo((message) =>
            {
                this.logger.LogInformation(message);
            },
            LogLevel.Information,
            DbContextLoggerOptions.DefaultWithUtcTime);
        }

        /// <inheritdoc />
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            if (this.Database.IsNpgsql())
            {
                modelBuilder.HasDefaultSchema(MtContext.Schema);
            }

            #region [ Tables ]

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

            #endregion

            #region [ Views ]

            new LastProjectRevisionConfiguration().Configure(modelBuilder.Entity<LastProjectRevisionView>());
            new AuthorContributionConfiguration().Configure(modelBuilder.Entity<AuthorContributionView>());
            new AuthorProjectContributionConfiguration().Configure(modelBuilder.Entity<AuthorProjectContributionView>());
            new ProjectHistoryRecordConfiguration().Configure(modelBuilder.Entity<ProjectHistoryRecordView>());

            #endregion
        }
    }
}