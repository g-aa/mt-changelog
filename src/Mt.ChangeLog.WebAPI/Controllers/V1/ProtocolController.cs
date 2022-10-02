﻿using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mt.ChangeLog.Logic.Features.Protocol;
using Mt.ChangeLog.TransferObjects.Other;
using Mt.ChangeLog.TransferObjects.Protocol;
using Mt.ChangeLog.WebAPI.Infrastracture;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Mt.ChangeLog.WebAPI.Controllers.V1
{
    /// <summary>
    /// Контроллер для работы с протоколами.
    /// </summary>
    [Route("api/protocol")]
    public sealed class ProtocolController : ApiControllerBase
    {
        /// <summary>
        /// Инициализация экземпляра класса <see cref="ProtocolController"/>.
        /// </summary>
        /// <param name="mediator">Медиатор.</param>
        public ProtocolController(IMediator mediator) : base(mediator)
        {
        }

        /// <summary>
        /// Получить все краткие модели <see cref="ProtocolShortModel"/>.
        /// </summary>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpGet]
        [Route("short")]
        [SwaggerResponse(StatusCodes.Status200OK, "Полный перечень кратких моделей протоколов.", typeof(IEnumerable<ProtocolShortModel>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Ошибка в логике приложения, ошибка валидации.", typeof(ApiProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Пользователь не авторизован.", typeof(ApiProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера.", typeof(ApiProblemDetails))]
        public async Task<IActionResult> GetShortModels(CancellationToken token = default)
        {
            var query = new GetShorts.Query();
            var result = await this.mediator.Send(query, token);
            return this.Ok(result);
        }

        /// <summary>
        /// Получить все модели <see cref="ProtocolTableModel"/> представления для таблиц.
        /// </summary>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpGet]
        [Route("table")]
        [SwaggerResponse(StatusCodes.Status200OK, "Полный перечень моделей протоколов для табличного представления.", typeof(IEnumerable<ProtocolTableModel>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Ошибка в логике приложения, ошибка валидации.", typeof(ApiProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Пользователь не авторизован.", typeof(ApiProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера.", typeof(ApiProblemDetails))]
        public async Task<IActionResult> GetTableModels(CancellationToken token = default)
        {
            var query = new GetTables.Query();
            var result = await this.mediator.Send(query, token);
            return this.Ok(result);
        }

        /// <summary>
        /// Получить шаблон модели <see cref="ProtocolModel"/>.
        /// </summary>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpGet]
        [Route("template")]
        [SwaggerResponse(StatusCodes.Status200OK, "Шаблон полной модели протокола.", typeof(ProtocolModel))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Ошибка в логике приложения, ошибка валидации.", typeof(ApiProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Пользователь не авторизован.", typeof(ApiProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера.", typeof(ApiProblemDetails))]
        public async Task<IActionResult> GetTemplateModel(CancellationToken token = default)
        {
            var query = new GetTemplate.Query();
            var result = await this.mediator.Send(query, token);
            return this.Ok(result);
        }

        /// <summary>
        /// Получить полную модель <see cref="ProtocolModel"/> по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpGet]
        [Route("{id:guid}")]
        [SwaggerResponse(StatusCodes.Status200OK, "Модель протокола.", typeof(ProtocolModel))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Ошибка в логике приложения, ошибка валидации.", typeof(ApiProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Пользователь не авторизован.", typeof(ApiProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера.", typeof(ApiProblemDetails))]
        public async Task<IActionResult> GetModel([FromRoute] Guid id, CancellationToken token = default)
        {
            var query = new GetById.Query(new BaseModel() { Id = id });
            var result = await this.mediator.Send(query, token);
            return this.Ok(result);
        }

        /// <summary>
        /// Добавить новый протокол в систему.
        /// </summary>
        /// <param name="model">Модель.</param>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpPost]
        [SwaggerResponse(StatusCodes.Status200OK, "Модель протокола добавлена в систему, ID модели в системе.", typeof(BaseModel))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Ошибка в логике приложения, ошибка валидации.", typeof(ApiProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Пользователь не авторизован.", typeof(ApiProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера.", typeof(ApiProblemDetails))]
        public async Task<IActionResult> PostModel([FromBody] ProtocolModel model, CancellationToken token = default)
        {
            var command = new Add.Command(model);
            var baseModel = await this.mediator.Send(command, token);
            return this.Ok(baseModel);
        }

        /// <summary>
        /// Обновить протокол в системе.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <param name="model">Модель.</param>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpPut]
        [Route("{id:guid}")]
        [SwaggerResponse(StatusCodes.Status200OK, "Модель протокола обновлена в системе.", typeof(StatusModel))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Ошибка в логике приложения, ошибка валидации.", typeof(ApiProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Пользователь не авторизован.", typeof(ApiProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера.", typeof(ApiProblemDetails))]
        public async Task<IActionResult> PutModel([FromRoute] Guid id, [FromBody] ProtocolModel model, CancellationToken token = default)
        {
            this.CheckGuids(id, model.Id);
            var command = new Update.Command(model);
            var result = await this.mediator.Send(command, token);
            return this.Ok(result);
        }

        /// <summary>
        /// Удалить протокол из системы.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpDelete]
        [Route("{id:guid}")]
        [SwaggerResponse(StatusCodes.Status200OK, "Модель протокола удалена из системы.", typeof(StatusModel))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Ошибка в логике приложения, ошибка валидации.", typeof(ApiProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Пользователь не авторизован.", typeof(ApiProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера.", typeof(ApiProblemDetails))]
        public async Task<IActionResult> DeleteModel([FromRoute] Guid id, CancellationToken token = default)
        {
            var command = new Delete.Command(new BaseModel() { Id = id });
            var result = await this.mediator.Send(command, token);
            return this.Ok(result);
        }
    }
}