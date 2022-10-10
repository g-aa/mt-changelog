using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;

namespace Mt.ChangeLog.WebAPI.Infrastracture
{
    /// <summary>
    /// Регистрация и настройка swagger в приложении.
    /// </summary>
    internal static class SwaggerSupport
    {
        /// <summary>
        /// Регистрация swagger UI в коллекции сервисов.
        /// </summary>
        /// <param name="services">Коллекция сервисов.</param>
        /// <returns>Модифицированная коллекция сервисов.</returns>
        internal static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo 
                { 
                    Title = $"Mt-ChangeLog API {Program.CurrentVersion}",
                    Version = "v1",
                    Description = "Rest API для взаимодействия с функционалом Mt-ChangeLog.",
                });

                options.EnableAnnotations();

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFile));
                options.SchemaGeneratorOptions.SchemaIdSelector = type => type.FullName;
            });

            return services;
        }

        /// <summary>
        /// Инициализация swagger UI.
        /// </summary>
        /// <param name="builder">Строитель приложения.</param>
        /// <returns>Модифицированный строитель приложения.</returns>
        internal static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder builder)
        {
            builder.UseSwagger();
            builder.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint($"/swagger/v1/swagger.json", $"Mt-ChangeLog API v1");
                options.DisplayRequestDuration();
            });

            return builder;
        }
    }
}