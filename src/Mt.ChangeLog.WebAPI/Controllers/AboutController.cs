using Microsoft.AspNetCore.Mvc;
using Mt.Results;
using Swashbuckle.AspNetCore.Annotations;

namespace Mt.ChangeLog.WebAPI.Controllers;

/// <summary>
/// Методы работы с квитанциями.
/// </summary>
[ApiController]
[Route("api/about")]
public sealed class AboutController : ControllerBase
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
    public MtMessageResult Version()
    {
        return new MtMessageResult(Program.ServiceName);
    }

    /// <summary>
    /// Получить описание приложения.
    /// </summary>
    /// <returns>Описание приложения.</returns>
    [HttpGet]
    [Route("description")]
    [SwaggerResponse(StatusCodes.Status200OK, "Версия приложения.", typeof(MtAppDescription))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Ошибка в логике приложения, ошибка валидации.", typeof(MtProblemDetails))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера.", typeof(MtProblemDetails))]
    public MtAppDescription Description()
    {
        return new MtAppDescription()
        {
            Version = Program.ServiceName,
            Repository = "https://github.com/g-aa/mt-changelog",
            Description = "Приложение предназначено для "
                + "отслеживания и регистрации изменений, "
                + "в программном обеспечении устройств автоматизации "
                + "(БМРЗ-100/120/150/160/M4) "
                + "электроэнергетической системы (ЭЭС).",
        };
    }
}