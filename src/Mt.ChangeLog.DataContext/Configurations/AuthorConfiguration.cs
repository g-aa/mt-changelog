using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mt.ChangeLog.Entities.Tables;

namespace Mt.ChangeLog.DataContext.Configurations;

/// <summary>
/// Конфигуратор сущности <see cref="AuthorEntity"/>.
/// </summary>
internal sealed class AuthorConfiguration : IEntityTypeConfiguration<AuthorEntity>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<AuthorEntity> builder)
    {
        builder.ToTable(
            "Author",
            t => t.HasComment("Таблица с перечнем авторов проектов и ревизий встраиваемого ПО блоков БМРЗ"));

        builder.HasIndex(e => new { e.FirstName, e.LastName })
            .HasDatabaseName("IX_Author_Name")
            .IsUnique();

        builder.Property(e => e.Id)
            .HasComment("Идентификатор");

        builder.Property(e => e.FirstName)
            .HasComment("Имя")
            .HasMaxLength(32)
            .IsRequired();

        builder.Property(e => e.LastName)
            .HasComment("Фамилия")
            .HasMaxLength(32)
            .IsRequired();

        builder.Property(e => e.Position)
            .HasComment("Должность")
            .HasMaxLength(250)
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