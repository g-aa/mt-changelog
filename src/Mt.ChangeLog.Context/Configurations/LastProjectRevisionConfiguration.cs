using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mt.ChangeLog.Entities.Views;
using Mt.Utilities;

namespace Mt.ChangeLog.Context.Configurations
{
    /// <summary>
    /// Конфигуратор сущности <see cref="LastProjectRevisionView"/>.
    /// </summary>
    internal sealed class LastProjectRevisionConfiguration : IEntityTypeConfiguration<LastProjectRevisionView>
    {
        /// <inheritdoc />
        public void Configure(EntityTypeBuilder<LastProjectRevisionView> builder)
        {
            Check.NotNull(builder, nameof(builder));

            builder.ToView("LastProjectsRevision");
            builder.HasComment("Представление с перечнем информации о последних редакциях проектов БМРЗ-100/120/150/160");
            builder.HasNoKey();
        }
    }
}