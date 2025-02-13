using Asp.Versioning;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using Mt.ChangeLog.Logic.Features.AnalogModule;
using Mt.ChangeLog.TransferObjects.AnalogModule;
using Mt.ChangeLog.TransferObjects.Other;

using Swashbuckle.AspNetCore.Annotations;

namespace Mt.ChangeLog.WebAPI.Controllers.V1;

/// <summary>
/// Контроллер для работы с аналоговыми модулями.
/// </summary>
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/analog-module")]
public sealed class AnalogModuleController : ControllerBase
{
    private readonly IMediator _mediator;

    /// <summary>
    /// Инициализация экземпляра класса <see cref="AnalogModuleController"/>.
    /// </summary>
    /// <param name="mediator">Медиатор.</param>
    public AnalogModuleController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Получить полный перечень кратких модели аналоговых модулей применяемых в блоках БМРЗ.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Результат действия.</returns>
    [HttpGet("list/short")]
    [SwaggerResponse(StatusCodes.Status200OK, "Полный перечень кратких моделей аналогового модуля.", typeof(IReadOnlyCollection<AnalogModuleShortModel>))]
    public Task<IReadOnlyCollection<AnalogModuleShortModel>> GetShortModels(CancellationToken cancellationToken)
    {
        var query = new GetShorts.Query();
        return _mediator.Send(query, cancellationToken);
    }

    /// <summary>
    /// Получить полный перечень моделей аналоговых модулей применяемых в блоках БМРЗ для табличного представления.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Результат действия.</returns>
    [HttpGet("list/table")]
    [SwaggerResponse(StatusCodes.Status200OK, "Полный перечень моделей аналогового модуля для табличного представления.", typeof(IReadOnlyCollection<AnalogModuleTableModel>))]
    public Task<IReadOnlyCollection<AnalogModuleTableModel>> GetTableModels(CancellationToken cancellationToken)
    {
        var query = new GetTables.Query();
        return _mediator.Send(query, cancellationToken);
    }

    /// <summary>
    /// Получить шаблон модели аналогового модуля применяемого в блоках БМРЗ.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Результат действия.</returns>
    [HttpGet("template")]
    [SwaggerResponse(StatusCodes.Status200OK, "Шаблон полной модели аналогового модуля.", typeof(AnalogModuleModel))]
    public Task<AnalogModuleModel> GetTemplateModel(CancellationToken cancellationToken)
    {
        var query = new GetTemplate.Query();
        return _mediator.Send(query, cancellationToken);
    }

    /// <summary>
    /// Получить полную модель аналогового модуля применяемого в блоках БМРЗ по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Результат действия.</returns>
    [HttpGet("{id:guid}")]
    [SwaggerResponse(StatusCodes.Status200OK, "Модель аналогового модуля.", typeof(AnalogModuleModel))]
    public Task<AnalogModuleModel> GetModel([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var query = new GetById.Query(new BaseModel { Id = id });
        return _mediator.Send(query, cancellationToken);
    }

    /// <summary>
    /// Добавить новый аналоговый модуль в систему.
    /// </summary>
    /// <param name="model">Модель.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Результат действия.</returns>
    [HttpPost]
    [SwaggerResponse(StatusCodes.Status200OK, "Модель аналогового модуля добавлена в систему, ID модели в системе.", typeof(MessageModel))]
    public Task<MessageModel> PostModel([FromBody] AnalogModuleModel model, CancellationToken cancellationToken)
    {
        var command = new Add.Command(model);
        return _mediator.Send(command, cancellationToken);
    }

    /// <summary>
    /// Обновить аналоговый модуль в системе.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="model">Модель.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Результат действия.</returns>
    [HttpPut("{id:guid}")]
    [SwaggerResponse(StatusCodes.Status200OK, "Модель аналогового модуля обновлена в системе.", typeof(MessageModel))]
    public Task<MessageModel> PutModel([FromRoute] Guid id, [FromBody] AnalogModuleModel model, CancellationToken cancellationToken)
    {
        var command = new Update.Command(id, model);
        return _mediator.Send(command, cancellationToken);
    }

    /// <summary>
    /// Удалить аналоговый модуль из системы.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Результат действия.</returns>
    [HttpDelete("{id:guid}")]
    [SwaggerResponse(StatusCodes.Status200OK, "Модель аналогового модуля удалена из системы.", typeof(MessageModel))]
    public Task<MessageModel> DeleteModel([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var command = new Delete.Command(new BaseModel { Id = id });
        return _mediator.Send(command, cancellationToken);
    }
}