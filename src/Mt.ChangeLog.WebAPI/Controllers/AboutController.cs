using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mt.ChangeLog.WebAPI.Infrastracture;
using Mt.Results;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;

namespace Mt.ChangeLog.WebAPI.Controllers
{
    /// <summary>
    /// Методы работы с квитанциями.
    /// </summary>
    [ApiController]
    [Route("api/about")]
    [Produces("application/json")]
    public class AboutController : ControllerBase
    {
        /// <summary>
        /// Получить версию приложения.
        /// </summary>
        /// <returns>Версия приложения.</returns>
        [HttpGet]
        [Route("version")]
        [SwaggerResponse(StatusCodes.Status200OK, "Версия приложения.", typeof(MtMessageResult))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Ошибка в логике приложения, ошибка валидации.", typeof(MtProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера.", typeof(MtProblemDetails))]
        public async Task<IActionResult> Version()
        {
            return await Task.FromResult(this.Ok(new MtMessageResult(Program.AppName)));
        }

        /// <summary>
        /// Получить описание приложения.
        /// </summary>
        /// <returns>Описание приложения.</returns>
        [HttpGet]
        [Route("description")]
        [SwaggerResponse(StatusCodes.Status200OK, "Версия приложения.", typeof(MtAppDescriptionModel))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Ошибка в логике приложения, ошибка валидации.", typeof(MtProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера.", typeof(MtProblemDetails))]
        public async Task<IActionResult> Description()
        {
            return await Task.FromResult(this.Ok(new MtAppDescriptionModel()
            {
                Version = Program.AppName,
                Copyright = "НТЦ Механотроники 1993 – 2022.",
                Repository = "https://github.com/g-aa/mt-changelog",
                Description = "Приложение предназначено для "
                    + "отслеживания и регистрации изменений, "
                    + "в программном обеспечении устройств автоматизации "
                    + "(БМРЗ-100/120/150/160/M4) "
                    + "электроэнергетической системы (ЭЭС).",
            }));
        }
    }
}