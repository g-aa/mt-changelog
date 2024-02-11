using Mt.ChangeLog.Entities.Tables;
using Mt.ChangeLog.TransferObjects.RelayAlgorithm;

namespace Mt.ChangeLog.Logic.Builders;

/// <summary>
/// Строитель <see cref="RelayAlgorithmEntity"/>.
/// </summary>
public class RelayAlgorithmBuilder
{
    private readonly RelayAlgorithmEntity _entity;

    private string _group;

    private string _title;

    private string _ansi;

    private string _logicalnode;

    private string _description;

    /// <summary>
    /// Инициализация экземпляра класса <see cref="RelayAlgorithmBuilder"/>.
    /// </summary>
    /// <param name="entity">Сущность.</param>
    public RelayAlgorithmBuilder(RelayAlgorithmEntity entity)
    {
        _entity = entity;
        _group = entity.Group;
        _title = entity.Title;
        _ansi = entity.ANSI;
        _logicalnode = entity.LogicalNode;
        _description = entity.Description;
    }

    /// <summary>
    /// Добавить атрибуты.
    /// </summary>
    /// <param name="model">Модель.</param>
    /// <returns>Строитель.</returns>
    public RelayAlgorithmBuilder SetAttributes(RelayAlgorithmModel model)
    {
        _group = model.Group;
        _title = model.Title;
        _ansi = model.ANSI;
        _logicalnode = model.LogicalNode;
        _description = model.Description;
        return this;
    }

    /// <summary>
    /// Построить сущность.
    /// </summary>
    /// <returns>Сущность.</returns>
    public RelayAlgorithmEntity Build()
    {
        // атрибуты:
        // _entity.Id - не обновляется!
        _entity.Group = _group;
        _entity.Title = _title;
        _entity.ANSI = _ansi;
        _entity.LogicalNode = _logicalnode;
        _entity.Description = _description;

        // _entity.ProjectRevisions - не обновляется!
        return _entity;
    }
}