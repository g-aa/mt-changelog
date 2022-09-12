using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mt.ChangeLog.TransferObjects.Other;
using Mt.ChangeLog.TransferObjects.ProjectRevision;
using Mt.ChangeLog.TransferObjects.ProjectStatus;
using Mt.ChangeLog.TransferObjects.ProjectVersion;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Mt.ChangeLog.WebAPI.Controllers.V1
{
    /// <summary>
    /// Контроллер для работы с проектами.
    /// </summary>
    [Route("api/project")]
    public sealed class ProjectController : ApiControllerBase
    {
        /// <summary>
        /// Инициализация экземпляра класса <see cref="ProjectController"/>.
        /// </summary>
        /// <param name="mediator">Медиатор.</param>
        public ProjectController(IMediator mediator) : base(mediator)
        {
        }

        #region [ Project status ]

        /// <summary>
        /// Получить все краткие модели <see cref="ProjectStatusShortModel"/>.
        /// </summary>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpGet]
        [Route("status/short")]
        [SwaggerResponse(StatusCodes.Status200OK, "Полный перечень кратких моделей статусов проекта.", typeof(IEnumerable<ProjectStatusShortModel>))]
        public async Task<IActionResult> GetStatusShortModels(CancellationToken token = default)
        {
            var query = new Mt.ChangeLog.Logic.Features.ProjectStatus.GetShorts.Query();
            var result = await this.mediator.Send(query, token);
            return this.Ok(result);
        }

        /// <summary>
        /// Получить все модели <see cref="ProjectStatusTableModel"/> представления для таблиц.
        /// </summary>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpGet]
        [Route("status/table")]
        [SwaggerResponse(StatusCodes.Status200OK, "Полный перечень моделей статусов проекта для табличного представления.", typeof(IEnumerable<ProjectStatusTableModel>))]
        public async Task<IActionResult> GetStatusTableModels(CancellationToken token = default)
        {
            var query = new Mt.ChangeLog.Logic.Features.ProjectStatus.GetTables.Query();
            var result = await this.mediator.Send(query, token);
            return this.Ok(result);
        }

        /// <summary>
        /// Получить шаблон модели <see cref="ProjectStatusModel"/>.
        /// </summary>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpGet]
        [Route("status/template")]
        [SwaggerResponse(StatusCodes.Status200OK, "Шаблон полной модели статуса проекта.", typeof(ProjectStatusModel))]
        public async Task<IActionResult> GetStatusTemplateModel(CancellationToken token = default)
        {
            var query = new Mt.ChangeLog.Logic.Features.ProjectStatus.GetTemplate.Query();
            var result = await this.mediator.Send(query, token);
            return this.Ok(result);
        }

        /// <summary>
        /// Получить полную модель <see cref="ProjectStatusModel"/> по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpGet]
        [Route("status/{id:guid}")]
        [SwaggerResponse(StatusCodes.Status200OK, "Модель статуса проекта.", typeof(ProjectStatusModel))]
        public async Task<IActionResult> GetStatusModel([FromRoute] Guid id, CancellationToken token = default)
        {
            var query = new Mt.ChangeLog.Logic.Features.ProjectStatus.GetById.Query(new BaseModel() { Id = id });
            var result = await this.mediator.Send(query, token);
            return this.Ok(result);
        }

        /// <summary>
        /// Добавить новый статус проекта в систему.
        /// </summary>
        /// <param name="model">Модель.</param>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpPost]
        [Route("status")]
        [SwaggerResponse(StatusCodes.Status200OK, "Модель статуса проекта добавлена в систему, ID модели в системе.", typeof(BaseModel))]
        public async Task<IActionResult> PostStatusModel([FromBody] ProjectStatusModel model, CancellationToken token = default)
        {
            var command = new Mt.ChangeLog.Logic.Features.ProjectStatus.Add.Command(model);
            var baseModel = await this.mediator.Send(command, token);
            return this.Ok(baseModel);
        }

        /// <summary>
        /// Обновить статус проекта в системе.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <param name="model">Модель.</param>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpPut]
        [Route("status/{id:guid}")]
        [SwaggerResponse(StatusCodes.Status200OK, "Модель статуса проекта обновлена в системе.", typeof(StatusModel))]
        public async Task<IActionResult> PutStatusModel([FromRoute] Guid id, [FromBody] ProjectStatusModel model, CancellationToken token = default)
        {
            this.CheckGuids(id, model.Id);
            var command = new Mt.ChangeLog.Logic.Features.ProjectStatus.Update.Command(model);
            var result = await this.mediator.Send(command, token);
            return this.Ok(result);
        }

        /// <summary>
        /// Удалить статус проекта из системы.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpDelete]
        [Route("status/{id:guid}")]
        [SwaggerResponse(StatusCodes.Status200OK, "Модель статуса проекта удалена из системы.", typeof(StatusModel))]
        public async Task<IActionResult> DeleteStatusModel([FromRoute] Guid id, CancellationToken token = default)
        {
            var command = new Mt.ChangeLog.Logic.Features.ProjectStatus.Delete.Command(new BaseModel() { Id = id });
            var result = await this.mediator.Send(command, token);
            return this.Ok(result);
        }

        #endregion

        #region [ Project version ]

        /// <summary>
        /// Получить все краткие модели <see cref="ProjectVersionShortModel"/>.
        /// </summary>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpGet]
        [Route("version/short")]
        [SwaggerResponse(StatusCodes.Status200OK, "Полный перечень кратких моделей версий проекта.", typeof(IEnumerable<ProjectVersionShortModel>))]
        public async Task<IActionResult> GetVersionShortModels(CancellationToken token = default)
        {
            var query = new Mt.ChangeLog.Logic.Features.ProjectVersion.GetShorts.Query();
            var result = await this.mediator.Send(query, token);
            return this.Ok(result);
        }

        /// <summary>
        /// Получить все модели <see cref="ProjectVersionTableModel"/> представления для таблиц.
        /// </summary>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpGet]
        [Route("version/table")]
        [SwaggerResponse(StatusCodes.Status200OK, "Полный перечень моделей версий проекта для табличного представления.", typeof(IEnumerable<ProjectVersionTableModel>))]
        public async Task<IActionResult> GetVersionTableModels(CancellationToken token = default)
        {
            var query = new Mt.ChangeLog.Logic.Features.ProjectVersion.GetTables.Query();
            var result = await this.mediator.Send(query, token);
            return this.Ok(result);
        }

        /// <summary>
        /// Получить шаблон модели <see cref="ProjectVersionModel"/>.
        /// </summary>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpGet]
        [Route("version/template")]
        [SwaggerResponse(StatusCodes.Status200OK, "Шаблон полной модели версии проекта.", typeof(ProjectVersionModel))]
        public async Task<IActionResult> GetVersionTemplateModel(CancellationToken token = default)
        {
            var query = new Mt.ChangeLog.Logic.Features.ProjectVersion.GetTemplate.Query();
            var result = await this.mediator.Send(query, token);
            return this.Ok(result);
        }

        /// <summary>
        /// Получить полную модель <see cref="ProjectVersionModel"/> по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpGet]
        [Route("version/{id:guid}")]
        [SwaggerResponse(StatusCodes.Status200OK, "Модель версии проекта.", typeof(ProjectVersionModel))]
        public async Task<IActionResult> GetVersionModel([FromRoute] Guid id, CancellationToken token = default)
        {
            var query = new Mt.ChangeLog.Logic.Features.ProjectVersion.GetById.Query(new BaseModel() { Id = id });
            var result = await this.mediator.Send(query, token);
            return this.Ok(result);
        }

        /// <summary>
        /// Добавить новую версию проекта в систему.
        /// </summary>
        /// <param name="model">Модель.</param>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpPost]
        [Route("version")]
        [SwaggerResponse(StatusCodes.Status200OK, "Модель версии проекта добавлена в систему, ID модели в системе.", typeof(BaseModel))]
        public async Task<IActionResult> PostVersionModel([FromBody] ProjectVersionModel model, CancellationToken token = default)
        {
            var command = new Mt.ChangeLog.Logic.Features.ProjectVersion.Add.Command(model);
            var baseModel = await this.mediator.Send(command, token);
            return this.Ok(baseModel);
        }

        /// <summary>
        /// Обновить версии проекта в системе.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <param name="model">Модель.</param>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpPut]
        [Route("version/{id:guid}")]
        [SwaggerResponse(StatusCodes.Status200OK, "Модель версии проекта обновлена в системе.", typeof(StatusModel))]
        public async Task<IActionResult> PutVersionModel([FromRoute] Guid id, [FromBody] ProjectVersionModel model, CancellationToken token = default)
        {
            this.CheckGuids(id, model.Id);
            var command = new Mt.ChangeLog.Logic.Features.ProjectVersion.Update.Command(model);
            var result = await this.mediator.Send(command, token);
            return this.Ok(result);
        }

        /// <summary>
        /// Удалить версию проекта из системы.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpDelete]
        [Route("version/{id:guid}")]
        [SwaggerResponse(StatusCodes.Status200OK, "Модель версии проекта удалена из системы.", typeof(StatusModel))]
        public async Task<IActionResult> DeleteVersionModel([FromRoute] Guid id, CancellationToken token = default)
        {
            var command = new Mt.ChangeLog.Logic.Features.ProjectVersion.Delete.Command(new BaseModel() { Id = id });
            var result = await this.mediator.Send(command, token);
            return this.Ok(result);
        }

        #endregion

        #region [ Project revision ]

        /// <summary>
        /// Получить все краткие модели <see cref="ProjectRevisionShortModel"/>.
        /// </summary>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpGet]
        [Route("revision/short")]
        [SwaggerResponse(StatusCodes.Status200OK, "Полный перечень кратких моделей редакций проекта.", typeof(IEnumerable<ProjectRevisionShortModel>))]
        public async Task<IActionResult> GetRevisionShortModels(CancellationToken token = default)
        {
            var query = new Mt.ChangeLog.Logic.Features.ProjectRevision.GetShorts.Query();
            var result = await this.mediator.Send(query, token);
            return this.Ok(result);
        }

        /// <summary>
        /// Получить все модели <see cref="ProjectRevisionTableModel"/> представления для таблиц.
        /// </summary>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpGet]
        [Route("revision/table")]
        [SwaggerResponse(StatusCodes.Status200OK, "Полный перечень моделей ревизий проекта для табличного представления.", typeof(IEnumerable<ProjectRevisionTableModel>))]
        public async Task<IActionResult> GetRevisionTableModels(CancellationToken token = default)
        {
            var query = new Mt.ChangeLog.Logic.Features.ProjectRevision.GetTables.Query();
            var result = await this.mediator.Send(query, token);
            return this.Ok(result);
        }

        /// <summary>
        /// Получить шаблон модели <see cref="ProjectRevisionModel"/>.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpGet]
        [Route("revision/template/{id:guid}")]
        [SwaggerResponse(StatusCodes.Status200OK, "Шаблон полной модели редакции проекта.", typeof(ProjectRevisionModel))]
        public async Task<IActionResult> GetRevisionTemplateModel([FromRoute] Guid id, CancellationToken token = default)
        {
            var query = new Mt.ChangeLog.Logic.Features.ProjectRevision.GetTemplate.Query(new BaseModel() { Id = id });
            var result = await this.mediator.Send(query, token);
            return this.Ok(result);
        }

        /// <summary>
        /// Получить полную модель <see cref="ProjectRevisionModel"/> по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpGet]
        [Route("revision/{id:guid}")]
        [SwaggerResponse(StatusCodes.Status200OK, "Модель редакции проекта.", typeof(ProjectRevisionModel))]
        public async Task<IActionResult> GetRevisionModel([FromRoute] Guid id, CancellationToken token = default)
        {
            var query = new Mt.ChangeLog.Logic.Features.ProjectRevision.GetById.Query(new BaseModel() { Id = id });
            var result = await this.mediator.Send(query, token);
            return this.Ok(result);
        }

        /// <summary>
        /// Добавить новую редакцию проекта в систему.
        /// </summary>
        /// <param name="model">Модель.</param>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpPost]
        [Route("revision")]
        [SwaggerResponse(StatusCodes.Status200OK, "Модель редакции проекта добавлена в систему, ID модели в системе.", typeof(BaseModel))]
        public async Task<IActionResult> PostRevisionModel([FromBody] ProjectRevisionModel model, CancellationToken token = default)
        {
            var command = new Mt.ChangeLog.Logic.Features.ProjectRevision.Add.Command(model);
            var baseModel = await this.mediator.Send(command, token);
            return this.Ok(baseModel);
        }

        /// <summary>
        /// Обновить редакции проекта в системе.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <param name="model">Модель.</param>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpPut]
        [Route("revision/{id:guid}")]
        [SwaggerResponse(StatusCodes.Status200OK, "Модель редакции проекта обновлена в системе.", typeof(StatusModel))]
        public async Task<IActionResult> PutRevisionModel([FromRoute] Guid id, [FromBody] ProjectRevisionModel model, CancellationToken token = default)
        {
            this.CheckGuids(id, model.Id);
            var command = new Mt.ChangeLog.Logic.Features.ProjectRevision.Update.Command(model);
            var result = await this.mediator.Send(command, token);
            return this.Ok(result);
        }

        /// <summary>
        /// Удалить редакцию проекта из системы.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpDelete]
        [Route("revision/{id:guid}")]
        [SwaggerResponse(StatusCodes.Status200OK, "Модель редакции проекта удалена из системы.", typeof(StatusModel))]
        public async Task<IActionResult> DeleteRevisionModel([FromRoute] Guid id, CancellationToken token = default)
        {
            var command = new Mt.ChangeLog.Logic.Features.ProjectRevision.Delete.Command(new BaseModel() { Id = id });
            var result = await this.mediator.Send(command, token);
            return this.Ok(result);
        }

        #endregion
    }
}