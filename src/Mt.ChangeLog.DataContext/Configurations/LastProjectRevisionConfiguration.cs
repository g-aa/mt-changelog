using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mt.ChangeLog.Entities.Views;

namespace Mt.ChangeLog.DataContext.Configurations;

/// <summary>
/// Конфигуратор сущности <see cref="LastProjectRevisionView"/>.
/// </summary>
internal sealed class LastProjectRevisionConfiguration : IEntityTypeConfiguration<LastProjectRevisionView>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<LastProjectRevisionView> builder)
    {
        builder.ToView("LastProjectsRevision");
#pragma warning disable CS0618 // Type or member is obsolete
        builder.HasComment("Представление с перечнем информации о последних редакциях проектов БМРЗ-100/120/150/160");
#pragma warning restore CS0618 // Type or member is obsolete
        builder.HasNoKey();
    }
}