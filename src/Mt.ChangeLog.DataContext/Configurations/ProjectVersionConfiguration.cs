using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mt.ChangeLog.Entities.Tables;
using Mt.Utilities;

namespace Mt.ChangeLog.DataContext.Configurations;

/// <summary>
/// Конфигуратор сущности <see cref="ProjectVersionEntity"/>.
/// </summary>
internal sealed class ProjectVersionConfiguration : IEntityTypeConfiguration<ProjectVersionEntity>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<ProjectVersionEntity> builder)
    {
        Check.NotNull(builder, nameof(builder));

        builder.ToTable(
            "ProjectVersion",
            t => t.HasComment("Таблица с перечнем проектов блоков БМРЗ-100/120/150/160"));

        builder.HasIndex(e => e.DIVG)
            .HasDatabaseName("IX_ProjectVersion_DIVG")
            .IsUnique();

        builder.HasIndex(e => new { e.Prefix, e.Title, e.Version })
            .HasDatabaseName("IX_ProjectVersion_Version")
            .IsUnique();

        builder.Property(e => e.DIVG)
            .HasMaxLength(13)
            .IsFixedLength()
            .IsRequired();

        builder.Property(e => e.Prefix)
            .HasMaxLength(10)
            .IsRequired();

        builder.Property(e => e.Title)
            .HasMaxLength(16)
            .IsRequired();

        builder.Property(e => e.Version)
            .HasMaxLength(2)
            .IsFixedLength()
            .IsRequired();

        builder.Property(e => e.Description)
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(e => e.AnalogModuleId)
            .IsRequired();

        builder.Property(e => e.PlatformId)
            .IsRequired();

        builder.Property(e => e.ProjectStatusId)
            .IsRequired();
    }
}