using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Mt.Utilities;

namespace Mt.ChangeLog.Context
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
        public static IServiceCollection AddApplicationContext(this IServiceCollection services)
        {
            Check.NotNull(services, nameof(services));
            services.AddDbContext<MtContext>((provider, options) =>
            {
                var configuration = provider.GetService<IConfiguration>();
                var sConnection = Check.NotNull(configuration["ConnectionStrings:NpgSqlDb"], "В файле 'appsettings.json' не указана строка подключения к БД.");
                options.UseNpgsql(sConnection);
            });
            return services;
        }
    }
}