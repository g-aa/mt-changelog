using System.Reflection;

using Microsoft.OpenApi.Models;

namespace Mt.ChangeLog.WebAPI.Infrastructure;

/// <summary>
/// Регистрация и настройка swagger в приложении.
/// </summary>
public static class SwaggerSupport
{
    /// <summary>
    /// Регистрация swagger UI в коллекции сервисов.
    /// </summary>
    /// <param name="services">Коллекция сервисов.</param>
    /// <param name="assemblies">Перечень сборок проекта.</param>
    /// <returns>Модифицированная коллекция сервисов.</returns>
    public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services, IReadOnlyCollection<Assembly> assemblies)
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
            options.OperationFilter<SwaggerResponseOperationFilter>();

            foreach (var assembly in assemblies)
            {
                var xmlDocumentation = $"{assembly.GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlDocumentation));
            }

            options.SchemaGeneratorOptions.SchemaIdSelector = (Type type) => type.Name;
        });

        return services;
    }

    /// <summary>
    /// Инициализация swagger UI.
    /// </summary>
    /// <param name="builder">Строитель приложения.</param>
    /// <returns>Модифицированный строитель приложения.</returns>
    public static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder builder)
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