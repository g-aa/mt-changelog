using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mt.ChangeLog.Entities.Tables;
using Mt.Utilities;

namespace Mt.ChangeLog.Context.Configurations
{
    /// <summary>
    /// Конфигуратор сущности <see cref="Author"/>.
    /// </summary>
    internal sealed class AuthorConfiguration : IEntityTypeConfiguration<Author>
    {
        /// <inheritdoc />
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            Check.NotNull(builder, nameof(builder));

            builder.ToTable("Author");
            builder.HasComment("Таблица с перечнем авторов проектов и ревизий встраиваемого ПО блоков БМРЗ-100/120/150/160");
            builder.HasIndex(e => new { e.FirstName, e.LastName }).HasDatabaseName("IX_Author_Name").IsUnique();

            builder.Property(e => e.FirstName)
                .HasMaxLength(32)
                .IsRequired();

            builder.Property(e => e.LastName)
                .HasMaxLength(32)
                .IsRequired();

            builder.Property(e => e.Position)
                .HasMaxLength(250)
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