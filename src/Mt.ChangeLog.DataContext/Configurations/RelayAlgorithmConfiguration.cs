using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mt.ChangeLog.Entities.Tables;

namespace Mt.ChangeLog.DataContext.Configurations;

/// <summary>
/// Конфигуратор сущности <see cref="RelayAlgorithmEntity"/>.
/// </summary>
internal sealed class RelayAlgorithmConfiguration : IEntityTypeConfiguration<RelayAlgorithmEntity>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<RelayAlgorithmEntity> builder)
    {
        builder.ToTable(
            "RelayAlgorithm",
            t => t.HasComment("Таблица с перечнем алгоритмов РЗиА поддерживаемых в блоках БМРЗ"));

        builder.HasIndex(e => e.Title)
            .HasDatabaseName("IX_RelayAlgorithm_Title")
            .IsUnique();

        /***
         * builder.HasIndex(e => e.ANSI)
         *     .HasDatabaseName("IX_RelayAlgorithm_ANSI")
         *     .IsUnique();
         *
         * builder.HasIndex(e => e.LogicalNode)
         *     .HasDatabaseName("IX_RelayAlgorithm_LN")
         *     .IsUnique();
         */

        builder.Property(e => e.Id)
            .HasComment("Идентификатор");

        builder.Property(e => e.Group)
            .HasComment("Наименование группы")
            .HasMaxLength(32)
            .IsRequired();

        builder.Property(e => e.Title)
            .HasComment("Наименование")
            .HasMaxLength(32)
            .IsRequired();

        builder.Property(e => e.ANSI)
            .HasComment("Код ANSI")
            .HasMaxLength(32)
            .IsRequired();

        builder.Property(e => e.LogicalNode)
            .HasComment("Логический узел IEC-61850")
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