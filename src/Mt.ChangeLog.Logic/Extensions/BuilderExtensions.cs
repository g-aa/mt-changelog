using Mt.ChangeLog.Entities.Tables;
using Mt.ChangeLog.Logic.Builders;

namespace Mt.ChangeLog.Logic.Extensions;

/// <summary>
/// Методы расширения для строителей.
/// </summary>
public static class BuilderExtensions
{
    /// <summary>
    /// Получить строитель.
    /// </summary>
    /// <param name="entity">Сущность.</param>
    /// <returns>Строитель.</returns>
    public static AnalogModuleBuilder GetBuilder(this AnalogModuleEntity entity)
    {
        return new AnalogModuleBuilder(entity);
    }

    /// <summary>
    /// Получить строитель.
    /// </summary>
    /// <param name="entity">Сущность.</param>
    /// <returns>Строитель.</returns>
    public static ArmEditBuilder GetBuilder(this ArmEditEntity entity)
    {
        return new ArmEditBuilder(entity);
    }

    /// <summary>
    /// Получить строитель.
    /// </summary>
    /// <param name="entity">Сущность.</param>
    /// <returns>Строитель.</returns>
    public static AuthorBuilder GetBuilder(this AuthorEntity entity)
    {
        return new AuthorBuilder(entity);
    }

    /// <summary>
    /// Получить строитель.
    /// </summary>
    /// <param name="entity">Сущность.</param>
    /// <returns>Строитель.</returns>
    public static CommunicationBuilder GetBuilder(this CommunicationEntity entity)
    {
        return new CommunicationBuilder(entity);
    }

    /// <summary>
    /// Получить строитель.
    /// </summary>
    /// <param name="entity">Сущность.</param>
    /// <returns>Строитель.</returns>
    public static PlatformBuilder GetBuilder(this PlatformEntity entity)
    {
        return new PlatformBuilder(entity);
    }

    /// <summary>
    /// Получить строитель.
    /// </summary>
    /// <param name="entity">Сущность.</param>
    /// <returns>Строитель.</returns>
    public static ProjectRevisionBuilder GetBuilder(this ProjectRevisionEntity entity)
    {
        return new ProjectRevisionBuilder(entity);
    }

    /// <summary>
    /// Получить строитель.
    /// </summary>
    /// <param name="entity">Сущность.</param>
    /// <returns>Строитель.</returns>
    public static ProjectStatusBuilder GetBuilder(this ProjectStatusEntity entity)
    {
        return new ProjectStatusBuilder(entity);
    }

    /// <summary>
    /// Получить строитель.
    /// </summary>
    /// <param name="entity">Сущность.</param>
    /// <returns>Строитель.</returns>
    public static ProjectVersionBuilder GetBuilder(this ProjectVersionEntity entity)
    {
        return new ProjectVersionBuilder(entity);
    }

    /// <summary>
    /// Получить строитель.
    /// </summary>
    /// <param name="entity">Сущность.</param>
    /// <returns>Строитель.</returns>
    public static ProtocolBuilder GetBuilder(this ProtocolEntity entity)
    {
        return new ProtocolBuilder(entity);
    }

    /// <summary>
    /// Получить строитель.
    /// </summary>
    /// <param name="entity">Сущность.</param>
    /// <returns>Строитель.</returns>
    public static RelayAlgorithmBuilder GetBuilder(this RelayAlgorithmEntity entity)
    {
        return new RelayAlgorithmBuilder(entity);
    }
}