using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Mt.Results;
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
        /// <summary>
        /// Передавать детализацию об исключениях. 
        /// </summary>
        private readonly bool passDetails;

        /// <summary>
        /// Инициализация экземпляра класса <see cref="ApiExceptionFilter"/>.
        /// </summary>
        public ApiExceptionFilter()
        {
            this.passDetails = false;
        }

        /// <inheritdoc />
        public void OnException(ExceptionContext context)
        {
            Check.NotNull(context, nameof(context));
            var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<ApiExceptionFilter>>();
            
            switch (context.Exception)
            {
                case MtException exception:
                    logger?.LogWarning(context.Exception, context.Exception.Message);
                    context.Result = new ObjectResult(new MtProblemDetails(exception))
                    {
                        StatusCode = exception.Code.HttpStatusCode(),
                    };
                    break;

                case MtBaseException exception:
                    logger?.LogWarning(context.Exception, context.Exception.Message);
                    context.Result = new ObjectResult(new MtProblemDetails(exception))
                    {
                        StatusCode = 400,
                    };
                    break;

                default:
                    logger?.LogError(context.Exception, context.Exception.Message);
                    var code = ErrorCode.InternalServerError;
                    var details = new MtProblemDetails(code.Title(), this.passDetails ? context.Exception.Message : code.Desc());
                    context.Result = new ObjectResult(details)
                    {
                        StatusCode = code.HttpStatusCode(),
                    };
                    break;
            }
        }
    }
}