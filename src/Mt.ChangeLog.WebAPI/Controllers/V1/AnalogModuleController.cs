using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mt.ChangeLog.Logic.Features.AnalogModule;
using Mt.ChangeLog.TransferObjects.AnalogModule;
using Mt.ChangeLog.TransferObjects.Other;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Mt.ChangeLog.WebAPI.Controllers.V1
{
    /// <summary>
    /// Контроллер для работы с аналоговыми модулями.
    /// </summary>
    [ApiController]
    [Route("api/analog-module")]
    [Produces("application/json")]
    public sealed class AnalogModuleController : ControllerBase
    {
        /// <summary>
        /// Медиатор.
        /// </summary>
        private readonly IMediator mediator;

        /// <summary>
        /// Инициализация экземпляра класса <see cref="AnalogModuleController"/>.
        /// </summary>
        /// <param name="mediator">Медиатор.</param>
        public AnalogModuleController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        /// <summary>
        /// Получить все краткие модели аналогового модуля <see cref="AnalogModuleShortModel"/>.
        /// </summary>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpGet]
        [Route("short")]
        [SwaggerResponse(StatusCodes.Status200OK, "Полный перечень кратких моделей аналогового модуля.", typeof(IEquatable<AnalogModuleShortModel>))]
        public async Task<IActionResult> GetShortModels(CancellationToken token = default)
        {
            var query = new GetShorts.Query();
            var result = await this.mediator.Send(query, token);
            return this.Ok(result);
        }

        /// <summary>
        /// Получить все модели <see cref="AnalogModuleTableModel"> представления для таблиц.
        /// </summary>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpGet]
        [Route("table")]
        [SwaggerResponse(StatusCodes.Status200OK, "Полный перечень моделей аналогового модуля для табличного представления.", typeof(IEquatable<AnalogModuleTableModel>))]
        public async Task<IActionResult> GetTableModels(CancellationToken token = default)
        {
            var query = new GetTables.Query();
            var result = await this.mediator.Send(query, token);
            return this.Ok(result);
        }

        /// <summary>
        /// Получить шаблон модели <see cref="AnalogModuleModel">.
        /// </summary>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpGet]
        [Route("template")]
        [SwaggerResponse(StatusCodes.Status200OK, "Шаблон полной модели аналогового модуля.", typeof(AnalogModuleModel))]
        public async Task<IActionResult> GetTemplateModel(CancellationToken token = default)
        {
            var query = new GetTemplate.Query();
            var result = await this.mediator.Send(query, token);
            return this.Ok(result);
        }

        /// <summary>
        /// Получить полную модель аналогового модуля по ID.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpGet]
        [Route("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, "Модель аналогового модуля.", typeof(AnalogModuleModel))]
        public async Task<IActionResult> GetModel([FromQuery] Guid id, CancellationToken token = default)
        {
            var query = new GetById.Query(new BaseModel() { Id = id });
            var result = await this.mediator.Send(query, token);
            return this.Ok(result);
        }

        /// <summary>
        /// Добавить новый аналоговый модуль в систему.
        /// </summary>
        /// <param name="model">Модель.</param>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpPost]
        [SwaggerResponse(StatusCodes.Status200OK, "Модель добавлена в систему, ID модели в системе.", typeof(BaseModel))]
        public async Task<IActionResult> PostModel([FromBody] AnalogModuleModel model, CancellationToken token = default)
        {
            var command = new Add.Command(model);
            await this.mediator.Send(command, token);
            return this.Ok();
        }

        /// <summary>
        /// Обновить аналоговый модуль в системе.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <param name="model">Модель.</param>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        /// <exception cref="ArgumentException"></exception>
        [HttpPut]
        [Route("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, "Модель обновлена в системе.", typeof(StatusModel))]
        public async Task<IActionResult> PutModel([FromQuery] Guid id, [FromBody] AnalogModuleModel model, CancellationToken token = default)
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
        /// Удалить аналоговый модуль из системы.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpDelete]
        [Route("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, "Модель удалена из системы.", typeof(StatusModel))]
        public async Task<IActionResult> DeleteModel([FromQuery] Guid id, CancellationToken token = default)
        {
            var command = new Delete.Command(new BaseModel() { Id = id });
            var result = await this.mediator.Send(command, token);
            return this.Ok(result);
        }
    }
}