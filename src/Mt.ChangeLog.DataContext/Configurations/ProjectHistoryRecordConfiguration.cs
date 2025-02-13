using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Mt.ChangeLog.Entities.Views;

namespace Mt.ChangeLog.DataContext.Configurations;

/// <summary>
/// Конфигуратор сущности <see cref="ProjectHistoryRecordView"/>.
/// </summary>
internal sealed class ProjectHistoryRecordConfiguration : IEntityTypeConfiguration<ProjectHistoryRecordView>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<ProjectHistoryRecordView> builder)
    {
        builder.ToView("ProjectHistoryRecord")
            .HasNoKey();
    }
}