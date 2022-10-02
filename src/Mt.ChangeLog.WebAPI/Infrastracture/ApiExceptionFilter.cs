using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Mt.Utilities;
using Mt.Utilities.Exceptions;
using Mt.Utilities.Extensions;

namespace Mt.ChangeLog.WebAPI.Infrastracture
{
    /// <summary>
    /// Обработчик стандартных исключений.
    /// </summary>
    public sealed class ApiExceptionFilter : IExceptionFilter
    {
        /// <inheritdoc />
        public void OnException(ExceptionContext context)
        {
            Check.NotNull(context, nameof(context));
            var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<ApiExceptionFilter>>();
            logger?.LogError(context.Exception, context.Exception.Message);

            switch (context.Exception)
            {
                case MtException mtException:
                    context.Result = new ObjectResult(new ApiProblemDetails(mtException.Code))
                    {
                        StatusCode = mtException.Code.HttpStatusCode(),
                    };
                    break;

                default:
                    var code = ErrorCode.InternalLogic;
                    context.Result = new ObjectResult(new ApiProblemDetails(code))
                    {
                        StatusCode = code.HttpStatusCode(),
                    };
                    break;
            }
        }
    }
}