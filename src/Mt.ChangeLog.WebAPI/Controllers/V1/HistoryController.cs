﻿using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mt.ChangeLog.Logic.Features.History;
using Mt.ChangeLog.TransferObjects.Historical;
using Mt.ChangeLog.TransferObjects.Other;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
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
            var query = new GetProjectVersionHistory.Query(new BaseModel() { Id = id });
            var result = await this.mediator.Send(query, token);
            return this.Ok(result);
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
            var query = new GetProjectRevisionHistory.Query(new BaseModel() { Id = id });
            var result = await this.mediator.Send(query, token);
            return this.Ok(result);
        }

        /// <summary>
        /// Получить перечень наименование версий проектов.
        /// </summary>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpGet]
        [Route("project/title")]
        [SwaggerResponse(StatusCodes.Status200OK, "Полный перечень наименование проектов.", typeof(IEnumerable<string>))]
        public async Task<IActionResult> GetProjectTitles(CancellationToken token = default)
        {
            var query = new GetProjectTitles.Query();
            var result = await this.mediator.Send(query, token);
            return this.Ok(result);
        }

        /// <summary>
        /// Получить дерево изменений проекта.
        /// </summary>
        /// <param name="title">Наименование версии проекта.</param>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpGet]
        [Route("tree/{title:string}")]
        [SwaggerResponse(StatusCodes.Status200OK, "Перечень моделий для дерева изменений.", typeof(IEnumerable<ProjectRevisionTreeModel>))]
        public async Task<IActionResult> GetTreeModel([FromRoute] string title, CancellationToken token = default)
        {
            var query = new GetProjectTree.Query(title);
            var result = await this.mediator.Send(query, token);
            return this.Ok(result);
        }

        /// <summary>
        /// Получить статистику по имеющимся данным в системе.
        /// </summary>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpGet]
        [Route("statistics")]
        [SwaggerResponse(StatusCodes.Status200OK, "Получить статистику по имеющимся данным в системе.", typeof(StatisticsModel))]
        public async Task<IActionResult> GetStatisticsModel(CancellationToken token = default)
        {
            var query = new GetStatistics.Query();
            var result = await this.mediator.Send(query, token);
            return this.Ok(result);
        }
    }
}