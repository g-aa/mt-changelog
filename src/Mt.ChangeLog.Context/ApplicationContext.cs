using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Mt.ChangeLog.Entities.Tables;
using Mt.ChangeLog.Entities.Views;

namespace Mt.ChangeLog.Context
{
    /// <summary>
    /// Контекст данных приложения.
    /// </summary>
    public sealed class ApplicationContext : DbContext
    {
        /// <summary>
        /// Журнал логирования.
        /// </summary>
        private readonly ILogger<ApplicationContext> logger;

        #region [ ProjectVersionEntities ]

        /// <summary>
        /// Перечень <see cref="Platform"/>.
        /// </summary>
        public DbSet<Platform> Platforms { get; set; }

        /// <summary>
        /// Перечень <see cref="AnalogModule"/>. 
        /// </summary>
        public DbSet<AnalogModule> AnalogModules { get; set; }

        /// <summary>
        /// Перечень <see cref="ProjectStatus"/>. 
        /// </summary>
        public DbSet<ProjectStatus> ProjectStatuses { get; set; }

        /// <summary>
        /// Перечень <see cref="ProjectVersion"/>. 
        /// </summary>
        public DbSet<ProjectVersion> ProjectVersions { get; set; }
        #endregion

        #region [ ProjectRevisionEntities ]

        /// <summary>
        /// Перечень <see cref="ProjectRevision"/>. 
        /// </summary>
        public DbSet<ProjectRevision> ProjectRevisions { get; set; }

        /// <summary>
        /// Перечень <see cref="ArmEdit"/>. 
        /// </summary>
        public DbSet<ArmEdit> ArmEdits { get; set; }

        /// <summary>
        /// Перечень <see cref="Author"/>. 
        /// </summary>
        public DbSet<Author> Authors { get; set; }

        /// <summary>
        /// Перечень <see cref="Protocol"/>. 
        /// </summary>
        public DbSet<Protocol> Protocols { get; set; }

        /// <summary>
        /// Перечень <see cref="Communication"/>. 
        /// </summary>
        public DbSet<Communication> Communications { get; set; }

        /// <summary>
        /// Перечень <see cref="RelayAlgorithm"/>. 
        /// </summary>
        public DbSet<RelayAlgorithm> RelayAlgorithms { get; set; }
        #endregion

        #region [ Views ]

        /// <summary>
        /// Перечень <see cref="LastProjectRevision"/>. 
        /// </summary>
        public DbSet<LastProjectRevision> LastProjectRevisions { get; set; }

        /// <summary>
        /// Перечень <see cref="AuthorContribution"/>. 
        /// </summary>
        public DbSet<AuthorContribution> AuthorContributions { get; set; }

        /// <summary>
        /// Перечень <see cref="AuthorProjectContribution"/>. 
        /// </summary>
        public DbSet<AuthorProjectContribution> AuthorProjectContributions { get; set; }

        /// <summary>
        /// Перечень <see cref="ProjectHistoryRecord"/>. 
        /// </summary>
        public DbSet<ProjectHistoryRecord> ProjectHistoryRecords { get; set; }
        #endregion

        /// <summary>
        /// Инициализация экземпляра класса <see cref="ApplicationContext"/>.
        /// </summary>
        /// <param name="options">Настройки.</param>
        /// <param name="logger">Журнал логирования.</param>
        public ApplicationContext(DbContextOptions<ApplicationContext> options, ILogger<ApplicationContext> logger) : base(options)
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
                modelBuilder.HasDefaultSchema("MT");
            }
        }
    }
}