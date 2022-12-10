using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mt.ChangeLog.Entities.Tables;
using Mt.Utilities;

namespace Mt.ChangeLog.Context.Configurations
{
    /// <summary>
    /// Конфигуратор сущности <see cref="AnalogModuleEntity"/>.
    /// </summary>
    internal sealed class AnalogModuleConfiguration : IEntityTypeConfiguration<AnalogModuleEntity>
    {
        /// <inheritdoc />
        public void Configure(EntityTypeBuilder<AnalogModuleEntity> builder)
        {
            Check.NotNull(builder, nameof(builder));

            builder.ToTable("AnalogModule");
            builder.HasComment("Таблица с перечнем аналоговых модулей используемых в блоках БМРЗ-100/120/150/160");
            builder.HasIndex(e => e.Title).HasDatabaseName("IX_AnalogModule_Title").IsUnique();
            // builder.HasIndex(e => e.DIVG).HasDatabaseName("IX_AnalogModule_DIVG").IsUnique(); // точных данных по ДИВГ нет

            builder.HasMany(am => am.Platforms)
                .WithMany(p => p.AnalogModules)
                .UsingEntity(e => e.ToTable("PlatformAnalogModule"));

            builder.Property(e => e.DIVG)
                .HasDefaultValue(DefaultString.DIVG)
                .HasMaxLength(13)
                .IsFixedLength()
                .IsRequired();

            builder.Property(e => e.Title)
                .HasMaxLength(10)
                .IsRequired();

            builder.Property(e => e.Current)
                .HasDefaultValue(DefaultString.Current)
                .HasMaxLength(2)
                .IsFixedLength()
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