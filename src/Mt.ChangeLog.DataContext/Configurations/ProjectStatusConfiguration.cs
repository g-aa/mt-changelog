using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mt.ChangeLog.Entities.Tables;

namespace Mt.ChangeLog.DataContext.Configurations;

/// <summary>
/// Конфигуратор сущности <see cref="ProjectStatusEntity"/>.
/// </summary>
internal sealed class ProjectStatusConfiguration : IEntityTypeConfiguration<ProjectStatusEntity>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<ProjectStatusEntity> builder)
    {
        builder.ToTable(
            "ProjectStatus",
            t => t.HasComment("Таблица со статусами проектов (БФПО)"));

        builder.HasIndex(e => e.Title)
            .HasDatabaseName("IX_ProjectStatus_Title")
            .IsUnique();

        builder.Property(e => e.Title)
            .HasMaxLength(32)
            .IsRequired();

        builder.Property(e => e.Description)
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(e => e.Default)
            .HasDefaultValue(false)
            .IsRequired();

        builder.Property(e => e.Removable)
            .HasDefaultValue(false)
            .IsRequired();
    }
}