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
            t => t.HasComment("Таблица с перечнем программных платформ применяемых в блоках БМРЗ"));

        builder.HasIndex(e => e.Title)
            .HasDatabaseName("IX_Platform_Title")
            .IsUnique();

        builder.Property(e => e.Id)
            .HasComment("Идентификатор");

        builder.Property(e => e.Title)
            .HasComment("Наименование")
            .HasMaxLength(10)
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