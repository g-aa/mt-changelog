using MediatR;
using Microsoft.AspNetCore.Mvc;
using Mt.ChangeLog.TransferObjects.Other;
using Mt.ChangeLog.TransferObjects.ProjectRevision;
using Mt.ChangeLog.TransferObjects.ProjectStatus;
using Mt.ChangeLog.TransferObjects.ProjectVersion;
using Swashbuckle.AspNetCore.Annotations;

namespace Mt.ChangeLog.WebAPI.Controllers.V1;

/// <summary>
/// Контроллер для работы с проектами.
/// </summary>
[ApiController]
[Route("api/project")]
public sealed class ProjectController : ControllerBase
{
    private readonly IMediator mediator;

    /// <summary>
    /// Инициализация экземпляра класса <see cref="ProjectController"/>.
    /// </summary>
    /// <param name="mediator">Медиатор.</param>
    public ProjectController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    #region [ Project status ]

    /// <summary>
    /// Получить полный перечень кратких моделей статусов проекта.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Результат действия.</returns>
    [HttpGet("status/short")]
    [SwaggerResponse(StatusCodes.Status200OK, "Полный перечень кратких моделей статусов проекта.", typeof(IEnumerable<ProjectStatusShortModel>))]
    public Task<IEnumerable<ProjectStatusShortModel>> GetStatusShortModels(CancellationToken cancellationToken)
    {
        var query = new Mt.ChangeLog.Logic.Features.ProjectStatus.GetShorts.Query();
        return this.mediator.Send(query, cancellationToken);
    }

    /// <summary>
    /// Получить полный перечень моделей статусов проекта для табличного представления.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Результат действия.</returns>
    [HttpGet("status/table")]
    [SwaggerResponse(StatusCodes.Status200OK, "Полный перечень моделей статусов проекта для табличного представления.", typeof(IEnumerable<ProjectStatusTableModel>))]
    public Task<IEnumerable<ProjectStatusTableModel>> GetStatusTableModels(CancellationToken cancellationToken)
    {
        var query = new Mt.ChangeLog.Logic.Features.ProjectStatus.GetTables.Query();
        return this.mediator.Send(query, cancellationToken);
    }

    /// <summary>
    /// Получить шаблон модели статуса проекта.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Результат действия.</returns>
    [HttpGet("status/template")]
    [SwaggerResponse(StatusCodes.Status200OK, "Шаблон полной модели статуса проекта.", typeof(ProjectStatusModel))]
    public Task<ProjectStatusModel> GetStatusTemplateModel(CancellationToken cancellationToken)
    {
        var query = new Mt.ChangeLog.Logic.Features.ProjectStatus.GetTemplate.Query();
        return this.mediator.Send(query, cancellationToken);
    }

    /// <summary>
    /// Получить полную модель статуса проекта по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Результат действия.</returns>
    [HttpGet("status/{id:guid}")]
    [SwaggerResponse(StatusCodes.Status200OK, "Модель статуса проекта.", typeof(ProjectStatusModel))]
    public Task<ProjectStatusModel> GetStatusModel([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var query = new Mt.ChangeLog.Logic.Features.ProjectStatus.GetById.Query(new BaseModel { Id = id });
        return this.mediator.Send(query, cancellationToken);
    }

    /// <summary>
    /// Добавить новый статус проекта в систему.
    /// </summary>
    /// <param name="model">Модель.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Результат действия.</returns>
    [HttpPost("status")]
    [SwaggerResponse(StatusCodes.Status200OK, "Модель статуса проекта добавлена в систему, ID модели в системе.", typeof(MessageModel))]
    public Task<MessageModel> PostStatusModel([FromBody] ProjectStatusModel model, CancellationToken cancellationToken)
    {
        var command = new Mt.ChangeLog.Logic.Features.ProjectStatus.Add.Command(model);
        return this.mediator.Send(command, cancellationToken);
    }

    /// <summary>
    /// Обновить статус проекта в системе.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="model">Модель.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Результат действия.</returns>
    [HttpPut("status/{id:guid}")]
    [SwaggerResponse(StatusCodes.Status200OK, "Модель статуса проекта обновлена в системе.", typeof(MessageModel))]
    public Task<MessageModel> PutStatusModel([FromRoute] Guid id, [FromBody] ProjectStatusModel model, CancellationToken cancellationToken)
    {
        var command = new Mt.ChangeLog.Logic.Features.ProjectStatus.Update.Command(id, model);
        return this.mediator.Send(command, cancellationToken);
    }

    /// <summary>
    /// Удалить статус проекта из системы.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Результат действия.</returns>
    [HttpDelete("status/{id:guid}")]
    [SwaggerResponse(StatusCodes.Status200OK, "Модель статуса проекта удалена из системы.", typeof(MessageModel))]
    public Task<MessageModel> DeleteStatusModel([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var command = new Mt.ChangeLog.Logic.Features.ProjectStatus.Delete.Command(new BaseModel { Id = id });
        return this.mediator.Send(command, cancellationToken);
    }

    #endregion

    #region [ Project version ]

    /// <summary>
    /// Получить полный перечень кратких моделей версий проектов.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Результат действия.</returns>
    [HttpGet("version/short")]
    [SwaggerResponse(StatusCodes.Status200OK, "Полный перечень кратких моделей версий проекта.", typeof(IEnumerable<ProjectVersionShortModel>))]
    public Task<IEnumerable<ProjectVersionShortModel>> GetVersionShortModels(CancellationToken cancellationToken)
    {
        var query = new Mt.ChangeLog.Logic.Features.ProjectVersion.GetShorts.Query();
        return this.mediator.Send(query, cancellationToken);
    }

    /// <summary>
    /// Получить полный перечень модели версий проектов для табличного представления.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Результат действия.</returns>
    [HttpGet("version/table")]
    [SwaggerResponse(StatusCodes.Status200OK, "Полный перечень моделей версий проекта для табличного представления.", typeof(IEnumerable<ProjectVersionTableModel>))]
    public Task<IEnumerable<ProjectVersionTableModel>> GetVersionTableModels(CancellationToken cancellationToken)
    {
        var query = new Mt.ChangeLog.Logic.Features.ProjectVersion.GetTables.Query();
        return this.mediator.Send(query, cancellationToken);
    }

    /// <summary>
    /// Получить перечень наименование версий проектов.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Результат действия.</returns>
    [HttpGet("version/title")]
    [SwaggerResponse(StatusCodes.Status200OK, "Полный перечень наименование версий проектов.", typeof(IEnumerable<string>))]
    public Task<IEnumerable<string>> GetProjectTitles(CancellationToken cancellationToken)
    {
        var query = new Mt.ChangeLog.Logic.Features.ProjectVersion.GetTitles.Query();
        return this.mediator.Send(query, cancellationToken);
    }

    /// <summary>
    /// Получить шаблон модели версии проекта.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Результат действия.</returns>
    [HttpGet("version/template")]
    [SwaggerResponse(StatusCodes.Status200OK, "Шаблон полной модели версии проекта.", typeof(ProjectVersionModel))]
    public Task<ProjectVersionModel> GetVersionTemplateModel(CancellationToken cancellationToken)
    {
        var query = new Mt.ChangeLog.Logic.Features.ProjectVersion.GetTemplate.Query();
        return this.mediator.Send(query, cancellationToken);
    }

    /// <summary>
    /// Получить полную модель версии проекта по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Результат действия.</returns>
    [HttpGet("version/{id:guid}")]
    [SwaggerResponse(StatusCodes.Status200OK, "Модель версии проекта.", typeof(ProjectVersionModel))]
    public Task<ProjectVersionModel> GetVersionModel([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var query = new Mt.ChangeLog.Logic.Features.ProjectVersion.GetById.Query(new BaseModel { Id = id });
        return this.mediator.Send(query, cancellationToken);
    }

    /// <summary>
    /// Добавить новую версию проекта в систему.
    /// </summary>
    /// <param name="model">Модель.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Результат действия.</returns>
    [HttpPost("version")]
    [SwaggerResponse(StatusCodes.Status200OK, "Модель версии проекта добавлена в систему, ID модели в системе.", typeof(MessageModel))]
    public Task<MessageModel> PostVersionModel([FromBody] ProjectVersionModel model, CancellationToken cancellationToken)
    {
        var command = new Mt.ChangeLog.Logic.Features.ProjectVersion.Add.Command(model);
        return this.mediator.Send(command, cancellationToken);
    }

    /// <summary>
    /// Обновить версии проекта в системе.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="model">Модель.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Результат действия.</returns>
    [HttpPut("version/{id:guid}")]
    [SwaggerResponse(StatusCodes.Status200OK, "Модель версии проекта обновлена в системе.", typeof(MessageModel))]
    public Task<MessageModel> PutVersionModel([FromRoute] Guid id, [FromBody] ProjectVersionModel model, CancellationToken cancellationToken)
    {
        var command = new Mt.ChangeLog.Logic.Features.ProjectVersion.Update.Command(id, model);
        return this.mediator.Send(command, cancellationToken);
    }

    /// <summary>
    /// Удалить версию проекта из системы.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Результат действия.</returns>
    [HttpDelete("version/{id:guid}")]
    [SwaggerResponse(StatusCodes.Status200OK, "Модель версии проекта удалена из системы.", typeof(MessageModel))]
    public Task<MessageModel> DeleteVersionModel([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var command = new Mt.ChangeLog.Logic.Features.ProjectVersion.Delete.Command(new BaseModel { Id = id });
        return this.mediator.Send(command, cancellationToken);
    }

    #endregion

    #region [ Project revision ]

    /// <summary>
    /// Получить полный перечень кратких модели редакций проектов.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Результат действия.</returns>
    [HttpGet("revision/short")]
    [SwaggerResponse(StatusCodes.Status200OK, "Полный перечень кратких моделей редакций проекта.", typeof(IEnumerable<ProjectRevisionShortModel>))]
    public Task<IEnumerable<ProjectRevisionShortModel>> GetRevisionShortModels(CancellationToken cancellationToken)
    {
        var query = new Mt.ChangeLog.Logic.Features.ProjectRevision.GetShorts.Query();
        return this.mediator.Send(query, cancellationToken);
    }

    /// <summary>
    /// Получить полный перечень модели редакций проектов для табличного представления.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Результат действия.</returns>
    [HttpGet("revision/table")]
    [SwaggerResponse(StatusCodes.Status200OK, "Полный перечень моделей ревизий проекта для табличного представления.", typeof(IEnumerable<ProjectRevisionTableModel>))]
    public Task<IEnumerable<ProjectRevisionTableModel>> GetRevisionTableModels(CancellationToken cancellationToken)
    {
        var query = new Mt.ChangeLog.Logic.Features.ProjectRevision.GetTables.Query();
        return this.mediator.Send(query, cancellationToken);
    }

    /// <summary>
    /// Получить перечень последних редакций проектов.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Результат действия.</returns>
    [HttpGet("revision/last")]
    [SwaggerResponse(StatusCodes.Status200OK, "Перечень модель последних редакции проектов.", typeof(IEnumerable<LastProjectRevisionModel>))]
    public Task<IEnumerable<LastProjectRevisionModel>> GetLatestRevisionModel(CancellationToken cancellationToken)
    {
        var query = new Mt.ChangeLog.Logic.Features.ProjectRevision.GetLatest.Query();
        return this.mediator.Send(query, cancellationToken);
    }

    /// <summary>
    /// Получить шаблон модели редакции проекта.
    /// </summary>
    /// <param name="id">Идентификатор версии проекта.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Результат действия.</returns>
    [HttpGet("revision/template/{id:guid}")]
    [SwaggerResponse(StatusCodes.Status200OK, "Шаблон полной модели редакции проекта.", typeof(ProjectRevisionModel))]
    public Task<ProjectRevisionModel> GetRevisionTemplateModel([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var query = new Mt.ChangeLog.Logic.Features.ProjectRevision.GetTemplate.Query(new BaseModel { Id = id });
        return this.mediator.Send(query, cancellationToken);
    }

    /// <summary>
    /// Получить полную модель редакции проекта по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Результат действия.</returns>
    [HttpGet("revision/{id:guid}")]
    [SwaggerResponse(StatusCodes.Status200OK, "Модель редакции проекта.", typeof(ProjectRevisionModel))]
    public Task<ProjectRevisionModel> GetRevisionModel([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var query = new Mt.ChangeLog.Logic.Features.ProjectRevision.GetById.Query(new BaseModel { Id = id });
        return this.mediator.Send(query, cancellationToken);
    }

    /// <summary>
    /// Добавить новую редакцию проекта в систему.
    /// </summary>
    /// <param name="model">Модель.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Результат действия.</returns>
    [HttpPost("revision")]
    [SwaggerResponse(StatusCodes.Status200OK, "Модель редакции проекта добавлена в систему, ID модели в системе.", typeof(MessageModel))]
    public Task<MessageModel> PostRevisionModel([FromBody] ProjectRevisionModel model, CancellationToken cancellationToken)
    {
        var command = new Mt.ChangeLog.Logic.Features.ProjectRevision.Add.Command(model);
        return this.mediator.Send(command, cancellationToken);
    }

    /// <summary>
    /// Обновить редакции проекта в системе.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="model">Модель.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Результат действия.</returns>
    [HttpPut("revision/{id:guid}")]
    [SwaggerResponse(StatusCodes.Status200OK, "Модель редакции проекта обновлена в системе.", typeof(MessageModel))]
    public Task<MessageModel> PutRevisionModel([FromRoute] Guid id, [FromBody] ProjectRevisionModel model, CancellationToken cancellationToken)
    {
        var command = new Mt.ChangeLog.Logic.Features.ProjectRevision.Update.Command(id, model);
        return this.mediator.Send(command, cancellationToken);
    }

    /// <summary>
    /// Удалить редакцию проекта из системы.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Результат действия.</returns>
    [HttpDelete("revision/{id:guid}")]
    [SwaggerResponse(StatusCodes.Status200OK, "Модель редакции проекта удалена из системы.", typeof(MessageModel))]
    public Task<MessageModel> DeleteRevisionModel([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var command = new Mt.ChangeLog.Logic.Features.ProjectRevision.Delete.Command(new BaseModel { Id = id });
        return this.mediator.Send(command, cancellationToken);
    }

    #endregion
}