using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Mt.Utilities.Exceptions;
using Mt.Utilities.Extensions;

using ValidationResult = FluentValidation.Results.ValidationResult;

namespace Mt.ChangeLog.WebAPI.Infrastructure;

/// <summary>
/// Обработчик стандартных исключений.
/// </summary>
public sealed class ApiExceptionFilter : IExceptionFilter
{
    /// <summary>
    /// Передавать детализацию об исключениях.
    /// </summary>
    private readonly bool _passDetails;

    /// <summary>
    /// Инициализация экземпляра класса <see cref="ApiExceptionFilter"/>.
    /// </summary>
    public ApiExceptionFilter()
    {
        _passDetails = false;
    }

    /// <inheritdoc />
    public void OnException(ExceptionContext context)
    {
        var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<ApiExceptionFilter>>();
        switch (context.Exception)
        {
            case ValidationException validationException:
                var result = new ValidationResult(validationException.Errors);
                result.AddToModelState(context.ModelState, null);
                var validationError = new ValidationProblemDetails(context.ModelState);
                context.Result = new BadRequestObjectResult(validationError);
                break;

            case MtException exception:
                logger.LogWarning(context.Exception, context.Exception.Message);
                var mtDetails = new ProblemDetails
                {
                    Status = exception.Code.HttpStatusCode(),
                    Title = exception.Title,
                    Detail = exception.Desc,
                };
                context.Result = new ObjectResult(mtDetails)
                {
                    StatusCode = mtDetails.Status,
                };
                break;

            case MtBaseException exception:
                logger.LogWarning(context.Exception, context.Exception.Message);
                var baseDetails = new ProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = exception.Title,
                    Detail = exception.Desc,
                };
                context.Result = new BadRequestObjectResult(baseDetails);
                break;

            default:
                logger.LogError(context.Exception, context.Exception.Message);
                var code = ErrorCode.InternalServerError;
                var details = new ProblemDetails
                {
                    Status = code.HttpStatusCode(),
                    Detail = _passDetails ? context.Exception.Message : code.Desc(),
                };
                context.Result = new ObjectResult(details)
                {
                    StatusCode = details.Status,
                };
                break;
        }
    }
}