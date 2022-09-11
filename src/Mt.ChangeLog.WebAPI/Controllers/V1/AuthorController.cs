using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mt.ChangeLog.Logic.Features.Author;
using Mt.ChangeLog.TransferObjects.Author;
using Mt.ChangeLog.TransferObjects.Other;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Mt.ChangeLog.WebAPI.Controllers.V1
{
    /// <summary>
    /// Контроллер для работы с авторами.
    /// </summary>
    [ApiController]
    [Route("api/author")]
    [Produces("application/json")]
    public sealed class AuthorController : ControllerBase
    {
        /// <summary>
        /// Медиатор.
        /// </summary>
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
        /// Получить все краткие модели <see cref="AuthorShortModel"/>.
        /// </summary>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpGet]
        [Route("short")]
        [SwaggerResponse(StatusCodes.Status200OK, "Полный перечень кратких моделей авторов.", typeof(IEquatable<AuthorShortModel>))]
        public async Task<IActionResult> GetShortModels(CancellationToken token = default)
        {
            var query = new GetShorts.Query();
            var result = await this.mediator.Send(query, token);
            return this.Ok(result);
        }

        /// <summary>
        /// Получить все модели <see cref="AuthorTableModel"/> представления для таблиц.
        /// </summary>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpGet]
        [Route("table")]
        [SwaggerResponse(StatusCodes.Status200OK, "Полный перечень моделей авторов для табличного представления.", typeof(IEquatable<AuthorTableModel>))]
        public async Task<IActionResult> GetTableModels(CancellationToken token = default)
        {
            var query = new GetTables.Query();
            var result = await this.mediator.Send(query, token);
            return this.Ok(result);
        }

        /// <summary>
        /// Получить все модели <see cref="AuthorContributionModel"/>.
        /// </summary>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpGet]
        [Route("contribution")]
        [SwaggerResponse(StatusCodes.Status200OK, "Полный перечень моделей авторов для табличного представления.", typeof(IEquatable<AuthorContributionModel>))]
        public async Task<IActionResult> GetContributionModels(CancellationToken token = default)
        {
            var query = new GetContributions.Query();
            var result = await this.mediator.Send(query, token);
            return this.Ok(result);
        }

        /// <summary>
        /// Получить все модели <see cref="AuthorProjectContributionModel"/>.
        /// </summary>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpGet]
        [Route("contribution/project")]
        [SwaggerResponse(StatusCodes.Status200OK, "Полный перечень моделей авторов для табличного представления.", typeof(IEquatable<AuthorProjectContributionModel>))]
        public async Task<IActionResult> GetProjectContributionModels(CancellationToken token = default)
        {
            var query = new GetProjectContributions.Query();
            var result = await this.mediator.Send(query, token);
            return this.Ok(result);
        }

        /// <summary>
        /// Получить шаблон модели <see cref="AuthorModel"/>.
        /// </summary>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpGet]
        [Route("template")]
        [SwaggerResponse(StatusCodes.Status200OK, "Шаблон полной модели автора.", typeof(AuthorModel))]
        public async Task<IActionResult> GetTemplateModel(CancellationToken token = default)
        {
            var query = new GetTemplate.Query();
            var result = await this.mediator.Send(query, token);
            return this.Ok(result);
        }

        /// <summary>
        /// Получить полную модель <see cref="AuthorModel"/> по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpGet]
        [Route("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, "Модель автора.", typeof(AuthorModel))]
        public async Task<IActionResult> GetModel([FromQuery] Guid id, CancellationToken token = default)
        {
            var query = new GetById.Query(new BaseModel() { Id = id });
            var result = await this.mediator.Send(query, token);
            return this.Ok(result);
        }

        /// <summary>
        /// Добавить нового автора в систему.
        /// </summary>
        /// <param name="model">Модель.</param>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpPost]
        [SwaggerResponse(StatusCodes.Status200OK, "Модель автора добавлена в систему, ID модели в системе.", typeof(BaseModel))]
        public async Task<IActionResult> PostModel([FromBody] AuthorModel model, CancellationToken token = default)
        {
            var command = new Add.Command(model);
            var baseModel = await this.mediator.Send(command, token);
            return this.Ok(baseModel);
        }

        /// <summary>
        /// Обновить автора в системе.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <param name="model">Модель.</param>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpPut]
        [Route("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, "Модель автора обновлена в системе.", typeof(StatusModel))]
        public async Task<IActionResult> PutModel([FromQuery] Guid id, [FromBody] AuthorModel model, CancellationToken token = default)
        {
            if (id != model.Id)
            {
                throw new ArgumentException($"url id = {id} is not equal to entity id = {model.Id}");
            }
            var command = new Update.Command(model);
            var result = await this.mediator.Send(command, token);
            return this.Ok(result);
        }

        /// <summary>
        /// Удалить автора из системы.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpDelete]
        [Route("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, "Модель автора удалена из системы.", typeof(StatusModel))]
        public async Task<IActionResult> DeleteModel([FromQuery] Guid id, CancellationToken token = default)
        {
            var command = new Delete.Command(new BaseModel() { Id = id });
            var result = await this.mediator.Send(command, token);
            return this.Ok(result);
        }
    }
}