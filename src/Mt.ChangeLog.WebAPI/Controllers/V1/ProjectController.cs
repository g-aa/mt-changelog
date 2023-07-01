using MediatR;
using Microsoft.AspNetCore.Mvc;
using Mt.ChangeLog.TransferObjects.Other;
using Mt.ChangeLog.TransferObjects.ProjectRevision;
using Mt.ChangeLog.TransferObjects.ProjectStatus;
using Mt.ChangeLog.TransferObjects.ProjectVersion;
using Mt.Results;
using Swashbuckle.AspNetCore.Annotations;

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
        /// Получить полный перечень кратких моделей статусов проекта.
        /// </summary>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpGet]
        [Route("status/short")]
        [SwaggerResponse(StatusCodes.Status200OK, "Полный перечень кратких моделей статусов проекта.", typeof(IEnumerable<ProjectStatusShortModel>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Ошибка в логике приложения, ошибка валидации.", typeof(MtProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Пользователь не авторизован.", typeof(MtProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера.", typeof(MtProblemDetails))]
        public async Task<IActionResult> GetStatusShortModels(CancellationToken token = default)
        {
            var query = new Mt.ChangeLog.Logic.Features.ProjectStatus.GetShorts.Query();
            var result = await this.mediator.Send(query, token);
            return this.Ok(result);
        }

        /// <summary>
        /// Получить полный перечень моделей статусов проекта для табличного представления.
        /// </summary>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpGet]
        [Route("status/table")]
        [SwaggerResponse(StatusCodes.Status200OK, "Полный перечень моделей статусов проекта для табличного представления.", typeof(IEnumerable<ProjectStatusTableModel>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Ошибка в логике приложения, ошибка валидации.", typeof(MtProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Пользователь не авторизован.", typeof(MtProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера.", typeof(MtProblemDetails))]
        public async Task<IActionResult> GetStatusTableModels(CancellationToken token = default)
        {
            var query = new Mt.ChangeLog.Logic.Features.ProjectStatus.GetTables.Query();
            var result = await this.mediator.Send(query, token);
            return this.Ok(result);
        }

        /// <summary>
        /// Получить шаблон модели статуса проекта.
        /// </summary>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpGet]
        [Route("status/template")]
        [SwaggerResponse(StatusCodes.Status200OK, "Шаблон полной модели статуса проекта.", typeof(ProjectStatusModel))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Ошибка в логике приложения, ошибка валидации.", typeof(MtProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Пользователь не авторизован.", typeof(MtProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера.", typeof(MtProblemDetails))]
        public async Task<IActionResult> GetStatusTemplateModel(CancellationToken token = default)
        {
            var query = new Mt.ChangeLog.Logic.Features.ProjectStatus.GetTemplate.Query();
            var result = await this.mediator.Send(query, token);
            return this.Ok(result);
        }

        /// <summary>
        /// Получить полную модель статуса проекта по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpGet]
        [Route("status/{id:guid}")]
        [SwaggerResponse(StatusCodes.Status200OK, "Модель статуса проекта.", typeof(ProjectStatusModel))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Ошибка в логике приложения, ошибка валидации.", typeof(MtProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Пользователь не авторизован.", typeof(MtProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера.", typeof(MtProblemDetails))]
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
        [SwaggerResponse(StatusCodes.Status200OK, "Модель статуса проекта добавлена в систему, ID модели в системе.", typeof(MtMessageResult))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Ошибка в логике приложения, ошибка валидации.", typeof(MtProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Пользователь не авторизован.", typeof(MtProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера.", typeof(MtProblemDetails))]
        public async Task<IActionResult> PostStatusModel([FromBody] ProjectStatusModel model, CancellationToken token = default)
        {
            var command = new Mt.ChangeLog.Logic.Features.ProjectStatus.Add.Command(model);
            var result = await this.mediator.Send(command, token);
            return this.Ok(new MtMessageResult(result));
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
        [SwaggerResponse(StatusCodes.Status200OK, "Модель статуса проекта обновлена в системе.", typeof(MtMessageResult))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Ошибка в логике приложения, ошибка валидации.", typeof(MtProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Пользователь не авторизован.", typeof(MtProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера.", typeof(MtProblemDetails))]
        public async Task<IActionResult> PutStatusModel([FromRoute] Guid id, [FromBody] ProjectStatusModel model, CancellationToken token = default)
        {
            await this.CheckGuidsAsync(id, model.Id);
            var command = new Mt.ChangeLog.Logic.Features.ProjectStatus.Update.Command(model);
            var result = await this.mediator.Send(command, token);
            return this.Ok(new MtMessageResult(result));
        }

        /// <summary>
        /// Удалить статус проекта из системы.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpDelete]
        [Route("status/{id:guid}")]
        [SwaggerResponse(StatusCodes.Status200OK, "Модель статуса проекта удалена из системы.", typeof(MtMessageResult))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Ошибка в логике приложения, ошибка валидации.", typeof(MtProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Пользователь не авторизован.", typeof(MtProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера.", typeof(MtProblemDetails))]
        public async Task<IActionResult> DeleteStatusModel([FromRoute] Guid id, CancellationToken token = default)
        {
            var command = new Mt.ChangeLog.Logic.Features.ProjectStatus.Delete.Command(new BaseModel() { Id = id });
            var result = await this.mediator.Send(command, token);
            return this.Ok(new MtMessageResult(result));
        }

        #endregion

        #region [ Project version ]

        /// <summary>
        /// Получить полный перечень кратких моделей версий проектов.
        /// </summary>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpGet]
        [Route("version/short")]
        [SwaggerResponse(StatusCodes.Status200OK, "Полный перечень кратких моделей версий проекта.", typeof(IEnumerable<ProjectVersionShortModel>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Ошибка в логике приложения, ошибка валидации.", typeof(MtProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Пользователь не авторизован.", typeof(MtProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера.", typeof(MtProblemDetails))]
        public async Task<IActionResult> GetVersionShortModels(CancellationToken token = default)
        {
            var query = new Mt.ChangeLog.Logic.Features.ProjectVersion.GetShorts.Query();
            var result = await this.mediator.Send(query, token);
            return this.Ok(result);
        }

        /// <summary>
        /// Получить полный перечень модели версий проектов для табличного представления.
        /// </summary>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpGet]
        [Route("version/table")]
        [SwaggerResponse(StatusCodes.Status200OK, "Полный перечень моделей версий проекта для табличного представления.", typeof(IEnumerable<ProjectVersionTableModel>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Ошибка в логике приложения, ошибка валидации.", typeof(MtProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Пользователь не авторизован.", typeof(MtProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера.", typeof(MtProblemDetails))]
        public async Task<IActionResult> GetVersionTableModels(CancellationToken token = default)
        {
            var query = new Mt.ChangeLog.Logic.Features.ProjectVersion.GetTables.Query();
            var result = await this.mediator.Send(query, token);
            return this.Ok(result);
        }

        /// <summary>
        /// Получить перечень наименование версий проектов.
        /// </summary>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpGet]
        [Route("version/title")]
        [SwaggerResponse(StatusCodes.Status200OK, "Полный перечень наименование версий проектов.", typeof(IEnumerable<string>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Ошибка в логике приложения, ошибка валидации.", typeof(MtProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Пользователь не авторизован.", typeof(MtProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера.", typeof(MtProblemDetails))]
        public async Task<IActionResult> GetProjectTitles(CancellationToken token = default)
        {
            var query = new Mt.ChangeLog.Logic.Features.ProjectVersion.GetTitles.Query();
            var result = await this.mediator.Send(query, token);
            return this.Ok(result);
        }

        /// <summary>
        /// Получить шаблон модели версии проекта.
        /// </summary>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpGet]
        [Route("version/template")]
        [SwaggerResponse(StatusCodes.Status200OK, "Шаблон полной модели версии проекта.", typeof(ProjectVersionModel))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Ошибка в логике приложения, ошибка валидации.", typeof(MtProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Пользователь не авторизован.", typeof(MtProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера.", typeof(MtProblemDetails))]
        public async Task<IActionResult> GetVersionTemplateModel(CancellationToken token = default)
        {
            var query = new Mt.ChangeLog.Logic.Features.ProjectVersion.GetTemplate.Query();
            var result = await this.mediator.Send(query, token);
            return this.Ok(result);
        }

        /// <summary>
        /// Получить полную модель версии проекта по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpGet]
        [Route("version/{id:guid}")]
        [SwaggerResponse(StatusCodes.Status200OK, "Модель версии проекта.", typeof(ProjectVersionModel))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Ошибка в логике приложения, ошибка валидации.", typeof(MtProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Пользователь не авторизован.", typeof(MtProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера.", typeof(MtProblemDetails))]
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
        [SwaggerResponse(StatusCodes.Status200OK, "Модель версии проекта добавлена в систему, ID модели в системе.", typeof(MtMessageResult))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Ошибка в логике приложения, ошибка валидации.", typeof(MtProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Пользователь не авторизован.", typeof(MtProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера.", typeof(MtProblemDetails))]
        public async Task<IActionResult> PostVersionModel([FromBody] ProjectVersionModel model, CancellationToken token = default)
        {
            var command = new Mt.ChangeLog.Logic.Features.ProjectVersion.Add.Command(model);
            var result = await this.mediator.Send(command, token);
            return this.Ok(new MtMessageResult(result));
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
        [SwaggerResponse(StatusCodes.Status200OK, "Модель версии проекта обновлена в системе.", typeof(MtMessageResult))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Ошибка в логике приложения, ошибка валидации.", typeof(MtProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Пользователь не авторизован.", typeof(MtProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера.", typeof(MtProblemDetails))]
        public async Task<IActionResult> PutVersionModel([FromRoute] Guid id, [FromBody] ProjectVersionModel model, CancellationToken token = default)
        {
            await this.CheckGuidsAsync(id, model.Id);
            var command = new Mt.ChangeLog.Logic.Features.ProjectVersion.Update.Command(model);
            var result = await this.mediator.Send(command, token);
            return this.Ok(new MtMessageResult(result));
        }

        /// <summary>
        /// Удалить версию проекта из системы.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpDelete]
        [Route("version/{id:guid}")]
        [SwaggerResponse(StatusCodes.Status200OK, "Модель версии проекта удалена из системы.", typeof(MtMessageResult))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Ошибка в логике приложения, ошибка валидации.", typeof(MtProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Пользователь не авторизован.", typeof(MtProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера.", typeof(MtProblemDetails))]
        public async Task<IActionResult> DeleteVersionModel([FromRoute] Guid id, CancellationToken token = default)
        {
            var command = new Mt.ChangeLog.Logic.Features.ProjectVersion.Delete.Command(new BaseModel() { Id = id });
            var result = await this.mediator.Send(command, token);
            return this.Ok(new MtMessageResult(result));
        }

        #endregion

        #region [ Project revision ]

        /// <summary>
        /// Получить полный перечень кратких модели редакций проектов.
        /// </summary>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpGet]
        [Route("revision/short")]
        [SwaggerResponse(StatusCodes.Status200OK, "Полный перечень кратких моделей редакций проекта.", typeof(IEnumerable<ProjectRevisionShortModel>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Ошибка в логике приложения, ошибка валидации.", typeof(MtProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Пользователь не авторизован.", typeof(MtProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера.", typeof(MtProblemDetails))]
        public async Task<IActionResult> GetRevisionShortModels(CancellationToken token = default)
        {
            var query = new Mt.ChangeLog.Logic.Features.ProjectRevision.GetShorts.Query();
            var result = await this.mediator.Send(query, token);
            return this.Ok(result);
        }

        /// <summary>
        /// Получить полный перечень модели редакций проектов для табличного представления.
        /// </summary>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpGet]
        [Route("revision/table")]
        [SwaggerResponse(StatusCodes.Status200OK, "Полный перечень моделей ревизий проекта для табличного представления.", typeof(IEnumerable<ProjectRevisionTableModel>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Ошибка в логике приложения, ошибка валидации.", typeof(MtProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Пользователь не авторизован.", typeof(MtProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера.", typeof(MtProblemDetails))]
        public async Task<IActionResult> GetRevisionTableModels(CancellationToken token = default)
        {
            var query = new Mt.ChangeLog.Logic.Features.ProjectRevision.GetTables.Query();
            var result = await this.mediator.Send(query, token);
            return this.Ok(result);
        }

        /// <summary>
        /// Получить перечень последних редакций проектов.
        /// </summary>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpGet]
        [Route("revision/last")]
        [SwaggerResponse(StatusCodes.Status200OK, "Перечень модель последних редакции проектов.", typeof(IEnumerable<LastProjectRevisionModel>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Ошибка в логике приложения, ошибка валидации.", typeof(MtProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Пользователь не авторизован.", typeof(MtProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера.", typeof(MtProblemDetails))]
        public async Task<IActionResult> GetLatestRevisionModel(CancellationToken token = default)
        {
            var query = new Mt.ChangeLog.Logic.Features.ProjectRevision.GetLatest.Query();
            var result = await this.mediator.Send(query, token);
            return this.Ok(result);
        }

        /// <summary>
        /// Получить шаблон модели редакции проекта.
        /// </summary>
        /// <param name="id">Идентификатор версии проекта.</param>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpGet]
        [Route("revision/template/{id:guid}")]
        [SwaggerResponse(StatusCodes.Status200OK, "Шаблон полной модели редакции проекта.", typeof(ProjectRevisionModel))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Ошибка в логике приложения, ошибка валидации.", typeof(MtProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Пользователь не авторизован.", typeof(MtProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера.", typeof(MtProblemDetails))]
        public async Task<IActionResult> GetRevisionTemplateModel([FromRoute] Guid id, CancellationToken token = default)
        {
            var query = new Mt.ChangeLog.Logic.Features.ProjectRevision.GetTemplate.Query(new BaseModel() { Id = id });
            var result = await this.mediator.Send(query, token);
            return this.Ok(result);
        }

        /// <summary>
        /// Получить полную модель редакции проекта по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpGet]
        [Route("revision/{id:guid}")]
        [SwaggerResponse(StatusCodes.Status200OK, "Модель редакции проекта.", typeof(ProjectRevisionModel))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Ошибка в логике приложения, ошибка валидации.", typeof(MtProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Пользователь не авторизован.", typeof(MtProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера.", typeof(MtProblemDetails))]
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
        [SwaggerResponse(StatusCodes.Status200OK, "Модель редакции проекта добавлена в систему, ID модели в системе.", typeof(MtMessageResult))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Ошибка в логике приложения, ошибка валидации.", typeof(MtProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Пользователь не авторизован.", typeof(MtProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера.", typeof(MtProblemDetails))]
        public async Task<IActionResult> PostRevisionModel([FromBody] ProjectRevisionModel model, CancellationToken token = default)
        {
            var command = new Mt.ChangeLog.Logic.Features.ProjectRevision.Add.Command(model);
            var result = await this.mediator.Send(command, token);
            return this.Ok(new MtMessageResult(result));
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
        [SwaggerResponse(StatusCodes.Status200OK, "Модель редакции проекта обновлена в системе.", typeof(MtMessageResult))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Ошибка в логике приложения, ошибка валидации.", typeof(MtProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Пользователь не авторизован.", typeof(MtProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера.", typeof(MtProblemDetails))]
        public async Task<IActionResult> PutRevisionModel([FromRoute] Guid id, [FromBody] ProjectRevisionModel model, CancellationToken token = default)
        {
            await this.CheckGuidsAsync(id, model.Id);
            var command = new Mt.ChangeLog.Logic.Features.ProjectRevision.Update.Command(model);
            var result = await this.mediator.Send(command, token);
            return this.Ok(new MtMessageResult(result));
        }

        /// <summary>
        /// Удалить редакцию проекта из системы.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpDelete]
        [Route("revision/{id:guid}")]
        [SwaggerResponse(StatusCodes.Status200OK, "Модель редакции проекта удалена из системы.", typeof(MtMessageResult))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Ошибка в логике приложения, ошибка валидации.", typeof(MtProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Пользователь не авторизован.", typeof(MtProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера.", typeof(MtProblemDetails))]
        public async Task<IActionResult> DeleteRevisionModel([FromRoute] Guid id, CancellationToken token = default)
        {
            var command = new Mt.ChangeLog.Logic.Features.ProjectRevision.Delete.Command(new BaseModel() { Id = id });
            var result = await this.mediator.Send(command, token);
            return this.Ok(new MtMessageResult(result));
        }

        #endregion
    }
}