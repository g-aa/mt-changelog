using MediatR;
using Microsoft.AspNetCore.Mvc;
using Mt.ChangeLog.Logic.Features.Communication;
using Mt.ChangeLog.TransferObjects.Communication;
using Mt.ChangeLog.TransferObjects.Other;
using Swashbuckle.AspNetCore.Annotations;

namespace Mt.ChangeLog.WebAPI.Controllers.V1;

/// <summary>
/// Контроллер для работы с коммуникационным модулем.
/// </summary>
[ApiController]
[Route("api/communication")]
public sealed class CommunicationController : ControllerBase
{
    private readonly IMediator mediator;

    /// <summary>
    /// Инициализация экземпляра класса <see cref="CommunicationController"/>.
    /// </summary>
    /// <param name="mediator">Медиатор.</param>
    public CommunicationController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    /// <summary>
    /// Получить полный перечень кратких моделей коммуникационных модулей.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Результат действия.</returns>
    [HttpGet("short")]
    [SwaggerResponse(StatusCodes.Status200OK, "Полный перечень кратких моделей коммуникационного модуля.", typeof(IEnumerable<CommunicationShortModel>))]
    public Task<IEnumerable<CommunicationShortModel>> GetShortModels(CancellationToken cancellationToken)
    {
        var query = new GetShorts.Query();
        return this.mediator.Send(query, cancellationToken);
    }

    /// <summary>
    /// Получить полный перечень моделей коммуникационных модулей для табличного представления.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Результат действия.</returns>
    [HttpGet("table")]
    [SwaggerResponse(StatusCodes.Status200OK, "Полный перечень моделей коммуникационного модуля для табличного представления.", typeof(IEnumerable<CommunicationTableModel>))]
    public Task<IEnumerable<CommunicationTableModel>> GetTableModels(CancellationToken cancellationToken)
    {
        var query = new GetTables.Query();
        return this.mediator.Send(query, cancellationToken);
    }

    /// <summary>
    /// Получить шаблон модели коммуникационного модуля.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Результат действия.</returns>
    [HttpGet("template")]
    [SwaggerResponse(StatusCodes.Status200OK, "Шаблон полной модели коммуникационного модуля.", typeof(CommunicationModel))]
    public Task<CommunicationModel> GetTemplateModel(CancellationToken cancellationToken)
    {
        var query = new GetTemplate.Query();
        return this.mediator.Send(query, cancellationToken);
    }

    /// <summary>
    /// Получить полную модель коммуникационного модуля по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Результат действия.</returns>
    [HttpGet("{id:guid}")]
    [SwaggerResponse(StatusCodes.Status200OK, "Модель коммуникационного модуля.", typeof(CommunicationModel))]
    public Task<CommunicationModel> GetModel([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var query = new GetById.Query(new BaseModel { Id = id });
        return this.mediator.Send(query, cancellationToken);
    }

    /// <summary>
    /// Добавить новый коммуникационный модуль в систему.
    /// </summary>
    /// <param name="model">Модель.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Результат действия.</returns>
    [HttpPost]
    [SwaggerResponse(StatusCodes.Status200OK, "Модель коммуникационного модуля добавлена в систему, ID модели в системе.", typeof(MessageModel))]
    public Task<MessageModel> PostModel([FromBody] CommunicationModel model, CancellationToken cancellationToken)
    {
        var command = new Add.Command(model);
        return this.mediator.Send(command, cancellationToken);
    }

    /// <summary>
    /// Обновить коммуникационный модуль в системе.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="model">Модель.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Результат действия.</returns>
    [HttpPut("{id:guid}")]
    [SwaggerResponse(StatusCodes.Status200OK, "Модель коммуникационного модуля обновлена в системе.", typeof(MessageModel))]
    public Task<MessageModel> PutModel([FromRoute] Guid id, [FromBody] CommunicationModel model, CancellationToken cancellationToken)
    {
        var command = new Update.Command(id, model);
        return this.mediator.Send(command, cancellationToken);
    }

    /// <summary>
    /// Удалить коммуникационный модуль из системы.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Результат действия.</returns>
    [HttpDelete("{id:guid}")]
    [SwaggerResponse(StatusCodes.Status200OK, "Модель коммуникационного модуля удалена из системы.", typeof(MessageModel))]
    public Task<MessageModel> DeleteModel([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var command = new Delete.Command(new BaseModel { Id = id });
        return this.mediator.Send(command, cancellationToken);
    }
}