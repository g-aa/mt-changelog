using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mt.ChangeLog.Entities.Views;
using Mt.Utilities;

namespace Mt.ChangeLog.DataContext.Configurations;

/// <summary>
/// Конфигуратор сущности <see cref="ProjectHistoryRecordView"/>.
/// </summary>
public class ProjectHistoryRecordConfiguration : IEntityTypeConfiguration<ProjectHistoryRecordView>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<ProjectHistoryRecordView> builder)
    {
        Check.NotNull(builder, nameof(builder));

        builder.ToView("ProjectHistoryRecord");
#pragma warning disable CS0618 // Type or member is obsolete
        builder.HasComment("Представление с перечнем информации о отдельной редакции проекта (БФПО) БМРЗ-100/120/150/160");
#pragma warning restore CS0618 // Type or member is obsolete
        builder.HasNoKey();
    }
}