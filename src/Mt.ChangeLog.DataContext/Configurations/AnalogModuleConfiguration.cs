using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mt.ChangeLog.Entities.Tables;
using Mt.Utilities;

namespace Mt.ChangeLog.DataContext.Configurations;

/// <summary>
/// Конфигуратор сущности <see cref="AnalogModuleEntity"/>.
/// </summary>
internal sealed class AnalogModuleConfiguration : IEntityTypeConfiguration<AnalogModuleEntity>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<AnalogModuleEntity> builder)
    {
        builder.ToTable(
            "AnalogModule",
            t => t.HasComment("Таблица с перечнем аналоговых модулей используемых в блоках БМРЗ"));

        builder.HasIndex(e => e.Title)
            .HasDatabaseName("IX_AnalogModule_Title")
            .IsUnique();

        /***
         * builder.HasIndex(e => e.DIVG)
         *     .HasDatabaseName("IX_AnalogModule_DIVG")
         *     .IsUnique();
         */

        builder.HasMany(am => am.Platforms)
            .WithMany(p => p.AnalogModules)
            .UsingEntity(e => e.ToTable("PlatformAnalogModule"));

        builder.Property(e => e.Id)
            .HasComment("Идентификатор");

        builder.Property(e => e.DIVG)
            .HasComment("ДИВГ")
            .HasDefaultValue(DefaultString.DIVG)
            .HasMaxLength(13)
            .IsFixedLength()
            .IsRequired();

        builder.Property(e => e.Title)
            .HasComment("Наименование")
            .HasMaxLength(10)
            .IsRequired();

        builder.Property(e => e.Current)
            .HasComment("Номинальный ток")
            .HasDefaultValue(DefaultString.Current)
            .HasMaxLength(2)
            .IsFixedLength()
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