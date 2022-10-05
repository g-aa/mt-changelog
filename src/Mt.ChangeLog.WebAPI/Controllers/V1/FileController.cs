using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mt.ChangeLog.Logic.Features.File;
using Mt.ChangeLog.Logic.Features.History;
using Mt.ChangeLog.TransferObjects.Other;
using Mt.ChangeLog.WebAPI.Infrastracture;
using Mt.Utilities.IO;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Mt.ChangeLog.WebAPI.Controllers.V1
{
    /// <summary>
    /// Контроллер для работы с файлами.
    /// </summary>
    [Route("api/file")]
    public sealed class FileController : ApiControllerBase
    {
        /// <summary>
        /// Инициализация экземпляра класса <see cref="FileController"/>.
        /// </summary>
        /// <param name="mediator">Медиатор.</param>
        public FileController(IMediator mediator) : base(mediator)
        {
        }

        /// <summary>
        /// Получить полную историю изменения версии проекта.
        /// </summary>
        /// <param name="id">Идентификатор версии проекта.</param>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpGet]
        [Route("changelog/{id:guid}")]
        [SwaggerResponse(StatusCodes.Status200OK, "Полная история изменения версии проекта.", typeof(FileModel))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Ошибка в логике приложения, ошибка валидации.", typeof(ApiProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Пользователь не авторизован.", typeof(ApiProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера.", typeof(ApiProblemDetails))]
        public async Task<IActionResult> GetProjectVersionChangeLog([FromRoute] Guid id, CancellationToken token = default)
        {
            var query = new GetProjectVersionHistory.Query(new BaseModel() { Id = id });
            var result = await this.mediator.Send(query, token);
            return this.Ok(result);
        }

        /// <summary>
        /// Получить полный архив с перечнем логов изменений из системы.
        /// </summary>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpGet]
        [Route("changelog/archive/full")]
        [SwaggerResponse(StatusCodes.Status200OK, "Полный архив логов изменения проектов.", typeof(FileModel))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Ошибка в логике приложения, ошибка валидации.", typeof(ApiProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Пользователь не авторизован.", typeof(ApiProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера.", typeof(ApiProblemDetails))]
        public async Task<IActionResult> GetChangeLogArchive(CancellationToken token = default)
        {
            var query = new GetFullArchive.Query();
            var result = await this.mediator.Send(query, token);
            return this.Ok(result);
        }
    }
}