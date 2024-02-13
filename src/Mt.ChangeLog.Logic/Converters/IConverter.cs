namespace Mt.ChangeLog.Logic.Converters;

/// <summary>
/// Интерфейс преобразователя объектов.
/// </summary>
/// <typeparam name="TSource">Тип объекта источника данных.</typeparam>
/// <typeparam name="TDestination">Тип объекта назначения данных.</typeparam>
public interface IConverter<in TSource, out TDestination>
    where TSource : notnull
    where TDestination : notnull
{
    /// <summary>
    /// Преобразовать объект.
    /// </summary>
    /// <param name="source">Исходный объект.</param>
    /// <returns>Объект назначения.</returns>
    TDestination Convert(TSource source);
}