using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mt.ChangeLog.Entities.Tables;

namespace Mt.ChangeLog.DataContext.Configurations;

/// <summary>
/// Конфигуратор сущности <see cref="ProtocolEntity"/>.
/// </summary>
internal sealed class ProtocolConfiguration : IEntityTypeConfiguration<ProtocolEntity>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<ProtocolEntity> builder)
    {
        builder.ToTable(
            "Protocol",
            t => t.HasComment("Таблица с перечнем протоколов информационного обмена поддерживаемых в блоках БМРЗ"));

        builder.HasIndex(e => e.Title)
            .HasDatabaseName("IX_Protocol_Title")
            .IsUnique();

        builder.Property(e => e.Id)
            .HasComment("Идентификатор");

        builder.Property(e => e.Title)
            .HasComment("Наименование")
            .HasMaxLength(32)
            .IsRequired();

        builder.Property(e => e.Description)
            .HasComment("Описание")
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(e => e.Default)
            .HasComment("Признак значения по умолчанию")
            .HasDefaultValue(false)
            .IsRequired();

        builder.Property(e => e.Removable)
            .HasComment("Возможность удалить")
            .HasDefaultValue(false)
            .IsRequired();
    }
}