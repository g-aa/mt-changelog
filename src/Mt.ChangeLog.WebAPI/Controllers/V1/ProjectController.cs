using MediatR;
using Microsoft.AspNetCore.Mvc;
using Mt.ChangeLog.TransferObjects.Other;
using Mt.ChangeLog.TransferObjects.ProjectRevision;
using Mt.ChangeLog.TransferObjects.ProjectStatus;
using Mt.ChangeLog.TransferObjects.ProjectVersion;
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
        public Task<IEnumerable<ProjectStatusShortModel>> GetStatusShortModels(CancellationToken token)
        {
            var query = new Mt.ChangeLog.Logic.Features.ProjectStatus.GetShorts.Query();
            return this.mediator.Send(query, token);
        }

        /// <summary>
        /// Получить полный перечень моделей статусов проекта для табличного представления.
        /// </summary>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpGet]
        [Route("status/table")]
        [SwaggerResponse(StatusCodes.Status200OK, "Полный перечень моделей статусов проекта для табличного представления.", typeof(IEnumerable<ProjectStatusTableModel>))]
        public Task<IEnumerable<ProjectStatusTableModel>> GetStatusTableModels(CancellationToken token)
        {
            var query = new Mt.ChangeLog.Logic.Features.ProjectStatus.GetTables.Query();
            return this.mediator.Send(query, token);
        }

        /// <summary>
        /// Получить шаблон модели статуса проекта.
        /// </summary>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpGet]
        [Route("status/template")]
        [SwaggerResponse(StatusCodes.Status200OK, "Шаблон полной модели статуса проекта.", typeof(ProjectStatusModel))]
        public Task<ProjectStatusModel> GetStatusTemplateModel(CancellationToken token)
        {
            var query = new Mt.ChangeLog.Logic.Features.ProjectStatus.GetTemplate.Query();
            return this.mediator.Send(query, token);
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
        public Task<ProjectStatusModel> GetStatusModel([FromRoute] Guid id, CancellationToken token)
        {
            var query = new Mt.ChangeLog.Logic.Features.ProjectStatus.GetById.Query(new BaseModel() { Id = id });
            return this.mediator.Send(query, token);
        }

        /// <summary>
        /// Добавить новый статус проекта в систему.
        /// </summary>
        /// <param name="model">Модель.</param>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpPost]
        [Route("status")]
        [SwaggerResponse(StatusCodes.Status200OK, "Модель статуса проекта добавлена в систему, ID модели в системе.", typeof(MessageModel))]
        public Task<MessageModel> PostStatusModel([FromBody] ProjectStatusModel model, CancellationToken token)
        {
            var command = new Mt.ChangeLog.Logic.Features.ProjectStatus.Add.Command(model);
            return this.mediator.Send(command, token);
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
        [SwaggerResponse(StatusCodes.Status200OK, "Модель статуса проекта обновлена в системе.", typeof(MessageModel))]
        public Task<MessageModel> PutStatusModel([FromRoute] Guid id, [FromBody] ProjectStatusModel model, CancellationToken token)
        {
            this.CheckGuids(id, model.Id);
            var command = new Mt.ChangeLog.Logic.Features.ProjectStatus.Update.Command(model);
            return this.mediator.Send(command, token);
        }

        /// <summary>
        /// Удалить статус проекта из системы.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpDelete]
        [Route("status/{id:guid}")]
        [SwaggerResponse(StatusCodes.Status200OK, "Модель статуса проекта удалена из системы.", typeof(MessageModel))]
        public Task<MessageModel> DeleteStatusModel([FromRoute] Guid id, CancellationToken token)
        {
            var command = new Mt.ChangeLog.Logic.Features.ProjectStatus.Delete.Command(new BaseModel() { Id = id });
            return this.mediator.Send(command, token);
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
        public Task<IEnumerable<ProjectVersionShortModel>> GetVersionShortModels(CancellationToken token)
        {
            var query = new Mt.ChangeLog.Logic.Features.ProjectVersion.GetShorts.Query();
            return this.mediator.Send(query, token);
        }

        /// <summary>
        /// Получить полный перечень модели версий проектов для табличного представления.
        /// </summary>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpGet]
        [Route("version/table")]
        [SwaggerResponse(StatusCodes.Status200OK, "Полный перечень моделей версий проекта для табличного представления.", typeof(IEnumerable<ProjectVersionTableModel>))]
        public Task<IEnumerable<ProjectVersionTableModel>> GetVersionTableModels(CancellationToken token)
        {
            var query = new Mt.ChangeLog.Logic.Features.ProjectVersion.GetTables.Query();
            return this.mediator.Send(query, token);
        }

        /// <summary>
        /// Получить перечень наименование версий проектов.
        /// </summary>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpGet]
        [Route("version/title")]
        [SwaggerResponse(StatusCodes.Status200OK, "Полный перечень наименование версий проектов.", typeof(IEnumerable<string>))]
        public Task<IEnumerable<string>> GetProjectTitles(CancellationToken token)
        {
            var query = new Mt.ChangeLog.Logic.Features.ProjectVersion.GetTitles.Query();
            return this.mediator.Send(query, token);
        }

        /// <summary>
        /// Получить шаблон модели версии проекта.
        /// </summary>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpGet]
        [Route("version/template")]
        [SwaggerResponse(StatusCodes.Status200OK, "Шаблон полной модели версии проекта.", typeof(ProjectVersionModel))]
        public Task<ProjectVersionModel> GetVersionTemplateModel(CancellationToken token)
        {
            var query = new Mt.ChangeLog.Logic.Features.ProjectVersion.GetTemplate.Query();
            return this.mediator.Send(query, token);
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
        public Task<ProjectVersionModel> GetVersionModel([FromRoute] Guid id, CancellationToken token)
        {
            var query = new Mt.ChangeLog.Logic.Features.ProjectVersion.GetById.Query(new BaseModel() { Id = id });
            return this.mediator.Send(query, token);
        }

        /// <summary>
        /// Добавить новую версию проекта в систему.
        /// </summary>
        /// <param name="model">Модель.</param>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpPost]
        [Route("version")]
        [SwaggerResponse(StatusCodes.Status200OK, "Модель версии проекта добавлена в систему, ID модели в системе.", typeof(MessageModel))]
        public Task<MessageModel> PostVersionModel([FromBody] ProjectVersionModel model, CancellationToken token)
        {
            var command = new Mt.ChangeLog.Logic.Features.ProjectVersion.Add.Command(model);
            return this.mediator.Send(command, token);
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
        [SwaggerResponse(StatusCodes.Status200OK, "Модель версии проекта обновлена в системе.", typeof(MessageModel))]
        public Task<MessageModel> PutVersionModel([FromRoute] Guid id, [FromBody] ProjectVersionModel model, CancellationToken token)
        {
            this.CheckGuids(id, model.Id);
            var command = new Mt.ChangeLog.Logic.Features.ProjectVersion.Update.Command(model);
            return this.mediator.Send(command, token);
        }

        /// <summary>
        /// Удалить версию проекта из системы.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpDelete]
        [Route("version/{id:guid}")]
        [SwaggerResponse(StatusCodes.Status200OK, "Модель версии проекта удалена из системы.", typeof(MessageModel))]
        public Task<MessageModel> DeleteVersionModel([FromRoute] Guid id, CancellationToken token)
        {
            var command = new Mt.ChangeLog.Logic.Features.ProjectVersion.Delete.Command(new BaseModel() { Id = id });
            return this.mediator.Send(command, token);
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
        public Task<IEnumerable<ProjectRevisionShortModel>> GetRevisionShortModels(CancellationToken token)
        {
            var query = new Mt.ChangeLog.Logic.Features.ProjectRevision.GetShorts.Query();
            return this.mediator.Send(query, token);
        }

        /// <summary>
        /// Получить полный перечень модели редакций проектов для табличного представления.
        /// </summary>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpGet]
        [Route("revision/table")]
        [SwaggerResponse(StatusCodes.Status200OK, "Полный перечень моделей ревизий проекта для табличного представления.", typeof(IEnumerable<ProjectRevisionTableModel>))]
        public Task<IEnumerable<ProjectRevisionTableModel>> GetRevisionTableModels(CancellationToken token)
        {
            var query = new Mt.ChangeLog.Logic.Features.ProjectRevision.GetTables.Query();
            return this.mediator.Send(query, token);
        }

        /// <summary>
        /// Получить перечень последних редакций проектов.
        /// </summary>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpGet]
        [Route("revision/last")]
        [SwaggerResponse(StatusCodes.Status200OK, "Перечень модель последних редакции проектов.", typeof(IEnumerable<LastProjectRevisionModel>))]
        public Task<IEnumerable<LastProjectRevisionModel>> GetLatestRevisionModel(CancellationToken token)
        {
            var query = new Mt.ChangeLog.Logic.Features.ProjectRevision.GetLatest.Query();
            return this.mediator.Send(query, token);
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
        public Task<ProjectRevisionModel> GetRevisionTemplateModel([FromRoute] Guid id, CancellationToken token)
        {
            var query = new Mt.ChangeLog.Logic.Features.ProjectRevision.GetTemplate.Query(new BaseModel() { Id = id });
            return this.mediator.Send(query, token);
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
        public Task<ProjectRevisionModel> GetRevisionModel([FromRoute] Guid id, CancellationToken token)
        {
            var query = new Mt.ChangeLog.Logic.Features.ProjectRevision.GetById.Query(new BaseModel() { Id = id });
            return this.mediator.Send(query, token);
        }

        /// <summary>
        /// Добавить новую редакцию проекта в систему.
        /// </summary>
        /// <param name="model">Модель.</param>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpPost]
        [Route("revision")]
        [SwaggerResponse(StatusCodes.Status200OK, "Модель редакции проекта добавлена в систему, ID модели в системе.", typeof(MessageModel))]
        public Task<MessageModel> PostRevisionModel([FromBody] ProjectRevisionModel model, CancellationToken token)
        {
            var command = new Mt.ChangeLog.Logic.Features.ProjectRevision.Add.Command(model);
            return this.mediator.Send(command, token);
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
        [SwaggerResponse(StatusCodes.Status200OK, "Модель редакции проекта обновлена в системе.", typeof(MessageModel))]
        public Task<MessageModel> PutRevisionModel([FromRoute] Guid id, [FromBody] ProjectRevisionModel model, CancellationToken token)
        {
            this.CheckGuids(id, model.Id);
            var command = new Mt.ChangeLog.Logic.Features.ProjectRevision.Update.Command(model);
            return this.mediator.Send(command, token);
        }

        /// <summary>
        /// Удалить редакцию проекта из системы.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpDelete]
        [Route("revision/{id:guid}")]
        [SwaggerResponse(StatusCodes.Status200OK, "Модель редакции проекта удалена из системы.", typeof(MessageModel))]
        public Task<MessageModel> DeleteRevisionModel([FromRoute] Guid id, CancellationToken token)
        {
            var command = new Mt.ChangeLog.Logic.Features.ProjectRevision.Delete.Command(new BaseModel() { Id = id });
            return this.mediator.Send(command, token);
        }

        #endregion
    }
}