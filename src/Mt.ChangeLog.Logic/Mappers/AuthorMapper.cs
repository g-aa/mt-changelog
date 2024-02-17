using Mt.ChangeLog.Entities.Tables;
using Mt.ChangeLog.TransferObjects.Author;

namespace Mt.ChangeLog.Logic.Mappers;

/// <summary>
/// Методы расширения для класса <see cref="AuthorEntity"/>.
/// </summary>
public static class AuthorMapper
{
    /// <summary>
    /// Преобразовать сущность <see cref="AuthorEntity"/> в модель <see cref="AuthorShortModel"/>.
    /// </summary>
    /// <param name="entity">Сущность.</param>
    /// <returns>Модель.</returns>
    public static AuthorShortModel ToShortModel(this AuthorEntity entity)
    {
        return new AuthorShortModel
        {
            Id = entity.Id,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
        };
    }

    /// <summary>
    /// Преобразовать сущность <see cref="AuthorEntity"/> в модель <see cref="AuthorModel"/>.
    /// </summary>
    /// <param name="entity">Сущность.</param>
    /// <returns>Модель.</returns>
    public static AuthorModel ToModel(this AuthorEntity entity)
    {
        return new AuthorModel
        {
            Id = entity.Id,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            Position = entity.Position,
        };
    }
}