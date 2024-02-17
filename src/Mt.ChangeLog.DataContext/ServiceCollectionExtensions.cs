using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace Mt.ChangeLog.DataContext;

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
    public static IServiceCollection AddApplicationContext(this IServiceCollection services)
    {
        return services
            .AddDbContext<IMtContext, MtContext>((provider, options) =>
            {
                var configuration = provider.GetRequiredService<IConfiguration>();
                var connectionString = new NpgsqlConnectionStringBuilder(configuration.GetConnectionString("NpgSqlDb"));
                options.UseNpgsql(connectionString.ConnectionString);
            });
    }
}