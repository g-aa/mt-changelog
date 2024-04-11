using Microsoft.Extensions.DependencyInjection;
using Mt.ChangeLog.DataAccess.Abstraction;
using Mt.ChangeLog.DataAccess.Implementation;

namespace Mt.ChangeLog.DataAccess;

/// <summary>
/// Методы расширения для <see cref="IServiceCollection"/>.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Добавить компоненты уровня доступа к  данным в коллекцию сервисов.
    /// </summary>
    /// <param name="services">Коллекция сервисов.</param>
    /// <returns>Модифицированная коллекция сервисов.</returns>
    public static IServiceCollection AddDataAccess(this IServiceCollection services)
    {
        return services
            .AddTransient<IAnalogModuleRepository, AnalogModuleRepository>()
            .AddTransient<IArmEditRepository, ArmEditRepository>()
            .AddTransient<IAuthorRepository, AuthorRepository>()
            .AddScoped(provider => provider.GetRequiredService<NpgsqlConnectionFactory>().CreateConnection())
            .AddSingleton<NpgsqlConnectionFactory>();
    }
}