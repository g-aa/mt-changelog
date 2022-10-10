using Mt.ChangeLog.Entities.Views;
using Mt.ChangeLog.TransferObjects.Author;
using Mt.Utilities;

namespace Mt.ChangeLog.Entities.Extensions.Views
{
    /// <summary>
    /// Методы расширения для класса <see cref="AuthorContribution"/>.
    /// </summary>
    public static class AuthorContributionExtensions
    {
        /// <summary>
        /// Преобразовать сущность <see cref="AuthorContribution"/> в модель <see cref="AuthorContributionModel"/>.
        /// </summary>
        /// <param name="entity">Сущность.</param>
        /// <returns>Модель.</returns>
        public static AuthorContributionModel ToModel(this AuthorContribution entity)
        {
            Check.NotNull(entity, nameof(entity));
            var result = new AuthorContributionModel()
            {
                Author = entity.Author,
                Contribution = entity.Contribution
            };
            return result;
        }

        /// <summary>
        /// Преобразовать сущность <see cref="AuthorProjectContribution"/> в модель <see cref="AuthorProjectContributionModel"/>.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static AuthorProjectContributionModel ToModel(this AuthorProjectContribution entity)
        {
            Check.NotNull(entity, nameof(entity));
            var result = new AuthorProjectContributionModel()
            {
                Author = entity.Author,
                Contribution = entity.Contribution,
                ProjectPrefix = entity.ProjectPrefix,
                ProjectTitle = entity.ProjectTitle,
                ProjectVersion = entity.ProjectVersion
            };
            return result;
        }
    }
}