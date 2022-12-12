using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mt.ChangeLog.Entities.Views;
using Mt.Utilities;

namespace Mt.ChangeLog.Context.Configurations
{
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
            builder.HasComment("Представление с перечнем информации о отдельной редакции проекта (БФПО) БМРЗ-100/120/150/160");
            builder.HasNoKey();
        }
    }
}