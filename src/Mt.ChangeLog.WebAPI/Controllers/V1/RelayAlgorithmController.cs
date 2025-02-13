using Asp.Versioning;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using Mt.ChangeLog.Logic.Features.RelayAlgorithm;
using Mt.ChangeLog.TransferObjects.Other;
using Mt.ChangeLog.TransferObjects.RelayAlgorithm;

using Swashbuckle.AspNetCore.Annotations;

namespace Mt.ChangeLog.WebAPI.Controllers.V1;

/// <summary>
/// Контроллер для работы с алгоритмами.
/// </summary>
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/relay-algorithm")]
public sealed class RelayAlgorithmController : ControllerBase
{
    private readonly IMediator _mediator;

    /// <summary>
    /// Инициализация экземпляра класса <see cref="RelayAlgorithmController"/>.
    /// </summary>
    /// <param name="mediator">Медиатор.</param>
    public RelayAlgorithmController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Получить полный перечень кратких моделей алгоритмов РЗиА.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Результат действия.</returns>
    [HttpGet("list/short")]
    [SwaggerResponse(StatusCodes.Status200OK, "Полный перечень кратких моделей алгоритмов.", typeof(IReadOnlyCollection<RelayAlgorithmShortModel>))]
    public Task<IReadOnlyCollection<RelayAlgorithmShortModel>> GetShortModels(CancellationToken cancellationToken)
    {
        var query = new GetShorts.Query();
        return _mediator.Send(query, cancellationToken);
    }

    /// <summary>
    /// Получить полный перечень моделей алгоритмов РЗиА для табличного представления.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Результат действия.</returns>
    [HttpGet("list/table")]
    [SwaggerResponse(StatusCodes.Status200OK, "Полный перечень моделей алгоритмов для табличного представления.", typeof(IReadOnlyCollection<RelayAlgorithmTableModel>))]
    public Task<IReadOnlyCollection<RelayAlgorithmTableModel>> GetTableModels(CancellationToken cancellationToken)
    {
        var query = new GetTables.Query();
        return _mediator.Send(query, cancellationToken);
    }

    /// <summary>
    /// Получить шаблон модели алгоритма РЗиА.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Результат действия.</returns>
    [HttpGet("template")]
    [SwaggerResponse(StatusCodes.Status200OK, "Шаблон полной модели алгоритма.", typeof(RelayAlgorithmModel))]
    public Task<RelayAlgorithmModel> GetTemplateModel(CancellationToken cancellationToken)
    {
        var query = new GetTemplate.Query();
        return _mediator.Send(query, cancellationToken);
    }

    /// <summary>
    /// Получить полную модель алгоритма РЗиА по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Результат действия.</returns>
    [HttpGet("{id:guid}")]
    [SwaggerResponse(StatusCodes.Status200OK, "Модель алгоритма.", typeof(RelayAlgorithmModel))]
    public Task<RelayAlgorithmModel> GetModel([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var query = new GetById.Query(new BaseModel { Id = id });
        return _mediator.Send(query, cancellationToken);
    }

    /// <summary>
    /// Добавить новый алгоритм РЗиА в систему.
    /// </summary>
    /// <param name="model">Модель.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Результат действия.</returns>
    [HttpPost]
    [SwaggerResponse(StatusCodes.Status200OK, "Модель алгоритма добавлена в систему, ID модели в системе.", typeof(MessageModel))]
    public Task<MessageModel> PostModel([FromBody] RelayAlgorithmModel model, CancellationToken cancellationToken)
    {
        var command = new Add.Command(model);
        return _mediator.Send(command, cancellationToken);
    }

    /// <summary>
    /// Обновить алгоритм РЗиА в системе.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="model">Модель.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Результат действия.</returns>
    [HttpPut("{id:guid}")]
    [SwaggerResponse(StatusCodes.Status200OK, "Модель алгоритма обновлена в системе.", typeof(MessageModel))]
    public Task<MessageModel> PutModel([FromRoute] Guid id, [FromBody] RelayAlgorithmModel model, CancellationToken cancellationToken)
    {
        var command = new Update.Command(id, model);
        return _mediator.Send(command, cancellationToken);
    }

    /// <summary>
    /// Удалить алгоритм РЗиА из системы.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Результат действия.</returns>
    [HttpDelete("{id:guid}")]
    [SwaggerResponse(StatusCodes.Status200OK, "Модель алгоритма удалена из системы.", typeof(MessageModel))]
    public Task<MessageModel> DeleteModel([FromQuery] Guid id, CancellationToken cancellationToken)
    {
        var command = new Delete.Command(new BaseModel { Id = id });
        return _mediator.Send(command, cancellationToken);
    }
}