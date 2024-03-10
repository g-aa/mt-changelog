using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mt.ChangeLog.Entities.Tables;

namespace Mt.ChangeLog.DataContext.Configurations;

/// <summary>
/// Конфигуратор сущности <see cref="CommunicationEntity"/>.
/// </summary>
internal sealed class CommunicationConfiguration : IEntityTypeConfiguration<CommunicationEntity>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<CommunicationEntity> builder)
    {
        builder.ToTable(
            "Communication",
            t => t.HasComment("Таблица с перечнем коммуникационных модулей поддерживаемых в блоках БМРЗ"));

        builder.HasIndex(e => e.Title)
            .HasDatabaseName("IX_Communication_Title")
            .IsUnique();

        builder.HasMany(ca => ca.Protocols)
            .WithMany(p => p.Communications)
            .UsingEntity(e => e.ToTable("CommunicationProtocol"));

        builder.Property(e => e.Id)
            .HasComment("Идентификатор");

        builder.Property(e => e.Title)
            .HasComment("Наименование")
            .HasMaxLength(255)
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