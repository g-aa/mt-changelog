using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mt.ChangeLog.Entities.Tables;
using Mt.Utilities;

namespace Mt.ChangeLog.Context.Configurations
{
    /// <summary>
    /// Конфигуратор сущности <see cref="RelayAlgorithmEntity"/>.
    /// </summary>
    internal sealed class RelayAlgorithmConfiguration : IEntityTypeConfiguration<RelayAlgorithmEntity>
    {
        /// <inheritdoc />
        public void Configure(EntityTypeBuilder<RelayAlgorithmEntity> builder)
        {
            Check.NotNull(builder, nameof(builder));

            builder.ToTable("RelayAlgorithm");
            builder.HasComment("Таблица с перечнем алгоритмов РЗиА поддерживаемых в блоках БМРЗ-100/120/150/160");
            builder.HasIndex(e => e.Title).HasDatabaseName("IX_RelayAlgorithm_Title").IsUnique();
            // builder.HasIndex(e => e.ANSI).HasDatabaseName("IX_RelayAlgorithm_ANSI").IsUnique(); // точных данных по кодам ANSI нет
            // builder.HasIndex(e => e.LogicalNode).HasDatabaseName("IX_RelayAlgorithm_LN").IsUnique(); // точных данных по наименованию LN в 61850 нет

            builder.Property(e => e.Group)
                .HasMaxLength(32)
                .IsRequired();

            builder.Property(e => e.Title)
                .HasMaxLength(32)
                .IsRequired();

            builder.Property(e => e.ANSI)
                .HasMaxLength(32)
                .IsRequired();

            builder.Property(e => e.LogicalNode)
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
}