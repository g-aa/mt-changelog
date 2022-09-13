using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mt.ChangeLog.TransferObjects.Historical;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Mt.ChangeLog.WebAPI.Controllers.V1
{
    /// <summary>
    /// Контроллер для работы с историями изменений.
    /// </summary>
    [Route("api/history")]
    public class HistoryController : ApiControllerBase
    {
        /// <summary>
        /// Инициализация экземпляра класса <see cref="HistoryController"/>.
        /// </summary>
        /// <param name="mediator">Медиатор.</param>
        public HistoryController(IMediator mediator) : base(mediator)
        {
        }

        /// <summary>
        /// Получить историю изменения для версии проета.
        /// </summary>
        /// <param name="id">Идентификатор версии проекта.</param>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpGet]
        [Route("version/{id:guid}")]
        [SwaggerResponse(StatusCodes.Status200OK, "История изменения версии проекта.", typeof(ProjectVersionHistoryModel))]
        public async Task<IActionResult> GetVersionHistoryModel([FromRoute] Guid id, CancellationToken token = default)
        {
            return this.Ok();
        }

        /// <summary>
        /// Получить историю изменения для редакции проета.
        /// </summary>
        /// <param name="id">Идентификатор редакции проекта.</param>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpGet]
        [Route("revision/{id:guid}")]
        [SwaggerResponse(StatusCodes.Status200OK, "История изменения редакции проекта.", typeof(ProjectRevisionHistoryModel))]
        public async Task<IActionResult> GetRevisionHistoryModel([FromRoute] Guid id, CancellationToken token = default)
        {
            return this.Ok();
        }
    }
}