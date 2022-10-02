using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mt.ChangeLog.Entities.Tables;
using Mt.Utilities;

namespace Mt.ChangeLog.Context.Configurations
{
    /// <summary>
    /// Конфигуратор сущности <see cref="ProjectRevision"/>.
    /// </summary>
    internal sealed class ProjectRevisionConfiguration : IEntityTypeConfiguration<ProjectRevision>
    {
        /// <inheritdoc />
        public void Configure(EntityTypeBuilder<ProjectRevision> builder)
        {
            Check.NotNull(builder, nameof(builder));

            builder.ToTable("ProjectRevision");
            builder.HasComment("Таблица с перечнем ревизий (редакций) проектов блоков БМРЗ-100/120/150/160");
            builder.HasIndex(e => new { e.ProjectVersionId, e.Revision }).HasDatabaseName("IX_ProjectRevision_Revision").IsUnique();

            builder.HasMany(pr => pr.Authors)
                .WithMany(a => a.ProjectRevisions)
                .UsingEntity(e => e.ToTable("ProjectRevisionAuthor"));

            builder.HasMany(pr => pr.RelayAlgorithms)
                .WithMany(ra => ra.ProjectRevisions)
                .UsingEntity(e => e.ToTable("ProjectRevisionRelayAlgorithm"));

            builder.Property(e => e.Date)
                .HasColumnType("timestamp without time zone")
                .IsRequired();

            builder.Property(e => e.Revision)
                .HasMaxLength(2)
                .IsFixedLength()
                .IsRequired();

            builder.Property(e => e.Reason)
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(e => e.Description)
                .HasMaxLength(5000)
                .IsRequired();
        }
    }
}