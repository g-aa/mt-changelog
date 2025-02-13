using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Mt.ChangeLog.Entities.Tables;

namespace Mt.ChangeLog.DataContext.Configurations;

/// <summary>
/// Конфигуратор сущности <see cref="ProjectRevisionEntity"/>.
/// </summary>
internal sealed class ProjectRevisionConfiguration : IEntityTypeConfiguration<ProjectRevisionEntity>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<ProjectRevisionEntity> builder)
    {
        builder.ToTable(
            "ProjectRevision",
            t => t.HasComment("Таблица с перечнем ревизий (редакций) проектов блоков БМРЗ"));

        builder.HasIndex(e => new { e.ProjectVersionId, e.Revision })
            .HasDatabaseName("IX_ProjectRevision_Revision")
            .IsUnique();

        builder.HasMany(pr => pr.Authors)
            .WithMany(a => a.ProjectRevisions)
            .UsingEntity(e => e.ToTable("ProjectRevisionAuthor"));

        builder.HasMany(pr => pr.RelayAlgorithms)
            .WithMany(ra => ra.ProjectRevisions)
            .UsingEntity(e => e.ToTable("ProjectRevisionRelayAlgorithm"));

        builder.Property(e => e.Id)
            .HasComment("Идентификатор");

        builder.Property(e => e.Date)
            .HasComment("Дата компиляции")
            .HasColumnType("timestamp without time zone")
            .IsRequired();

        builder.Property(e => e.Revision)
            .HasComment("Редакция")
            .HasMaxLength(2)
            .IsFixedLength()
            .IsRequired();

        builder.Property(e => e.Reason)
            .HasComment("Причина изменений")
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(e => e.Description)
            .HasComment("Описание")
            .HasMaxLength(5000)
            .IsRequired();
    }
}