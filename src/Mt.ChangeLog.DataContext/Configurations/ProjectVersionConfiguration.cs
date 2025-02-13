using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Mt.ChangeLog.Entities.Tables;

namespace Mt.ChangeLog.DataContext.Configurations;

/// <summary>
/// Конфигуратор сущности <see cref="ProjectVersionEntity"/>.
/// </summary>
internal sealed class ProjectVersionConfiguration : IEntityTypeConfiguration<ProjectVersionEntity>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<ProjectVersionEntity> builder)
    {
        builder.ToTable(
            "ProjectVersion",
            t => t.HasComment("Таблица с перечнем проектов блоков БМРЗ"));

        builder.HasIndex(e => e.DIVG)
            .HasDatabaseName("IX_ProjectVersion_DIVG")
            .IsUnique();

        builder.HasIndex(e => new { e.Prefix, e.Title, e.Version })
            .HasDatabaseName("IX_ProjectVersion_Version")
            .IsUnique();

        builder.Property(e => e.Id)
            .HasComment("Идентификатор");

        builder.Property(e => e.DIVG)
            .HasComment("ДИВГ")
            .HasMaxLength(13)
            .IsFixedLength()
            .IsRequired();

        builder.Property(e => e.Prefix)
            .HasComment("Префикс")
            .HasMaxLength(10)
            .IsRequired();

        builder.Property(e => e.Title)
            .HasComment("Наименование")
            .HasMaxLength(16)
            .IsRequired();

        builder.Property(e => e.Version)
            .HasComment("Версия")
            .HasMaxLength(2)
            .IsFixedLength()
            .IsRequired();

        builder.Property(e => e.Description)
            .HasComment("Описание")
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(e => e.AnalogModuleId)
            .HasComment("Идентификатор аналогового модуля")
            .IsRequired();

        builder.Property(e => e.PlatformId)
            .HasComment("Идентификатор платформы")
            .IsRequired();

        builder.Property(e => e.ProjectStatusId)
            .HasComment("Идентификатор статуса проекта")
            .IsRequired();
    }
}