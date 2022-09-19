using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Mt.ChangeLog.Context;
using Mt.Utilities;

namespace Mt.ChangeLog.WebAPI.Infrastracture
{
    /// <summary>
    /// Методы расширения для <see cref="IApplicationBuilder"/>.
    /// </summary>
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Инициализация начального состояния базы данных.
        /// </summary>
        /// <param name="builder">Строитель приложения.</param>
        /// <returns>Модифицированный строитель приложения.</returns>
        public static IApplicationBuilder UseDefaultDatabaseInitialization(this IApplicationBuilder builder)
        {
            var scope = Check.NotNull(builder, nameof(builder)).ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            using (var context = Check.NotNull(scope, nameof(scope)).ServiceProvider.GetService<ApplicationContext>())
            {
                Check.NotNull(context, nameof(context)).InitializeDefaultState();
            }
            return builder;
        }
    }
}