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
        builder.ToView("AuthorProjectContribution");
#pragma warning disable CS0618 // Type or member is obsolete
        builder.HasComment("Представление, статистика по авторам и их вкладам в проекты БМРЗ-100/120/150/160");
#pragma warning restore CS0618 // Type or member is obsolete
        builder.HasNoKey();
    }
}