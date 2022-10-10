using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mt.ChangeLog.Entities.Views;
using Mt.Utilities;

namespace Mt.ChangeLog.Context.Configurations
{
    /// <summary>
    /// Конфигуратор сущности <see cref="AuthorContribution"/>.
    /// </summary>
    internal sealed class AuthorContributionConfiguration : IEntityTypeConfiguration<AuthorContribution>
    {
        /// <inheritdoc />
        public void Configure(EntityTypeBuilder<AuthorContribution> builder)
        {
            Check.NotNull(builder, nameof(builder));

            builder.ToView("AuthorContribution");
            builder.HasComment("Представление, общая статистика по авторам и их вкладам в проекты БМРЗ-100/120/150/160");
            builder.HasNoKey();
        }
    }
}