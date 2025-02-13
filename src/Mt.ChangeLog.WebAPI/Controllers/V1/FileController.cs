using Asp.Versioning;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using Mt.ChangeLog.Logic.Features.File;
using Mt.ChangeLog.Logic.Features.History;
using Mt.ChangeLog.TransferObjects.Historical;
using Mt.ChangeLog.TransferObjects.Other;

using Swashbuckle.AspNetCore.Annotations;

namespace Mt.ChangeLog.WebAPI.Controllers.V1;

/// <summary>
/// Контроллер для работы с файлами.
/// </summary>
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/file")]
public sealed class FileController : ControllerBase
{
    private readonly IMediator _mediator;

    /// <summary>
    /// Инициализация экземпляра класса <see cref="FileController"/>.
    /// </summary>
    /// <param name="mediator">Медиатор.</param>
    public FileController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Получить полную историю изменения версии проекта.
    /// </summary>
    /// <param name="id">Идентификатор версии проекта.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Результат действия.</returns>
    [HttpGet("changelog/{id:guid}")]
    [SwaggerResponse(StatusCodes.Status200OK, "Полная история изменения версии проекта.", typeof(ProjectVersionHistoryModel))]
    public Task<ProjectVersionHistoryModel> GetProjectVersionChangeLog([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var query = new GetProjectVersionHistory.Query(new BaseModel { Id = id });
        return _mediator.Send(query, cancellationToken);
    }

    /// <summary>
    /// Получить полный архив с перечнем логов изменений из системы.
    /// </summary>
    /// <param name="token">Токен отмены.</param>
    /// <returns>Результат действия.</returns>
    [HttpGet("changelog/archive/full")]
    [SwaggerResponse(StatusCodes.Status200OK, "Полный архив логов изменения проектов.", typeof(FileModel))]
    public Task<FileModel> GetChangeLogArchive(CancellationToken token)
    {
        var query = new GetFullArchive.Query();
        return _mediator.Send(query, token);
    }
}