using Mt.ChangeLog.Entities.Tables;
using Mt.ChangeLog.TransferObjects.ArmEdit;

namespace Mt.ChangeLog.Logic.Builders;

/// <summary>
/// Строитель <see cref="ArmEditEntity"/>.
/// </summary>
public class ArmEditBuilder
{
    private readonly ArmEditEntity _entity;

    private string _divg;

    private string _version;

    private DateTime? _date;

    private string _description;

    /// <summary>
    /// Инициализация экземпляра класса <see cref="ArmEditBuilder"/>.
    /// </summary>
    /// <param name="entity">Сущность.</param>
    public ArmEditBuilder(ArmEditEntity entity)
    {
        _entity = entity;
        _divg = entity.DIVG;
        _version = entity.Version;
        _date = entity.Date;
        _description = entity.Description;
    }

    /// <summary>
    /// Добавить атрибуты.
    /// </summary>
    /// <param name="model">Модель.</param>
    /// <returns>Строитель.</returns>
    public ArmEditBuilder SetAttributes(ArmEditModel model)
    {
        _divg = model.DIVG;
        _version = model.Version;
        _date = model.Date;
        _description = model.Description;
        return this;
    }

    /// <summary>
    /// Построить сущность.
    /// </summary>
    /// <returns>Сущность.</returns>
    public ArmEditEntity Build()
    {
        // атрибуты:
        // _entity.Id - не обновляется!
        _entity.DIVG = _divg;
        _entity.Version = _version;
        _entity.Date = _date != null ? _date.Value : DateTime.UtcNow;
        _entity.Description = _description;

        // реляционные связи:
        // _entity.ProjectRevisions - не обновляется!
        return _entity;
    }
}