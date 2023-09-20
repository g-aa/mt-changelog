using Mt.ChangeLog.Entities.Tables;
using Mt.ChangeLog.TransferObjects.Author;

namespace Mt.ChangeLog.Logic.Builders;

/// <summary>
/// Строитель <see cref="AuthorEntity"/>.
/// </summary>
public sealed class AuthorBuilder
{
    private readonly AuthorEntity entity;

    private string firstName;

    private string lastName;

    private string position;

    /// <summary>
    /// Инициализация экземпляра класса <see cref="AuthorBuilder"/>.
    /// </summary>
    /// <param name="entity">Сущность.</param>
    public AuthorBuilder(AuthorEntity entity)
    {
        this.entity = entity;
        this.firstName = entity.FirstName;
        this.lastName = entity.LastName;
        this.position = entity.Position;
    }

    /// <summary>
    /// Добавить атрибуты.
    /// </summary>
    /// <param name="model">Модель.</param>
    /// <returns>Строитель.</returns>
    public AuthorBuilder SetAttributes(AuthorModel model)
    {
        this.firstName = model.FirstName;
        this.lastName = model.LastName;
        this.position = model.Position;
        return this;
    }

    /// <summary>
    /// Построить сущность.
    /// </summary>
    /// <returns>Сущность.</returns>
    public AuthorEntity Build()
    {
        // атрибуты:
        // this.entity.Id - не обновляется!
        this.entity.FirstName = this.firstName;
        this.entity.LastName = this.lastName;
        this.entity.Position = this.position;

        // реляционные связи:
        // this.entity.ProjectRevisions - не обновляется!
        return this.entity;
    }
}