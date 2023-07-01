using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mt.ChangeLog.Entities.Tables;
using Mt.Utilities;

namespace Mt.ChangeLog.Context.Configurations
{
    /// <summary>
    /// Конфигуратор сущности <see cref="ArmEditEntity"/>.
    /// </summary>
    internal sealed class ArmEditConfiguration : IEntityTypeConfiguration<ArmEditEntity>
    {
        /// <inheritdoc />
        public void Configure(EntityTypeBuilder<ArmEditEntity> builder)
        {
            Check.NotNull(builder, nameof(builder));

            builder.ToTable("ArmEdit");
            builder.HasComment("Таблица с перечнем ArmEdit используемых при компиляции проектов блоков БМРЗ-100/120/150/160");
            builder.HasIndex(e => e.Version).HasDatabaseName("IX_ArmEdit_Version").IsUnique();
            // builder.HasIndex(e => e.DIVG).HasDatabaseName("IX_ArmEdit_DIVG").IsUnique(); // точных данных по ДИВГ нет

            builder.Property(e => e.DIVG)
                .HasDefaultValue("ДИВГ.55101-00")
                .HasMaxLength(13)
                .IsFixedLength()
                .IsRequired();

            builder.Property(e => e.Version)
                .HasMaxLength(11)
                .IsFixedLength()
                .IsRequired();

            builder.Property(e => e.Date)
                .HasColumnType("timestamp without time zone")
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