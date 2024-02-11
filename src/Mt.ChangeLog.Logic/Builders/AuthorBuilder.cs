using Mt.ChangeLog.Entities.Tables;
using Mt.ChangeLog.TransferObjects.Author;

namespace Mt.ChangeLog.Logic.Builders;

/// <summary>
/// Строитель <see cref="AuthorEntity"/>.
/// </summary>
public sealed class AuthorBuilder
{
    private readonly AuthorEntity _entity;

    private string _firstName;

    private string _lastName;

    private string _position;

    /// <summary>
    /// Инициализация экземпляра класса <see cref="AuthorBuilder"/>.
    /// </summary>
    /// <param name="entity">Сущность.</param>
    public AuthorBuilder(AuthorEntity entity)
    {
        _entity = entity;
        _firstName = entity.FirstName;
        _lastName = entity.LastName;
        _position = entity.Position;
    }

    /// <summary>
    /// Добавить атрибуты.
    /// </summary>
    /// <param name="model">Модель.</param>
    /// <returns>Строитель.</returns>
    public AuthorBuilder SetAttributes(AuthorModel model)
    {
        _firstName = model.FirstName;
        _lastName = model.LastName;
        _position = model.Position;
        return this;
    }

    /// <summary>
    /// Построить сущность.
    /// </summary>
    /// <returns>Сущность.</returns>
    public AuthorEntity Build()
    {
        // атрибуты:
        // _entity.Id - не обновляется!
        _entity.FirstName = _firstName;
        _entity.LastName = _lastName;
        _entity.Position = _position;

        // реляционные связи:
        // _entity.ProjectRevisions - не обновляется!
        return _entity;
    }
}