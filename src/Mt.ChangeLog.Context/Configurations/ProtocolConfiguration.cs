using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mt.ChangeLog.Entities.Tables;
using Mt.Utilities;

namespace Mt.ChangeLog.Context.Configurations
{
    /// <summary>
    /// Конфигуратор сущности <see cref="Protocol"/>.
    /// </summary>
    internal sealed class ProtocolConfiguration : IEntityTypeConfiguration<Protocol>
    {
        /// <inheritdoc />
        public void Configure(EntityTypeBuilder<Protocol> builder)
        {
            Check.NotNull(builder, nameof(builder));

            builder.ToTable("Protocol");
            builder.HasComment("Таблица с перечнем протоколов информационного обмена поддерживаемых в блоках БМРЗ-100/120/150/160");
            builder.HasIndex(e => e.Title).HasDatabaseName("IX_Protocol_Title").IsUnique();

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
}