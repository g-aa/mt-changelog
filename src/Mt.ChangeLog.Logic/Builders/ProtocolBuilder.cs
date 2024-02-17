using Mt.ChangeLog.Entities.Tables;
using Mt.ChangeLog.TransferObjects.Protocol;

namespace Mt.ChangeLog.Logic.Builders;

/// <summary>
/// Строитель <see cref="ProtocolEntity"/>.
/// </summary>
public class ProtocolBuilder
{
    private readonly ProtocolEntity _entity;

    private string _title;

    private string _description;

    private IQueryable<CommunicationEntity> _communications;

    /// <summary>
    /// Инициализация экземпляра класса <see cref="ProtocolBuilder"/>.
    /// </summary>
    /// <param name="entity">Сущность.</param>
    public ProtocolBuilder(ProtocolEntity entity)
    {
        _entity = entity;
        _title = entity.Title;
        _description = entity.Description;
        _communications = entity.Communications.AsQueryable();
    }

    /// <summary>
    /// Добавить атрибуты.
    /// </summary>
    /// <param name="model">Модель.</param>
    /// <returns>Строитель.</returns>
    public ProtocolBuilder SetAttributes(ProtocolModel model)
    {
        _title = model.Title;
        _description = model.Description;
        return this;
    }

    /// <summary>
    /// Добавить перечень протоколов.
    /// </summary>
    /// <param name="modules">Перечень протоколов.</param>
    /// <returns>Строитель.</returns>
    public ProtocolBuilder SetModules(IQueryable<CommunicationEntity> modules)
    {
        _communications = modules;
        return this;
    }

    /// <summary>
    /// Построить сущность.
    /// </summary>
    /// <returns>Сущность.</returns>
    public ProtocolEntity Build()
    {
        // атрибуты:
        // _entity.Id - не обновляется!
        _entity.Title = _title;
        _entity.Description = _description;

        // реляционные связи:
        _entity.Communications = _communications.ToHashSet();
        return _entity;
    }
}