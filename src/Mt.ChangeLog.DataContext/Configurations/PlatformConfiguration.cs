using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mt.ChangeLog.Entities.Tables;

namespace Mt.ChangeLog.DataContext.Configurations;

/// <summary>
/// Конфигуратор сущности <see cref="PlatformEntity"/>.
/// </summary>
internal sealed class PlatformConfiguration : IEntityTypeConfiguration<PlatformEntity>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<PlatformEntity> builder)
    {
        builder.ToTable(
            "Platform",
            t => t.HasComment("Таблица с перечнем программных платформ применяемых в блоках БМРЗ-100/120/150/160"));

        builder.HasIndex(e => e.Title)
            .HasDatabaseName("IX_Platform_Title")
            .IsUnique();

        builder.Property(e => e.Title)
            .HasMaxLength(10)
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