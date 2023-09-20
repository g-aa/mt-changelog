using Microsoft.Extensions.DependencyInjection;
using Mt.ChangeLog.DataAccess.Abstraction;
using Mt.ChangeLog.DataAccess.Implementation;
using Mt.Utilities;

namespace Mt.ChangeLog.DataAccess;

/// <summary>
/// Методы расширения для <see cref="IServiceCollection"/>.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Добавить компоненты контекста данных в коллекцию сервисов.
    /// </summary>
    /// <param name="services">Коллекция сервисов.</param>
    /// <returns>Модифицированная коллекция сервисов.</returns>
    public static IServiceCollection AddDataAccess(this IServiceCollection services)
    {
        Check.NotNull(services, nameof(services));

        services.AddSingleton<NpgsqlConnectionFactory>();
        services.AddScoped(provider => provider.GetRequiredService<NpgsqlConnectionFactory>().CreateConnection());

        services.AddTransient<IAnalogModuleRepository, AnalogModuleRepository>();
        services.AddTransient<IArmEditRepository, ArmEditRepository>();
        services.AddTransient<IAuthorRepository, AuthorRepository>();

        return services;
    }
}