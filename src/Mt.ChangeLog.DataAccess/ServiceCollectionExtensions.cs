using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Mt.ChangeLog.DataAccess.Abstractions;
using Mt.Utilities;
using Npgsql;
using System.Data;

namespace Mt.ChangeLog.DataAccess
{
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
            services.AddScoped<IDbConnection>(provider =>
            {
                var configuration = Check.NotNull(provider.GetService<IConfiguration>(), nameof(IConfiguration));
                var sPgSqlConnection = Check.NotNull(
                    configuration["ConnectionStrings:PostgreSqlDbConnection"],
                    "В файле 'appsettings.json' не указана строка подключения к БД.");
                return new NpgsqlConnection(sPgSqlConnection);
            });

            services.AddTransient<IAnalogModuleRepository, AnalogModuleRepository>();
            services.AddTransient<IArmEditRepository, ArmEditRepository>();

            return services;
        }
    }
}