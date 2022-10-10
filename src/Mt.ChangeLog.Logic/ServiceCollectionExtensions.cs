using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Mt.Utilities;
using System.Reflection;

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
        /// <param name="assemblies">Перечень сборок проекта.</param>
        /// <returns>Модифицированная коллекция сервисов.</returns>
        public static IServiceCollection AddLogic(this IServiceCollection services, Assembly[] assemblies)
        {
            Check.NotNull(services, nameof(services));
            Check.NotEmpty(assemblies, nameof(assemblies));
            services.AddMediatR(assemblies);
            return services;
        }
    }
}