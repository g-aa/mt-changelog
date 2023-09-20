using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mt.ChangeLog.Entities.Tables;
using Mt.Utilities;

namespace Mt.ChangeLog.DataContext.Configurations;

/// <summary>
/// Конфигуратор сущности <see cref="ProtocolEntity"/>.
/// </summary>
internal sealed class ProtocolConfiguration : IEntityTypeConfiguration<ProtocolEntity>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<ProtocolEntity> builder)
    {
        Check.NotNull(builder, nameof(builder));

        builder.ToTable(
            "Protocol",
            t => t.HasComment("Таблица с перечнем протоколов информационного обмена поддерживаемых в блоках БМРЗ-100/120/150/160"));

        builder.HasIndex(e => e.Title)
            .HasDatabaseName("IX_Protocol_Title")
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