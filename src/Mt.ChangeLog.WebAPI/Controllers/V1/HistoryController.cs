using MediatR;
using Microsoft.AspNetCore.Mvc;
using Mt.ChangeLog.Logic.Features.History;
using Mt.ChangeLog.TransferObjects.Historical;
using Mt.ChangeLog.TransferObjects.Other;
using Swashbuckle.AspNetCore.Annotations;

namespace Mt.ChangeLog.WebAPI.Controllers.V1;

/// <summary>
/// Контроллер для работы с историями изменений.
/// </summary>
[Route("api/history")]
public sealed class HistoryController : ControllerBase
{
    private readonly IMediator _mediator;

    /// <summary>
    /// Инициализация экземпляра класса <see cref="HistoryController"/>.
    /// </summary>
    /// <param name="mediator">Медиатор.</param>
    public HistoryController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Получить статистику по имеющимся данным в системе.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Результат действия.</returns>
    [HttpGet("statistics")]
    [SwaggerResponse(StatusCodes.Status200OK, "Получить статистику по имеющимся данным в системе.", typeof(StatisticsModel))]
    public Task<StatisticsModel> GetStatisticsModel(CancellationToken cancellationToken)
    {
        var query = new GetStatistics.Query();
        return _mediator.Send(query, cancellationToken);
    }

    /// <summary>
    /// Получить историю изменения для версии проекта.
    /// </summary>
    /// <param name="id">Идентификатор версии проекта.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Результат действия.</returns>
    [HttpGet("version/{id:guid}")]
    [SwaggerResponse(StatusCodes.Status200OK, "История изменения версии проекта.", typeof(ProjectVersionHistoryModel))]
    public Task<ProjectVersionHistoryModel> GetVersionHistoryModel([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var query = new GetProjectVersionHistory.Query(new BaseModel { Id = id });
        return _mediator.Send(query, cancellationToken);
    }

    /// <summary>
    /// Получить историю изменения для редакции проекта.
    /// </summary>
    /// <param name="id">Идентификатор редакции проекта.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Результат действия.</returns>
    [HttpGet("revision/{id:guid}")]
    [SwaggerResponse(StatusCodes.Status200OK, "История изменения редакции проекта.", typeof(ProjectRevisionHistoryModel))]
    public Task<ProjectRevisionHistoryModel> GetRevisionHistoryModel([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var query = new GetProjectRevisionHistory.Query(new BaseModel { Id = id });
        return _mediator.Send(query, cancellationToken);
    }

    /// <summary>
    /// Получить дерево изменений проекта.
    /// </summary>
    /// <param name="title">Наименование версии проекта.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Результат действия.</returns>
    [HttpGet("tree/{title:length(2, 16)}")]
    [SwaggerResponse(StatusCodes.Status200OK, "Перечень моделей для дерева изменений.", typeof(IReadOnlyCollection<ProjectRevisionTreeModel>))]
    public Task<IReadOnlyCollection<ProjectRevisionTreeModel>> GetTreeModel([FromRoute] string title, CancellationToken cancellationToken)
    {
        var query = new GetProjectTree.Query(title);
        return _mediator.Send(query, cancellationToken);
    }
}