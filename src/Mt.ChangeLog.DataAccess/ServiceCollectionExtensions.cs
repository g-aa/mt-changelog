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
            Check.NotNull(services, nameof(services));
            services.AddScoped<IDbConnection>(provider =>
            {
                var configuration = provider.GetService<IConfiguration>();
                var sConnection = Check.NotNull(configuration["ConnectionStrings:NpgSqlDb"], "В файле 'appsettings.json' не указана строка подключения к БД.");
                return new NpgsqlConnection(sConnection);
            });

            services.AddTransient<IAnalogModuleRepository, AnalogModuleRepository>();
            services.AddTransient<IArmEditRepository, ArmEditRepository>();

            return services;
        }
    }
}