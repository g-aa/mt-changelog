using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mt.ChangeLog.Logic.Features.Author;
using Mt.ChangeLog.TransferObjects.Author;
using Mt.ChangeLog.TransferObjects.Other;
using Mt.ChangeLog.WebAPI.Infrastracture;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Mt.ChangeLog.WebAPI.Controllers.V1
{
    /// <summary>
    /// Контроллер для работы с авторами.
    /// </summary>
    [Route("api/author")]
    public sealed class AuthorController : ApiControllerBase
    {
        /// <summary>
        /// Инициализация экземпляра класса <see cref="AuthorController"/>.
        /// </summary>
        /// <param name="mediator">Медиатор.</param>
        public AuthorController(IMediator mediator) : base(mediator)
        {
        }

        /// <summary>
        /// Получить полный перечень кратких моделей авторов проектов.
        /// </summary>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpGet]
        [Route("short")]
        [SwaggerResponse(StatusCodes.Status200OK, "Полный перечень кратких моделей авторов.", typeof(IEnumerable<AuthorShortModel>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Ошибка в логике приложения, ошибка валидации.", typeof(ApiProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Пользователь не авторизован.", typeof(ApiProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера.", typeof(ApiProblemDetails))]
        public async Task<IActionResult> GetShortModels(CancellationToken token = default)
        {
            var query = new GetShorts.Query();
            var result = await this.mediator.Send(query, token);
            return this.Ok(result);
        }

        /// <summary>
        /// Получить полный перечень моделей авторов проектов для табличного представления.
        /// </summary>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpGet]
        [Route("table")]
        [SwaggerResponse(StatusCodes.Status200OK, "Полный перечень моделей авторов для табличного представления.", typeof(IEnumerable<AuthorTableModel>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Ошибка в логике приложения, ошибка валидации.", typeof(ApiProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Пользователь не авторизован.", typeof(ApiProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера.", typeof(ApiProblemDetails))]
        public async Task<IActionResult> GetTableModels(CancellationToken token = default)
        {
            var query = new GetTables.Query();
            var result = await this.mediator.Send(query, token);
            return this.Ok(result);
        }

        /// <summary>
        /// Получить полный перечень моделей автор-общий вклад в разработку.
        /// </summary>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpGet]
        [Route("contribution")]
        [SwaggerResponse(StatusCodes.Status200OK, "Полный перечень моделей авторов для табличного представления.", typeof(IEnumerable<AuthorContributionModel>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Ошибка в логике приложения, ошибка валидации.", typeof(ApiProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Пользователь не авторизован.", typeof(ApiProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера.", typeof(ApiProblemDetails))]
        public async Task<IActionResult> GetContributionModels(CancellationToken token = default)
        {
            var query = new GetContributions.Query();
            var result = await this.mediator.Send(query, token);
            return this.Ok(result);
        }

        /// <summary>
        /// Получить полный перечень моделей автор-вклад по проектам.
        /// </summary>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpGet]
        [Route("contribution/project")]
        [SwaggerResponse(StatusCodes.Status200OK, "Полный перечень моделей авторов для табличного представления.", typeof(IEnumerable<AuthorProjectContributionModel>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Ошибка в логике приложения, ошибка валидации.", typeof(ApiProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Пользователь не авторизован.", typeof(ApiProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера.", typeof(ApiProblemDetails))]
        public async Task<IActionResult> GetProjectContributionModels(CancellationToken token = default)
        {
            var query = new GetProjectContributions.Query();
            var result = await this.mediator.Send(query, token);
            return this.Ok(result);
        }

        /// <summary>
        /// Получить шаблон модели автора проекта.
        /// </summary>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpGet]
        [Route("template")]
        [SwaggerResponse(StatusCodes.Status200OK, "Шаблон полной модели автора.", typeof(AuthorModel))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Ошибка в логике приложения, ошибка валидации.", typeof(ApiProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Пользователь не авторизован.", typeof(ApiProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера.", typeof(ApiProblemDetails))]
        public async Task<IActionResult> GetTemplateModel(CancellationToken token = default)
        {
            var query = new GetTemplate.Query();
            var result = await this.mediator.Send(query, token);
            return this.Ok(result);
        }

        /// <summary>
        /// Получить полную модель автора проекта по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpGet]
        [Route("{id:guid}")]
        [SwaggerResponse(StatusCodes.Status200OK, "Модель автора.", typeof(AuthorModel))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Ошибка в логике приложения, ошибка валидации.", typeof(ApiProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Пользователь не авторизован.", typeof(ApiProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера.", typeof(ApiProblemDetails))]
        public async Task<IActionResult> GetModel([FromRoute] Guid id, CancellationToken token = default)
        {
            var query = new GetById.Query(new BaseModel() { Id = id });
            var result = await this.mediator.Send(query, token);
            return this.Ok(result);
        }

        /// <summary>
        /// Добавить нового автора проектов в систему.
        /// </summary>
        /// <param name="model">Модель.</param>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpPost]
        [SwaggerResponse(StatusCodes.Status200OK, "Модель автора добавлена в систему, ID модели в системе.", typeof(BaseModel))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Ошибка в логике приложения, ошибка валидации.", typeof(ApiProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Пользователь не авторизован.", typeof(ApiProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера.", typeof(ApiProblemDetails))]
        public async Task<IActionResult> PostModel([FromBody] AuthorModel model, CancellationToken token = default)
        {
            var command = new Add.Command(model);
            var baseModel = await this.mediator.Send(command, token);
            return this.Ok(baseModel);
        }

        /// <summary>
        /// Обновить автора проектов в системе.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <param name="model">Модель.</param>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpPut]
        [Route("{id:guid}")]
        [SwaggerResponse(StatusCodes.Status200OK, "Модель автора обновлена в системе.", typeof(StatusModel))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Ошибка в логике приложения, ошибка валидации.", typeof(ApiProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Пользователь не авторизован.", typeof(ApiProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера.", typeof(ApiProblemDetails))]
        public async Task<IActionResult> PutModel([FromRoute] Guid id, [FromBody] AuthorModel model, CancellationToken token = default)
        {
            this.CheckGuids(id, model.Id);
            var command = new Update.Command(model);
            var result = await this.mediator.Send(command, token);
            return this.Ok(result);
        }

        /// <summary>
        /// Удалить автора проектов из системы.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpDelete]
        [Route("{id:guid}")]
        [SwaggerResponse(StatusCodes.Status200OK, "Модель автора удалена из системы.", typeof(StatusModel))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Ошибка в логике приложения, ошибка валидации.", typeof(ApiProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Пользователь не авторизован.", typeof(ApiProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера.", typeof(ApiProblemDetails))]
        public async Task<IActionResult> DeleteModel([FromRoute] Guid id, CancellationToken token = default)
        {
            var command = new Delete.Command(new BaseModel() { Id = id });
            var result = await this.mediator.Send(command, token);
            return this.Ok(result);
        }
    }
}