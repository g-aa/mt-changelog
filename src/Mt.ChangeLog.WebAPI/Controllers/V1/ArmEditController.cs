using Asp.Versioning;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using Mt.ChangeLog.Logic.Features.ArmEdit;
using Mt.ChangeLog.TransferObjects.ArmEdit;
using Mt.ChangeLog.TransferObjects.Other;

using Swashbuckle.AspNetCore.Annotations;

namespace Mt.ChangeLog.WebAPI.Controllers.V1;

/// <summary>
/// Контроллер для работы с ArmEdit.
/// </summary>
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/arm-edit")]
public sealed class ArmEditController : ControllerBase
{
    private readonly IMediator _mediator;

    /// <summary>
    /// Инициализация экземпляра класса <see cref="ArmEditController"/>.
    /// </summary>
    /// <param name="mediator">Медиатор.</param>
    public ArmEditController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Получить полный перечень краткие модели Arm-Edit.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Результат действия.</returns>
    [HttpGet("list/short")]
    [SwaggerResponse(StatusCodes.Status200OK, "Полный перечень кратких моделей ArmEdit.", typeof(IReadOnlyCollection<ArmEditShortModel>))]
    public Task<IReadOnlyCollection<ArmEditShortModel>> GetShortModels(CancellationToken cancellationToken)
    {
        var query = new GetShorts.Query();
        return _mediator.Send(query, cancellationToken);
    }

    /// <summary>
    /// Получить полный перечень модели Arm-Edit для табличного представления.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Результат действия.</returns>
    [HttpGet("list/table")]
    [SwaggerResponse(StatusCodes.Status200OK, "Полный перечень моделей ArmEdit для табличного представления.", typeof(IReadOnlyCollection<ArmEditTableModel>))]
    public Task<IReadOnlyCollection<ArmEditTableModel>> GetTableModels(CancellationToken cancellationToken)
    {
        var query = new GetTables.Query();
        return _mediator.Send(query, cancellationToken);
    }

    /// <summary>
    /// Получить шаблон модели Arm-Edit.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Результат действия.</returns>
    [HttpGet("template")]
    [SwaggerResponse(StatusCodes.Status200OK, "Шаблон полной модели ArmEdit.", typeof(ArmEditModel))]
    public Task<ArmEditModel> GetTemplateModel(CancellationToken cancellationToken)
    {
        var query = new GetTemplate.Query();
        return _mediator.Send(query, cancellationToken);
    }

    /// <summary>
    /// Получить актуальную версию Arm-Edit.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Результат действия.</returns>
    [HttpGet("actual")]
    [SwaggerResponse(StatusCodes.Status200OK, "Актуальная версия модели ArmEdit.", typeof(ArmEditModel))]
    public Task<ArmEditModel> GetActualModel(CancellationToken cancellationToken)
    {
        var query = new GetActual.Query();
        return _mediator.Send(query, cancellationToken);
    }

    /// <summary>
    /// Получить полную модель Arm-Edit по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Результат действия.</returns>
    [HttpGet("{id:guid}")]
    [SwaggerResponse(StatusCodes.Status200OK, "Модель ArmEdit.", typeof(ArmEditModel))]
    public Task<ArmEditModel> GetModel([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var query = new GetById.Query(new BaseModel { Id = id });
        return _mediator.Send(query, cancellationToken);
    }

    /// <summary>
    /// Добавить новый Arm-Edit в систему.
    /// </summary>
    /// <param name="model">Модель.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Результат действия.</returns>
    [HttpPost]
    [SwaggerResponse(StatusCodes.Status200OK, "Модель ArmEdit добавлена в систему, ID модели в системе.", typeof(MessageModel))]
    public Task<MessageModel> PostModel([FromBody] ArmEditModel model, CancellationToken cancellationToken)
    {
        var command = new Add.Command(model);
        return _mediator.Send(command, cancellationToken);
    }

    /// <summary>
    /// Обновить Arm-Edit в системе.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="model">Модель.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Результат действия.</returns>
    [HttpPut("{id:guid}")]
    [SwaggerResponse(StatusCodes.Status200OK, "Модель ArmEdit обновлена в системе.", typeof(MessageModel))]
    public Task<MessageModel> PutModel([FromRoute] Guid id, [FromBody] ArmEditModel model, CancellationToken cancellationToken)
    {
        var command = new Update.Command(id, model);
        return _mediator.Send(command, cancellationToken);
    }

    /// <summary>
    /// Удалить Arm-Edit из системы.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Результат действия.</returns>
    [HttpDelete("{id:guid}")]
    [SwaggerResponse(StatusCodes.Status200OK, "Модель ArmEdit удалена из системы.", typeof(MessageModel))]
    public Task<MessageModel> DeleteModel([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var command = new Delete.Command(new BaseModel { Id = id });
        return _mediator.Send(command, cancellationToken);
    }
}