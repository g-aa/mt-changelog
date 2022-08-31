﻿using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Mt.Utilities;

namespace Mt.ChangeLog.TransferObjects
{
    /// <summary>
    /// Методы расширения для <see cref="IServiceCollection"/>.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services">Коллекция сервисов.</param>
        /// <returns></returns>
        public static IServiceCollection AddTransferObjects(this IServiceCollection services)
        {
            Check.NotNull(services, nameof(services));
            services.AddValidatorsFromAssembly(typeof(ServiceCollectionExtensions).Assembly);
            return services;
        }
    }
}