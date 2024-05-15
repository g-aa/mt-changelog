using System.Net.Mime;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization.Metadata;

using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Mt.ChangeLog.WebAPI.Infrastructure;

/// <summary>
/// Методы расширения для <see cref="IApplicationBuilder"/>.
/// </summary>
public static class DiagnosticApplicationBuilderExtensions
{
    private static readonly JsonSerializerOptions SerializerOptions = new()
    {
        WriteIndented = true,
        TypeInfoResolver = new DefaultJsonTypeInfoResolver(),
    };

    /// <summary>
    /// Инициализация конечных точек для 'HealthChecks'.
    /// </summary>
    /// <param name="builder">Строитель приложения.</param>
    /// <returns>Модифицированный строитель приложения.</returns>
    public static IApplicationBuilder UseDiagnostics(this IApplicationBuilder builder)
    {
        return builder
          .UseHealthChecks("/health/live")
          .UseHealthChecks("/health/ready", new HealthCheckOptions
          {
              Predicate = check => check.Tags.Contains("ready"),
          })
          .UseHealthChecks("/health/status", new HealthCheckOptions
          {
              Predicate = _ => true,
              ResponseWriter = HealthCheckResponseWriterAsync,
          });
    }

    /// <summary>
    /// Форматирует результат проверки в виде Json объекта.
    /// </summary>
    /// <param name="result">Health report.</param>
    /// <returns><see cref="JsonObject"/> с результатом проверки.</returns>
    internal static JsonObject HealthReportFormatter(HealthReport result)
    {
        return new JsonObject
        {
            ["status"] = result.Status.ToString(),
            ["totalDuration"] = result.TotalDuration.ToString(),
            ["entries"] = new JsonArray(result.Entries.Select(pair => new JsonObject
            {
                [pair.Key] = new JsonObject
                {
                    ["status"] = pair.Value.Status.ToString(),
                    ["description"] = pair.Value.Description,
                    ["duration"] = pair.Value.Duration.ToString(),
                    ["data"] = new JsonObject(pair.Value.Data.Select(p => new KeyValuePair<string, JsonNode?>(p.Key, JsonValue.Create(p.Value)))),
                },
            }).Cast<JsonNode?>().ToArray()),
        };
    }

    /// <summary>
    /// Записывает результат проверки в виде Json объекта.
    /// </summary>
    /// <param name="httpContext">Текущий http контекст.</param>
    /// <param name="result">Health report.</param>
    /// <returns>Результат выполнения асинхронной задачи.</returns>
    internal static Task HealthCheckResponseWriterAsync(HttpContext httpContext, HealthReport result)
    {
        httpContext.Response.ContentType = MediaTypeNames.Application.Json;
        return httpContext.Response.WriteAsync(HealthReportFormatter(result).ToJsonString(SerializerOptions));
    }
}