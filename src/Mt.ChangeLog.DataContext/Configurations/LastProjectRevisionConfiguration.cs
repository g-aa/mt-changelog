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
        builder.ToView("LastProjectsRevision")
            .HasNoKey();
    }
}