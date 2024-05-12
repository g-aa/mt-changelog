using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Mt.Results;
using Swashbuckle.AspNetCore.Annotations;

namespace Mt.ChangeLog.WebAPI.Controllers;

/// <summary>
/// Методы работы с квитанциями.
/// </summary>
[ApiVersionNeutral]
[Route("api/about")]
public sealed class AboutController : ControllerBase
{
    /// <summary>
    /// Получить версию приложения.
    /// </summary>
    /// <returns>Версия приложения.</returns>
    [HttpGet("version")]
    [SwaggerResponse(StatusCodes.Status200OK, "Версия приложения.", typeof(MtMessageResult))]
    public MtMessageResult Version()
    {
        return new MtMessageResult(Program.CurrentVersion);
    }

    /// <summary>
    /// Получить описание приложения.
    /// </summary>
    /// <returns>Описание приложения.</returns>
    [HttpGet("description")]
    [SwaggerResponse(StatusCodes.Status200OK, "Описание приложения.", typeof(MtAppDescription))]
    public MtAppDescription Description()
    {
        return new MtAppDescription
        {
            Version = Program.CurrentVersion,
            Repository = "https://github.com/g-aa/mt-changelog",
            Description = "Приложение предназначено для "
                + "отслеживания и регистрации изменений, "
                + "в программном обеспечении устройств автоматизации "
                + "(БМРЗ-100/120/150/160/M4) "
                + "электроэнергетической системы (ЭЭС).",
        };
    }
}