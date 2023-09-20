using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mt.ChangeLog.Entities.Views;
using Mt.Utilities;

namespace Mt.ChangeLog.DataContext.Configurations;

/// <summary>
/// Конфигуратор сущности <see cref="AuthorContributionView"/>.
/// </summary>
internal sealed class AuthorContributionConfiguration : IEntityTypeConfiguration<AuthorContributionView>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<AuthorContributionView> builder)
    {
        Check.NotNull(builder, nameof(builder));

        builder.ToView("AuthorContribution");
#pragma warning disable CS0618 // Type or member is obsolete
        builder.HasComment("Представление, общая статистика по авторам и их вкладам в проекты БМРЗ-100/120/150/160");
#pragma warning restore CS0618 // Type or member is obsolete
        builder.HasNoKey();
    }
}