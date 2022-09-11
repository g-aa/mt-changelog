using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mt.ChangeLog.Logic.Features.ArmEdit;
using Mt.ChangeLog.TransferObjects.ArmEdit;
using Mt.ChangeLog.TransferObjects.Other;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Mt.ChangeLog.WebAPI.Controllers.V1
{
    /// <summary>
    /// Контроллер для работы с ArmEdit.
    /// </summary>
    [ApiController]
    [Route("api/arm-edit")]
    [Produces("application/json")]
    public sealed class ArmEditController : ControllerBase
    {
        /// <summary>
        /// Медиатор.
        /// </summary>
        private readonly IMediator mediator;

        /// <summary>
        /// Инициализация экземпляра класса <see cref="ArmEditController"/>.
        /// </summary>
        /// <param name="mediator">Медиатор.</param>
        public ArmEditController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        /// <summary>
        /// Получить все краткие модели <see cref="ArmEditShortModel"/>.
        /// </summary>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpGet]
        [Route("short")]
        [SwaggerResponse(StatusCodes.Status200OK, "Полный перечень кратких моделей ArmEdit.", typeof(IEquatable<ArmEditShortModel>))]
        public async Task<IActionResult> GetShortModels(CancellationToken token = default)
        {
            var query = new GetShorts.Query();
            var result = await this.mediator.Send(query, token);
            return this.Ok(result);
        }

        /// <summary>
        /// Получить все модели <see cref="ArmEditTableModel"/> представления для таблиц.
        /// </summary>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpGet]
        [Route("table")]
        [SwaggerResponse(StatusCodes.Status200OK, "Полный перечень моделей ArmEdit для табличного представления.", typeof(IEquatable<ArmEditTableModel>))]
        public async Task<IActionResult> GetTableModels(CancellationToken token = default)
        {
            var query = new GetTables.Query();
            var result = await this.mediator.Send(query, token);
            return this.Ok(result);
        }

        /// <summary>
        /// Получить шаблон модели <see cref="ArmEditModel"/>.
        /// </summary>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpGet]
        [Route("template")]
        [SwaggerResponse(StatusCodes.Status200OK, "Шаблон полной модели ArmEdit.", typeof(ArmEditModel))]
        public async Task<IActionResult> GetTemplateModel(CancellationToken token = default)
        {
            var query = new GetTemplate.Query();
            var result = await this.mediator.Send(query, token);
            return this.Ok(result);
        }

        /// <summary>
        /// Получить актуальную версию модели <see cref="ArmEditModel"/>.
        /// </summary>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpGet]
        [Route("actual")]
        [SwaggerResponse(StatusCodes.Status200OK, "Актуальная версия модели ArmEdit.", typeof(ArmEditModel))]
        public async Task<IActionResult> GetActualModel(CancellationToken token = default)
        {
            var query = new GetActual.Query();
            var result = await this.mediator.Send(query, token);
            return this.Ok(result);
        }

        /// <summary>
        /// Получить полную модель <see cref="ArmEditModel"/> по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpGet]
        [Route("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, "Модель ArmEdit.", typeof(ArmEditModel))]
        public async Task<IActionResult> GetModel([FromQuery] Guid id, CancellationToken token = default)
        {
            var query = new GetById.Query(new BaseModel() { Id = id });
            var result = await this.mediator.Send(query, token);
            return this.Ok(result);
        }

        /// <summary>
        /// Добавить новый ArmEdit в систему.
        /// </summary>
        /// <param name="model">Модель.</param>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpPost]
        [SwaggerResponse(StatusCodes.Status200OK, "Модель ArmEdit добавлена в систему, ID модели в системе.", typeof(BaseModel))]
        public async Task<IActionResult> PostModel([FromBody] ArmEditModel model, CancellationToken token = default)
        {
            var command = new Add.Command(model);
            var baseModel = await this.mediator.Send(command, token);
            return this.Ok(baseModel);
        }

        /// <summary>
        /// Обновить ArmEdit в системе.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <param name="model">Модель.</param>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpPut]
        [Route("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, "Модель ArmEdit обновлена в системе.", typeof(StatusModel))]
        public async Task<IActionResult> PutModel([FromQuery] Guid id, [FromBody] ArmEditModel model, CancellationToken token = default)
        {
            if (id != model.Id)
            {
                throw new ArgumentException($"url id = {id} is not equal to entity id = {model.Id}");
            }
            var command = new Update.Command(model);
            var result = await this.mediator.Send(command, token);
            return this.Ok(result);
        }

        /// <summary>
        /// Удалить ArmEdit из системы.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpDelete]
        [Route("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, "Модель ArmEdit удалена из системы.", typeof(StatusModel))]
        public async Task<IActionResult> DeleteModel([FromQuery] Guid id, CancellationToken token = default)
        {
            var command = new Delete.Command(new BaseModel() { Id = id });
            var result = await this.mediator.Send(command, token);
            return this.Ok(result);
        }
    }
}