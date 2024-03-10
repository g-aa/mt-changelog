using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mt.ChangeLog.Entities.Tables;

namespace Mt.ChangeLog.DataContext.Configurations;

/// <summary>
/// Конфигуратор сущности <see cref="ArmEditEntity"/>.
/// </summary>
internal sealed class ArmEditConfiguration : IEntityTypeConfiguration<ArmEditEntity>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<ArmEditEntity> builder)
    {
        builder.ToTable(
            "ArmEdit",
            t => t.HasComment("Таблица с перечнем ArmEdit используемых при компиляции проектов блоков БМРЗ"));

        builder.HasIndex(e => e.Version)
            .HasDatabaseName("IX_ArmEdit_Version")
            .IsUnique();

        /***
         * builder.HasIndex(e => e.DIVG)
         *     .HasDatabaseName("IX_ArmEdit_DIVG")
         *     .IsUnique();
         */

        builder.Property(e => e.Id)
            .HasComment("Идентификатор");

        builder.Property(e => e.Id)
            .HasComment("Идентификатор");

        builder.Property(e => e.DIVG)
            .HasComment("ДИВГ")
            .HasDefaultValue("ДИВГ.55101-00")
            .HasMaxLength(13)
            .IsFixedLength()
            .IsRequired();

        builder.Property(e => e.Version)
            .HasComment("Версия ArmEdit")
            .HasMaxLength(11)
            .IsFixedLength()
            .IsRequired();

        builder.Property(e => e.Date)
            .HasComment("Дата и время компиляции")
            .HasColumnType("timestamp without time zone")
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