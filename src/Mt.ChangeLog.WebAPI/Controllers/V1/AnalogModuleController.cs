using MediatR;
using Microsoft.AspNetCore.Mvc;
using Mt.ChangeLog.Logic.Features.AnalogModule;
using Mt.ChangeLog.TransferObjects.AnalogModule;
using Mt.ChangeLog.TransferObjects.Other;
using Mt.Results;
using Swashbuckle.AspNetCore.Annotations;

namespace Mt.ChangeLog.WebAPI.Controllers.V1
{
    /// <summary>
    /// Контроллер для работы с аналоговыми модулями.
    /// </summary>
    [Route("api/analog-module")]
    public sealed class AnalogModuleController : ApiControllerBase
    {
        /// <summary>
        /// Инициализация экземпляра класса <see cref="AnalogModuleController"/>.
        /// </summary>
        /// <param name="mediator">Медиатор.</param>
        public AnalogModuleController(IMediator mediator) : base(mediator)
        {
        }

        /// <summary>
        /// Получить полный перечень кратких модели аналоговых модулей применяемых в блоках БМРЗ.
        /// </summary>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpGet]
        [Route("short")]
        [SwaggerResponse(StatusCodes.Status200OK, "Полный перечень кратких моделей аналогового модуля.", typeof(IEnumerable<AnalogModuleShortModel>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Ошибка в логике приложения, ошибка валидации.", typeof(MtProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Пользователь не авторизован.", typeof(MtProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера.", typeof(MtProblemDetails))]
        public async Task<IActionResult> GetShortModels(CancellationToken token = default)
        {
            var query = new GetShorts.Query();
            var result = await this.mediator.Send(query, token);
            return this.Ok(result);
        }

        /// <summary>
        /// Получить полный перечень моделей аналоговых модулей применяемых в блоках БМРЗ для табличного представления.
        /// </summary>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpGet]
        [Route("table")]
        [SwaggerResponse(StatusCodes.Status200OK, "Полный перечень моделей аналогового модуля для табличного представления.", typeof(IEnumerable<AnalogModuleTableModel>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Ошибка в логике приложения, ошибка валидации.", typeof(MtProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Пользователь не авторизован.", typeof(MtProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера.", typeof(MtProblemDetails))]
        public async Task<IActionResult> GetTableModels(CancellationToken token = default)
        {
            var query = new GetTables.Query();
            var result = await this.mediator.Send(query, token);
            return this.Ok(result);
        }

        /// <summary>
        /// Получить шаблон модели аналогового модуля применяемого в блоках БМРЗ.
        /// </summary>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpGet]
        [Route("template")]
        [SwaggerResponse(StatusCodes.Status200OK, "Шаблон полной модели аналогового модуля.", typeof(AnalogModuleModel))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Ошибка в логике приложения, ошибка валидации.", typeof(MtProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Пользователь не авторизован.", typeof(MtProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера.", typeof(MtProblemDetails))]
        public async Task<IActionResult> GetTemplateModel(CancellationToken token = default)
        {
            var query = new GetTemplate.Query();
            var result = await this.mediator.Send(query, token);
            return this.Ok(result);
        }

        /// <summary>
        /// Получить полную модель аналогового модуля применяемого в блоках БМРЗ по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpGet]
        [Route("{id:guid}")]
        [SwaggerResponse(StatusCodes.Status200OK, "Модель аналогового модуля.", typeof(AnalogModuleModel))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Ошибка в логике приложения, ошибка валидации.", typeof(MtProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Пользователь не авторизован.", typeof(MtProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера.", typeof(MtProblemDetails))]
        public async Task<IActionResult> GetModel([FromRoute] Guid id, CancellationToken token = default)
        {
            var query = new GetById.Query(new BaseModel() { Id = id });
            var result = await this.mediator.Send(query, token);
            return this.Ok(result);
        }

        /// <summary>
        /// Добавить новый аналоговый модуль в систему.
        /// </summary>
        /// <param name="model">Модель.</param>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpPost]
        [SwaggerResponse(StatusCodes.Status200OK, "Модель аналогового модуля добавлена в систему, ID модели в системе.", typeof(MtMessageResult))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Ошибка в логике приложения, ошибка валидации.", typeof(MtProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Пользователь не авторизован.", typeof(MtProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера.", typeof(MtProblemDetails))]
        public async Task<IActionResult> PostModel([FromBody] AnalogModuleModel model, CancellationToken token = default)
        {
            var command = new Add.Command(model);
            var result = await this.mediator.Send(command, token);
            return this.Ok(new MtMessageResult(result));
        }

        /// <summary>
        /// Обновить аналоговый модуль в системе.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <param name="model">Модель.</param>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpPut]
        [Route("{id:guid}")]
        [SwaggerResponse(StatusCodes.Status200OK, "Модель аналогового модуля обновлена в системе.", typeof(MtMessageResult))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Ошибка в логике приложения, ошибка валидации.", typeof(MtProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Пользователь не авторизован.", typeof(MtProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера.", typeof(MtProblemDetails))]
        public async Task<IActionResult> PutModel([FromRoute] Guid id, [FromBody] AnalogModuleModel model, CancellationToken token = default)
        {
            await this.CheckGuidsAsync(id, model.Id);
            var command = new Update.Command(model);
            var result = await this.mediator.Send(command, token);
            return this.Ok(new MtMessageResult(result));
        }

        /// <summary>
        /// Удалить аналоговый модуль из системы.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpDelete]
        [Route("{id:guid}")]
        [SwaggerResponse(StatusCodes.Status200OK, "Модель аналогового модуля удалена из системы.", typeof(MtMessageResult))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Ошибка в логике приложения, ошибка валидации.", typeof(MtProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Пользователь не авторизован.", typeof(MtProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера.", typeof(MtProblemDetails))]
        public async Task<IActionResult> DeleteModel([FromRoute] Guid id, CancellationToken token = default)
        {
            var command = new Delete.Command(new BaseModel() { Id = id });
            var result = await this.mediator.Send(command, token);
            return this.Ok(new MtMessageResult(result));
        }
    }
}