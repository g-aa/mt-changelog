using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mt.ChangeLog.Entities.Views;

namespace Mt.ChangeLog.DataContext.Configurations;

/// <summary>
/// Конфигуратор сущности <see cref="AuthorProjectContributionView"/>.
/// </summary>
internal sealed class AuthorProjectContributionConfiguration : IEntityTypeConfiguration<AuthorProjectContributionView>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<AuthorProjectContributionView> builder)
    {
        builder.ToView("AuthorProjectContribution")
            .HasNoKey();
    }
}