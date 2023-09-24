using MediatR;
using Microsoft.AspNetCore.Mvc;
using Mt.ChangeLog.Logic.Features.Author;
using Mt.ChangeLog.TransferObjects.Author;
using Mt.ChangeLog.TransferObjects.Other;
using Swashbuckle.AspNetCore.Annotations;

namespace Mt.ChangeLog.WebAPI.Controllers.V1;

/// <summary>
/// Контроллер для работы с авторами.
/// </summary>
[ApiController]
[Route("api/author")]
public sealed class AuthorController : ControllerBase
{
    private readonly IMediator mediator;

    /// <summary>
    /// Инициализация экземпляра класса <see cref="AuthorController"/>.
    /// </summary>
    /// <param name="mediator">Медиатор.</param>
    public AuthorController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    /// <summary>
    /// Получить полный перечень кратких моделей авторов проектов.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Результат действия.</returns>
    [HttpGet("list/short")]
    [SwaggerResponse(StatusCodes.Status200OK, "Полный перечень кратких моделей авторов.", typeof(IEnumerable<AuthorShortModel>))]
    public Task<IEnumerable<AuthorShortModel>> GetShortModels(CancellationToken cancellationToken)
    {
        var query = new GetShorts.Query();
        return this.mediator.Send(query, cancellationToken);
    }

    /// <summary>
    /// Получить полный перечень моделей авторов проектов для табличного представления.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Результат действия.</returns>
    [HttpGet("list/table")]
    [SwaggerResponse(StatusCodes.Status200OK, "Полный перечень моделей авторов для табличного представления.", typeof(IEnumerable<AuthorTableModel>))]
    public Task<IEnumerable<AuthorTableModel>> GetTableModels(CancellationToken cancellationToken)
    {
        var query = new GetTables.Query();
        return this.mediator.Send(query, cancellationToken);
    }

    /// <summary>
    /// Получить полный перечень моделей автор-общий вклад в разработку.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Результат действия.</returns>
    [HttpGet("list/contribution")]
    [SwaggerResponse(StatusCodes.Status200OK, "Полный перечень моделей авторов для табличного представления.", typeof(IEnumerable<AuthorContributionModel>))]
    public Task<IEnumerable<AuthorContributionModel>> GetContributionModels(CancellationToken cancellationToken)
    {
        var query = new GetContributions.Query();
        return this.mediator.Send(query, cancellationToken);
    }

    /// <summary>
    /// Получить полный перечень моделей автор-вклад по проектам.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Результат действия.</returns>
    [HttpGet("list/contribution/project")]
    [SwaggerResponse(StatusCodes.Status200OK, "Полный перечень моделей авторов для табличного представления.", typeof(IEnumerable<AuthorProjectContributionModel>))]
    public Task<IEnumerable<AuthorProjectContributionModel>> GetProjectContributionModels(CancellationToken cancellationToken)
    {
        var query = new GetProjectContributions.Query();
        return this.mediator.Send(query, cancellationToken);
    }

    /// <summary>
    /// Получить шаблон модели автора проекта.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Результат действия.</returns>
    [HttpGet("template")]
    [SwaggerResponse(StatusCodes.Status200OK, "Шаблон полной модели автора.", typeof(AuthorModel))]
    public Task<AuthorModel> GetTemplateModel(CancellationToken cancellationToken)
    {
        var query = new GetTemplate.Query();
        return this.mediator.Send(query, cancellationToken);
    }

    /// <summary>
    /// Получить полную модель автора проекта по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Результат действия.</returns>
    [HttpGet("{id:guid}")]
    [SwaggerResponse(StatusCodes.Status200OK, "Модель автора.", typeof(AuthorModel))]
    public Task<AuthorModel> GetModel([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var query = new GetById.Query(new BaseModel { Id = id });
        return this.mediator.Send(query, cancellationToken);
    }

    /// <summary>
    /// Добавить нового автора проектов в систему.
    /// </summary>
    /// <param name="model">Модель.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Результат действия.</returns>
    [HttpPost]
    [SwaggerResponse(StatusCodes.Status200OK, "Модель автора добавлена в систему, ID модели в системе.", typeof(MessageModel))]
    public Task<MessageModel> PostModel([FromBody] AuthorModel model, CancellationToken cancellationToken)
    {
        var command = new Add.Command(model);
        return this.mediator.Send(command, cancellationToken);
    }

    /// <summary>
    /// Обновить автора проектов в системе.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="model">Модель.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Результат действия.</returns>
    [HttpPut("{id:guid}")]
    [SwaggerResponse(StatusCodes.Status200OK, "Модель автора обновлена в системе.", typeof(MessageModel))]
    public Task<MessageModel> PutModel([FromRoute] Guid id, [FromBody] AuthorModel model, CancellationToken cancellationToken)
    {
        var command = new Update.Command(id, model);
        return this.mediator.Send(command, cancellationToken);
    }

    /// <summary>
    /// Удалить автора проектов из системы.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Результат действия.</returns>
    [HttpDelete("{id:guid}")]
    [SwaggerResponse(StatusCodes.Status200OK, "Модель автора удалена из системы.", typeof(MessageModel))]
    public Task<MessageModel> DeleteModel([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var command = new Delete.Command(new BaseModel { Id = id });
        return this.mediator.Send(command, cancellationToken);
    }
}