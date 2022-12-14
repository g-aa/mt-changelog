using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mt.ChangeLog.Entities.Views;
using Mt.Utilities;

namespace Mt.ChangeLog.Context.Configurations
{
    /// <summary>
    /// Конфигуратор сущности <see cref="AuthorProjectContributionView"/>.
    /// </summary>
    internal sealed class AuthorProjectContributionConfiguration : IEntityTypeConfiguration<AuthorProjectContributionView>
    {
        /// <inheritdoc />
        public void Configure(EntityTypeBuilder<AuthorProjectContributionView> builder)
        {
            Check.NotNull(builder, nameof(builder));

            builder.ToView("AuthorProjectContribution");
            builder.HasComment("Представление, статистика по авторам и их вкладам в проекты БМРЗ-100/120/150/160");
            builder.HasNoKey();
        }
    }
}