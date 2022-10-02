using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Mt.Utilities;
using System.Reflection;

namespace Mt.ChangeLog.TransferObjects
{
    /// <summary>
    /// Методы расширения для <see cref="IServiceCollection"/>.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Добавить компоненты DTOs в коллекцию сервисов.
        /// </summary>
        /// <param name="services">Коллекция сервисов.</param>
        /// <param name="assemblies">Перечень сборок проекта.</param>
        /// <returns>Модифицированная коллекция сервисов.</returns>
        public static IServiceCollection AddTransferObjects(this IServiceCollection services, Assembly[] assemblies)
        {
            Check.NotNull(services, nameof(services));
            Check.NotEmpty(assemblies, nameof(assemblies));
            services.AddValidatorsFromAssemblies(assemblies);
            return services;
        }
    }
}