using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mt.ChangeLog.Logic.Features.RelayAlgorithm;
using Mt.ChangeLog.TransferObjects.RelayAlgorithm;
using Mt.ChangeLog.TransferObjects.Other;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Mt.ChangeLog.WebAPI.Controllers.V1
{
    /// <summary>
    /// Контроллер для работы с алгоритмами.
    /// </summary>
    [Route("api/relay-algorithm")]
    public class RelayAlgorithmController : ApiControllerBase
    {
        /// <summary>
        /// Инициализация экземпляра класса <see cref="RelayAlgorithmController"/>.
        /// </summary>
        /// <param name="mediator">Медиатор.</param>
        public RelayAlgorithmController(IMediator mediator) : base(mediator)
        {
        }

        /// <summary>
        /// Получить все краткие модели <see cref="RelayAlgorithmShortModel"/>.
        /// </summary>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpGet]
        [Route("short")]
        [SwaggerResponse(StatusCodes.Status200OK, "Полный перечень кратких моделей алгоритмов.", typeof(IEnumerable<RelayAlgorithmShortModel>))]
        public async Task<IActionResult> GetShortModels(CancellationToken token = default)
        {
            var query = new GetShorts.Query();
            var result = await this.mediator.Send(query, token);
            return this.Ok(result);
        }

        /// <summary>
        /// Получить все модели <see cref="RelayAlgorithmTableModel"/> представления для таблиц.
        /// </summary>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpGet]
        [Route("table")]
        [SwaggerResponse(StatusCodes.Status200OK, "Полный перечень моделей алгоритмов для табличного представления.", typeof(IEnumerable<RelayAlgorithmTableModel>))]
        public async Task<IActionResult> GetTableModels(CancellationToken token = default)
        {
            var query = new GetTables.Query();
            var result = await this.mediator.Send(query, token);
            return this.Ok(result);
        }

        /// <summary>
        /// Получить шаблон модели <see cref="RelayAlgorithmModel"/>.
        /// </summary>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpGet]
        [Route("template")]
        [SwaggerResponse(StatusCodes.Status200OK, "Шаблон полной модели алгоритма.", typeof(RelayAlgorithmModel))]
        public async Task<IActionResult> GetTemplateModel(CancellationToken token = default)
        {
            var query = new GetTemplate.Query();
            var result = await this.mediator.Send(query, token);
            return this.Ok(result);
        }

        /// <summary>
        /// Получить полную модель <see cref="RelayAlgorithmModel"/> по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpGet]
        [Route("{id:guid}")]
        [SwaggerResponse(StatusCodes.Status200OK, "Модель алгоритма.", typeof(RelayAlgorithmModel))]
        public async Task<IActionResult> GetModel([FromRoute] Guid id, CancellationToken token = default)
        {
            var query = new GetById.Query(new BaseModel() { Id = id });
            var result = await this.mediator.Send(query, token);
            return this.Ok(result);
        }

        /// <summary>
        /// Добавить новый алгоритм в систему.
        /// </summary>
        /// <param name="model">Модель.</param>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpPost]
        [SwaggerResponse(StatusCodes.Status200OK, "Модель алгоритма добавлена в систему, ID модели в системе.", typeof(BaseModel))]
        public async Task<IActionResult> PostModel([FromBody] RelayAlgorithmModel model, CancellationToken token = default)
        {
            var command = new Add.Command(model);
            var baseModel = await this.mediator.Send(command, token);
            return this.Ok(baseModel);
        }

        /// <summary>
        /// Обновить алгоритм в системе.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <param name="model">Модель.</param>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpPut]
        [Route("{id:guid}")]
        [SwaggerResponse(StatusCodes.Status200OK, "Модель алгоритма обновлена в системе.", typeof(StatusModel))]
        public async Task<IActionResult> PutModel([FromRoute] Guid id, [FromBody] RelayAlgorithmModel model, CancellationToken token = default)
        {
            this.CheckGuids(id, model.Id);
            var command = new Update.Command(model);
            var result = await this.mediator.Send(command, token);
            return this.Ok(result);
        }

        /// <summary>
        /// Удалить алгоритм из системы.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpDelete]
        [Route("{id:guid}")]
        [SwaggerResponse(StatusCodes.Status200OK, "Модель алгоритма удалена из системы.", typeof(StatusModel))]
        public async Task<IActionResult> DeleteModel([FromQuery] Guid id, CancellationToken token = default)
        {
            var command = new Delete.Command(new BaseModel() { Id = id });
            var result = await this.mediator.Send(command, token);
            return this.Ok(result);
        }
    }
}