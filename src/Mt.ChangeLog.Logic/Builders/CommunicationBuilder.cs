using Mt.ChangeLog.Entities.Tables;
using Mt.ChangeLog.TransferObjects.Communication;

namespace Mt.ChangeLog.Logic.Builders;

/// <summary>
/// Строитель <see cref="CommunicationEntity"/>.
/// </summary>
public sealed class CommunicationBuilder
{
    private readonly CommunicationEntity _entity;

    private string _title;

    private string _description;

    private IQueryable<ProtocolEntity> _protocols;

    /// <summary>
    /// Инициализация экземпляра класса <see cref="CommunicationBuilder"/>.
    /// </summary>
    /// <param name="entity">Сущность.</param>
    public CommunicationBuilder(CommunicationEntity entity)
    {
        _entity = entity;
        _title = entity.Title;
        _description = entity.Description;
        _protocols = entity.Protocols.AsQueryable();
    }

    /// <summary>
    /// Добавить атрибуты.
    /// </summary>
    /// <param name="model">Модель.</param>
    /// <returns>Строитель.</returns>
    public CommunicationBuilder SetAttributes(CommunicationModel model)
    {
        _title = model.Title;
        _description = model.Description;
        return this;
    }

    /// <summary>
    /// Добавить перечень протоколов.
    /// </summary>
    /// <param name="protocols">Перечень протоколов.</param>
    /// <returns>Строитель.</returns>
    public CommunicationBuilder SetProtocols(IQueryable<ProtocolEntity> protocols)
    {
        _protocols = protocols;
        return this;
    }

    /// <summary>
    /// Построить сущность.
    /// </summary>
    /// <returns>Сущность.</returns>
    public CommunicationEntity Build()
    {
        // атрибуты:
        // _entity.Id - не обновляется!
        _entity.Title = _title;
        _entity.Description = _description;

        // реляционные связи:
        _entity.Protocols = _protocols.ToHashSet();
        return _entity;
    }
}