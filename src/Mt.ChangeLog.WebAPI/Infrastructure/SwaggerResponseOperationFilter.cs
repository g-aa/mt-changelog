using System.Diagnostics.CodeAnalysis;
using System.Globalization;

using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Mt.ChangeLog.WebAPI.Controllers;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Mt.ChangeLog.WebAPI.Infrastructure;

/// <summary>
/// Swagger core response type filter.
/// </summary>
[ExcludeFromCodeCoverage]
public sealed class SwaggerResponseOperationFilter : IOperationFilter
{
    private static readonly IReadOnlyDictionary<int, string> ErrorStatuses = new Dictionary<int, string>
    {
        { StatusCodes.Status400BadRequest, "Ошибка в логике приложения, ошибка валидации." },
        { StatusCodes.Status401Unauthorized, "Пользователь не авторизован." },
        { StatusCodes.Status403Forbidden, "Доступ к ресурсу запрещен." },
        { StatusCodes.Status422UnprocessableEntity, "Ошибка в логике приложения." },
        { StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера." },
    };

    /// <inheritdoc />
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (typeof(AboutController).Equals(context.MethodInfo.DeclaringType))
        {
            return;
        }

        var mediaType = new OpenApiMediaType
        {
            Schema = context.SchemaGenerator.GenerateSchema(typeof(ProblemDetails), context.SchemaRepository),
        };

        var content = new Dictionary<string, OpenApiMediaType>
        {
            { "text/plain", mediaType },
            { "application/json", mediaType },
            { "text/json", mediaType },
        };

        foreach (var status in ErrorStatuses)
        {
            var httpCode = status.Key.ToString(CultureInfo.InvariantCulture);
            if (!operation.Responses.ContainsKey(httpCode))
            {
                operation.Responses.Add(httpCode, new OpenApiResponse { Content = content, Description = status.Value, });
            }
        }
    }
}