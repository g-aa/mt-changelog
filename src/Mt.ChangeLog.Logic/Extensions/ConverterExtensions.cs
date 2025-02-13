using Microsoft.Extensions.DependencyInjection;

using Mt.ChangeLog.Logic.Converters;

namespace Mt.ChangeLog.Logic.Extensions;

/// <summary>
/// Методы расширения для <see cref="IServiceCollection"/>.
/// </summary>
#pragma warning disable S6602 // "Find" method should be used instead of the "FirstOrDefault" extension
public static class ConverterExtensions
{
    /// <summary>
    /// Добавить конвертеры моделей данных в коллекцию сервисов.
    /// </summary>
    /// <typeparam name="TSource">Источник конверторов данных.</typeparam>
    /// <param name="services">Коллекция сервисов.</param>
    /// <returns>Модифицированная коллекция сервисов.</returns>
    public static IServiceCollection AddConverters<TSource>(this IServiceCollection services)
        where TSource : notnull
    {
        var openGenericType = typeof(IConverter<,>);
        var predicate = (Type iType) => iType.IsGenericType && iType.GetGenericTypeDefinition() == openGenericType;

        var converterTypes = typeof(TSource).Assembly
            .GetTypes()
            .Where(type => type.GetInterfaces().FirstOrDefault(predicate) != null)
            .ToList();

        foreach (var type in converterTypes)
        {
            var iType = type.GetInterfaces().First(predicate);
            services.AddTransient(iType, type);
        }

        return services;
    }
}