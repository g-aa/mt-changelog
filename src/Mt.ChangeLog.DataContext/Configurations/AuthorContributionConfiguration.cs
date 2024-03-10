using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mt.ChangeLog.Entities.Views;

namespace Mt.ChangeLog.DataContext.Configurations;

/// <summary>
/// Конфигуратор сущности <see cref="AuthorContributionView"/>.
/// </summary>
internal sealed class AuthorContributionConfiguration : IEntityTypeConfiguration<AuthorContributionView>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<AuthorContributionView> builder)
    {
        builder.ToView("AuthorContribution")
            .HasNoKey();
    }
}