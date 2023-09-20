using MediatR;
using Microsoft.AspNetCore.Mvc;
using Mt.ChangeLog.Logic.Features.Platform;
using Mt.ChangeLog.TransferObjects.Other;
using Mt.ChangeLog.TransferObjects.Platform;
using Swashbuckle.AspNetCore.Annotations;

namespace Mt.ChangeLog.WebAPI.Controllers.V1;

/// <summary>
/// Контроллер для работы с платформами БМРЗ.
/// </summary>
[ApiController]
[Route("api/platform")]
public sealed class PlatformController : ControllerBase
{
    private readonly IMediator mediator;

    /// <summary>
    /// Инициализация экземпляра класса <see cref="PlatformController"/>.
    /// </summary>
    /// <param name="mediator">Медиатор.</param>
    public PlatformController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    /// <summary>
    /// Получить полный перечень кратких моделей платформ БМРЗ.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Результат действия.</returns>
    [HttpGet("short")]
    [SwaggerResponse(StatusCodes.Status200OK, "Полный перечень кратких моделей платформ.", typeof(IEnumerable<PlatformShortModel>))]
    public Task<IEnumerable<PlatformShortModel>> GetShortModels(CancellationToken cancellationToken)
    {
        var query = new GetShorts.Query();
        return this.mediator.Send(query, cancellationToken);
    }

    /// <summary>
    /// Получить полный перечень моделей платформ БМРЗ для табличного представления.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Результат действия.</returns>
    [HttpGet("table")]
    [SwaggerResponse(StatusCodes.Status200OK, "Полный перечень моделей платформ для табличного представления.", typeof(IEnumerable<PlatformTableModel>))]
    public Task<IEnumerable<PlatformTableModel>> GetTableModels(CancellationToken cancellationToken)
    {
        var query = new GetTables.Query();
        return this.mediator.Send(query, cancellationToken);
    }

    /// <summary>
    /// Получить шаблон модели платформы БМРЗ.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Результат действия.</returns>
    [HttpGet("template")]
    [SwaggerResponse(StatusCodes.Status200OK, "Шаблон полной модели платформы.", typeof(PlatformModel))]
    public Task<PlatformModel> GetTemplateModel(CancellationToken cancellationToken)
    {
        var query = new GetTemplate.Query();
        return this.mediator.Send(query, cancellationToken);
    }

    /// <summary>
    /// Получить полную модель платформ БМРЗ по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Результат действия.</returns>
    [HttpGet("{id:guid}")]
    [SwaggerResponse(StatusCodes.Status200OK, "Модель платформы.", typeof(PlatformModel))]
    public Task<PlatformModel> GetModel([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var query = new GetById.Query(new BaseModel { Id = id });
        return this.mediator.Send(query, cancellationToken);
    }

    /// <summary>
    /// Добавить новую платформу БМРЗ в систему.
    /// </summary>
    /// <param name="model">Модель.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Результат действия.</returns>
    [HttpPost]
    [SwaggerResponse(StatusCodes.Status200OK, "Модель платформы добавлена в систему, ID модели в системе.", typeof(MessageModel))]
    public Task<MessageModel> PostModel([FromBody] PlatformModel model, CancellationToken cancellationToken)
    {
        var command = new Add.Command(model);
        return this.mediator.Send(command, cancellationToken);
    }

    /// <summary>
    /// Обновить платформу БМРЗ в системе.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="model">Модель.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Результат действия.</returns>
    [HttpPut("{id:guid}")]
    [SwaggerResponse(StatusCodes.Status200OK, "Модель платформы обновлена в системе.", typeof(MessageModel))]
    public Task<MessageModel> PutModel([FromRoute] Guid id, [FromBody] PlatformModel model, CancellationToken cancellationToken)
    {
        var command = new Update.Command(id, model);
        return this.mediator.Send(command, cancellationToken);
    }

    /// <summary>
    /// Удалить платформу БМРЗ из системы.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Результат действия.</returns>
    [HttpDelete("{id:guid}")]
    [SwaggerResponse(StatusCodes.Status200OK, "Модель платформу удалена из системы.", typeof(MessageModel))]
    public Task<MessageModel> DeleteModel([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var command = new Delete.Command(new BaseModel { Id = id });
        return this.mediator.Send(command, cancellationToken);
    }
}