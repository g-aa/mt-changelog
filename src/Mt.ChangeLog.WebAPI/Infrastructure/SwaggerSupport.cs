using System.Reflection;

using Asp.Versioning;
using Asp.Versioning.ApiExplorer;

using Microsoft.Extensions.Options;

using Swashbuckle.AspNetCore.SwaggerGen;

namespace Mt.ChangeLog.WebAPI.Infrastructure;

/// <summary>
/// Регистрация и настройка swagger в приложении.
/// </summary>
public static class SwaggerSupport
{
    /// <summary>
    /// Добавить компоненты версионирования API в коллекцию сервисов.
    /// </summary>
    /// <param name="services">Коллекция сервисов.</param>
    /// <returns>Модифицированная коллекция сервисов.</returns>
    public static IServiceCollection AddApiVersioningSupport(this IServiceCollection services)
    {
        return services
            .AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
            })
            .AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v' VVV";
                options.SubstituteApiVersionInUrl = true;
            })
            .Services;
    }

    /// <summary>
    /// Регистрация swagger UI в коллекции сервисов.
    /// </summary>
    /// <param name="services">Коллекция сервисов.</param>
    /// <param name="assemblies">Перечень сборок проекта.</param>
    /// <returns>Модифицированная коллекция сервисов.</returns>
    public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services, IReadOnlyCollection<Assembly> assemblies)
    {
        return services
            .AddTransient<IConfigureOptions<SwaggerGenOptions>, SwaggerConfigureOptions>()
            .AddSwaggerGen(options =>
            {
                options.EnableAnnotations();
                options.OperationFilter<SwaggerResponseOperationFilter>();

                foreach (var assembly in assemblies)
                {
                    var xmlDocumentation = $"{assembly.GetName().Name}.xml";
                    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlDocumentation));
                }

                options.SchemaGeneratorOptions.SchemaIdSelector = (Type type) => type.Name;
            });
    }

    /// <summary>
    /// Инициализация swagger UI.
    /// </summary>
    /// <param name="builder">Строитель приложения.</param>
    /// <returns>Модифицированный строитель приложения.</returns>
    public static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder builder)
    {
        var apiVersionProvider = builder.ApplicationServices.GetRequiredService<IApiVersionDescriptionProvider>();
        return builder
            .UseSwagger(options =>
            {
                options.RouteTemplate = "swagger/{documentName}/mt-changelog.{json|yaml}";
            })
            .UseSwaggerUI(uiOptions =>
            {
                foreach (var groupName in apiVersionProvider.ApiVersionDescriptions.Select(e => e.GroupName))
                {
                    uiOptions.SwaggerEndpoint($"/swagger/{groupName}/mt-changelog.yaml", $"API {groupName}");
                }

                uiOptions.DisplayRequestDuration();
                uiOptions.DefaultModelsExpandDepth(0);
            });
    }
}