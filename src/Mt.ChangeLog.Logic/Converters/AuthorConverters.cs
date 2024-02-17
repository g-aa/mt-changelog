using Mt.ChangeLog.Entities.Tables;
using Mt.ChangeLog.Entities.Views;
using Mt.ChangeLog.TransferObjects.Author;

namespace Mt.ChangeLog.Logic.Converters;

/// <summary>
/// Преобразователь объектов для authors.
/// </summary>
public static class AuthorConverters
{
    /// <summary>
    /// Преобразовать сущность <see cref="AuthorContributionView"/> в модель <see cref="AuthorContributionModel"/>.
    /// </summary>
    public sealed class EntityToContributionModelConverter : IConverter<AuthorContributionView, AuthorContributionModel>
    {
        /// <inheritdoc />
        public AuthorContributionModel Convert(AuthorContributionView source)
        {
            return new AuthorContributionModel
            {
                Author = source.Author,
                Contribution = source.Contribution,
            };
        }
    }

    /// <summary>
    /// Преобразовать сущность <see cref="AuthorProjectContributionView"/> в модель <see cref="AuthorProjectContributionModel"/>.
    /// </summary>
    public sealed class EntityToProjectContributionModelConverter : IConverter<AuthorProjectContributionView, AuthorProjectContributionModel>
    {
        /// <inheritdoc />
        public AuthorProjectContributionModel Convert(AuthorProjectContributionView source)
        {
            return new AuthorProjectContributionModel
            {
                Author = source.Author,
                Contribution = source.Contribution,
                ProjectPrefix = source.ProjectPrefix,
                ProjectTitle = source.ProjectTitle,
                ProjectVersion = source.ProjectVersion,
            };
        }
    }

    /// <summary>
    /// Преобразовать сущность <see cref="AuthorEntity"/> в модель <see cref="AuthorShortModel"/>.
    /// </summary>
    public sealed class EntityToShortModelConverter : IConverter<AuthorEntity, AuthorShortModel>
    {
        /// <inheritdoc />
        public AuthorShortModel Convert(AuthorEntity source)
        {
            return new AuthorShortModel
            {
                Id = source.Id,
                FirstName = source.FirstName,
                LastName = source.LastName,
            };
        }
    }

    /// <summary>
    /// Преобразовать сущность <see cref="AuthorEntity"/> в модель <see cref="AuthorModel"/>.
    /// </summary>
    public sealed class EntityToModelConverter : IConverter<AuthorEntity, AuthorModel>
    {
        /// <inheritdoc />
        public AuthorModel Convert(AuthorEntity source)
        {
            return new AuthorModel
            {
                Id = source.Id,
                FirstName = source.FirstName,
                LastName = source.LastName,
                Position = source.Position,
            };
        }
    }
}