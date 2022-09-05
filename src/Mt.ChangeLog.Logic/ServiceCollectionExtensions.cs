using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Mt.Utilities;

namespace Mt.ChangeLog.Logic
{
    /// <summary>
    /// Методы расширения для <see cref="IServiceCollection"/>.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Добавить компоненты логики в коллекцию сервисов.
        /// </summary>
        /// <param name="services">Коллекция сервисов.</param>
        /// <returns>Модифицированная коллекция сервисов.</returns>
        public static IServiceCollection AddLogic(this IServiceCollection services)
        {
            Check.NotNull(services, nameof(services));
            services.AddMediatR(typeof(ServiceCollectionExtensions).Assembly);
            return services;
        }
    }
}