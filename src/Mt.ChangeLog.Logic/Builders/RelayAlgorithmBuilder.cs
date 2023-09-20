using Mt.ChangeLog.Entities.Tables;
using Mt.ChangeLog.TransferObjects.RelayAlgorithm;

namespace Mt.ChangeLog.Logic.Builders;

/// <summary>
/// Строитель <see cref="RelayAlgorithmEntity"/>.
/// </summary>
public class RelayAlgorithmBuilder
{
    private readonly RelayAlgorithmEntity entity;

    private string group;

    private string title;

    private string ansi;

    private string logicalnode;

    private string description;

    /// <summary>
    /// Инициализация экземпляра класса <see cref="RelayAlgorithmBuilder"/>.
    /// </summary>
    /// <param name="entity">Сущность.</param>
    public RelayAlgorithmBuilder(RelayAlgorithmEntity entity)
    {
        this.entity = entity;
        this.group = entity.Group;
        this.title = entity.Title;
        this.ansi = entity.ANSI;
        this.logicalnode = entity.LogicalNode;
        this.description = entity.Description;
    }

    /// <summary>
    /// Добавить атрибуты.
    /// </summary>
    /// <param name="model">Модель.</param>
    /// <returns>Строитель.</returns>
    public RelayAlgorithmBuilder SetAttributes(RelayAlgorithmModel model)
    {
        this.group = model.Group;
        this.title = model.Title;
        this.ansi = model.ANSI;
        this.logicalnode = model.LogicalNode;
        this.description = model.Description;
        return this;
    }

    /// <summary>
    /// Построить сущность.
    /// </summary>
    /// <returns>Сущность.</returns>
    public RelayAlgorithmEntity Build()
    {
        // атрибуты:
        // this.entity.Id - не обновляется!
        this.entity.Group = this.group;
        this.entity.Title = this.title;
        this.entity.ANSI = this.ansi;
        this.entity.LogicalNode = this.logicalnode;
        this.entity.Description = this.description;

        // this.entity.ProjectRevisions - не обновляется!
        return this.entity;
    }
}