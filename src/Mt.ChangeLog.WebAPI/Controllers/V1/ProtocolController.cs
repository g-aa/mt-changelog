using Asp.Versioning;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using Mt.ChangeLog.Logic.Features.Protocol;
using Mt.ChangeLog.TransferObjects.Other;
using Mt.ChangeLog.TransferObjects.Protocol;

using Swashbuckle.AspNetCore.Annotations;

namespace Mt.ChangeLog.WebAPI.Controllers.V1;

/// <summary>
/// Контроллер для работы с протоколами.
/// </summary>
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/protocol")]
public sealed class ProtocolController : ControllerBase
{
    private readonly IMediator _mediator;

    /// <summary>
    /// Инициализация экземпляра класса <see cref="ProtocolController"/>.
    /// </summary>
    /// <param name="mediator">Медиатор.</param>
    public ProtocolController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Получить полный перечень кратких моделей протокола инф. обмена.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Результат действия.</returns>
    [HttpGet("list/short")]
    [SwaggerResponse(StatusCodes.Status200OK, "Полный перечень кратких моделей протоколов.", typeof(IReadOnlyCollection<ProtocolShortModel>))]
    public Task<IReadOnlyCollection<ProtocolShortModel>> GetShortModels(CancellationToken cancellationToken)
    {
        var query = new GetShorts.Query();
        return _mediator.Send(query, cancellationToken);
    }

    /// <summary>
    /// Получить полный перечень модели протокола инф. обмена для табличного представления.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Результат действия.</returns>
    [HttpGet("list/table")]
    [SwaggerResponse(StatusCodes.Status200OK, "Полный перечень моделей протоколов для табличного представления.", typeof(IReadOnlyCollection<ProtocolTableModel>))]
    public Task<IReadOnlyCollection<ProtocolTableModel>> GetTableModels(CancellationToken cancellationToken)
    {
        var query = new GetTables.Query();
        return _mediator.Send(query, cancellationToken);
    }

    /// <summary>
    /// Получить шаблон модели протокола инф. обмена.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Результат действия.</returns>
    [HttpGet("template")]
    [SwaggerResponse(StatusCodes.Status200OK, "Шаблон полной модели протокола.", typeof(ProtocolModel))]
    public Task<ProtocolModel> GetTemplateModel(CancellationToken cancellationToken)
    {
        var query = new GetTemplate.Query();
        return _mediator.Send(query, cancellationToken);
    }

    /// <summary>
    /// Получить полную модель протокола инф. обмена по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Результат действия.</returns>
    [HttpGet("{id:guid}")]
    [SwaggerResponse(StatusCodes.Status200OK, "Модель протокола.", typeof(ProtocolModel))]
    public Task<ProtocolModel> GetModel([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var query = new GetById.Query(new BaseModel { Id = id });
        return _mediator.Send(query, cancellationToken);
    }

    /// <summary>
    /// Добавить новый протокол инф. обмена в систему.
    /// </summary>
    /// <param name="model">Модель.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Результат действия.</returns>
    [HttpPost]
    [SwaggerResponse(StatusCodes.Status200OK, "Модель протокола добавлена в систему, ID модели в системе.", typeof(MessageModel))]
    public Task<MessageModel> PostModel([FromBody] ProtocolModel model, CancellationToken cancellationToken)
    {
        var command = new Add.Command(model);
        return _mediator.Send(command, cancellationToken);
    }

    /// <summary>
    /// Обновить протокол инф. обмена в системе.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="model">Модель.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Результат действия.</returns>
    [HttpPut("{id:guid}")]
    [SwaggerResponse(StatusCodes.Status200OK, "Модель протокола обновлена в системе.", typeof(MessageModel))]
    public Task<MessageModel> PutModel([FromRoute] Guid id, [FromBody] ProtocolModel model, CancellationToken cancellationToken)
    {
        var command = new Update.Command(id, model);
        return _mediator.Send(command, cancellationToken);
    }

    /// <summary>
    /// Удалить протокол инф. обмена из системы.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Результат действия.</returns>
    [HttpDelete("{id:guid}")]
    [SwaggerResponse(StatusCodes.Status200OK, "Модель протокола удалена из системы.", typeof(MessageModel))]
    public Task<MessageModel> DeleteModel([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var command = new Delete.Command(new BaseModel { Id = id });
        return _mediator.Send(command, cancellationToken);
    }
}