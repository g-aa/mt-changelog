using Mt.ChangeLog.Entities.Views;
using Mt.ChangeLog.TransferObjects.Author;

namespace Mt.ChangeLog.Logic.Mappers;

/// <summary>
/// Методы расширения для класса <see cref="AuthorContributionView"/>.
/// </summary>
public static class AuthorContributionMapper
{
    /// <summary>
    /// Преобразовать сущность <see cref="AuthorContributionView"/> в модель <see cref="AuthorContributionModel"/>.
    /// </summary>
    /// <param name="entity">Сущность.</param>
    /// <returns>Модель.</returns>
    public static AuthorContributionModel ToModel(this AuthorContributionView entity)
    {
        return new AuthorContributionModel
        {
            Author = entity.Author,
            Contribution = entity.Contribution,
        };
    }

    /// <summary>
    /// Преобразовать сущность <see cref="AuthorProjectContributionView"/> в модель <see cref="AuthorProjectContributionModel"/>.
    /// </summary>
    /// <param name="entity">Сущность.</param>
    /// <returns>Модель.</returns>
    public static AuthorProjectContributionModel ToModel(this AuthorProjectContributionView entity)
    {
        return new AuthorProjectContributionModel
        {
            Author = entity.Author,
            Contribution = entity.Contribution,
            ProjectPrefix = entity.ProjectPrefix,
            ProjectTitle = entity.ProjectTitle,
            ProjectVersion = entity.ProjectVersion,
        };
    }
}