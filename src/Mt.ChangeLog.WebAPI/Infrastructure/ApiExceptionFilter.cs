using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Mt.Utilities.Exceptions;
using Mt.Utilities.Extensions;

namespace Mt.ChangeLog.WebAPI.Infrastructure;

/// <summary>
/// Обработчик стандартных исключений.
/// </summary>
public sealed class ApiExceptionFilter : IExceptionFilter
{
    /// <inheritdoc />
    public void OnException(ExceptionContext context)
    {
        var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<ApiExceptionFilter>>();
        switch (context.Exception)
        {
            case ValidationException validationException:
                logger.LogWarning(validationException, validationException.Message);
                var errors = validationException.Errors
                    .GroupBy(e => e.PropertyName)
                    .ToDictionary(k => k.Key, v => v.Select(e => e.ErrorMessage).ToArray());
                var validationError = new ValidationProblemDetails(errors);
                context.Result = new BadRequestObjectResult(validationError);
                break;

            case MtException mtException:
                logger.LogWarning(mtException, mtException.Message);
                var mtDetails = new ProblemDetails
                {
                    Status = mtException.Code.HttpStatusCode(),
                    Title = mtException.Title,
                    Detail = mtException.Desc,
                };
                context.Result = new ObjectResult(mtDetails)
                {
                    StatusCode = mtDetails.Status,
                };
                break;

            case MtBaseException mtBaseException:
                logger.LogWarning(mtBaseException, mtBaseException.Message);
                var baseDetails = new ProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = mtBaseException.Title,
                    Detail = mtBaseException.Desc,
                };
                context.Result = new BadRequestObjectResult(baseDetails);
                break;

            default:
                logger.LogError(context.Exception, context.Exception.Message);
                var code = ErrorCode.InternalServerError;
                var details = new ProblemDetails
                {
                    Status = code.HttpStatusCode(),
                    Detail = code.Desc(),
                };
                context.Result = new ObjectResult(details)
                {
                    StatusCode = details.Status,
                };
                break;
        }
    }
}