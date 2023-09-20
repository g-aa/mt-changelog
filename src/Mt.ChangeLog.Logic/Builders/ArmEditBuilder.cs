using Mt.ChangeLog.Entities.Tables;
using Mt.ChangeLog.TransferObjects.ArmEdit;

namespace Mt.ChangeLog.Logic.Builders;

/// <summary>
/// Строитель <see cref="ArmEditEntity"/>.
/// </summary>
public class ArmEditBuilder
{
    private readonly ArmEditEntity entity;

    private string divg;

    private string version;

    private DateTime? date;

    private string description;

    /// <summary>
    /// Инициализация экземпляра класса <see cref="ArmEditBuilder"/>.
    /// </summary>
    /// <param name="entity">Сущность.</param>
    public ArmEditBuilder(ArmEditEntity entity)
    {
        this.entity = entity;
        this.divg = entity.DIVG;
        this.version = entity.Version;
        this.date = entity.Date;
        this.description = entity.Description;
    }

    /// <summary>
    /// Добавить атрибуты.
    /// </summary>
    /// <param name="model">Модель.</param>
    /// <returns>Строитель.</returns>
    public ArmEditBuilder SetAttributes(ArmEditModel model)
    {
        this.divg = model.DIVG;
        this.version = model.Version;
        this.date = model.Date;
        this.description = model.Description;
        return this;
    }

    /// <summary>
    /// Построить сущность.
    /// </summary>
    /// <returns>Сущность.</returns>
    public ArmEditEntity Build()
    {
        // атрибуты:
        // this.entity.Id - не обновляется!
        this.entity.DIVG = this.divg;
        this.entity.Version = this.version;
        this.entity.Date = this.date != null ? this.date.Value : DateTime.Now;
        this.entity.Description = this.description;

        // реляционные связи:
        // this.entity.ProjectRevisions - не обновляется!
        return this.entity;
    }
}