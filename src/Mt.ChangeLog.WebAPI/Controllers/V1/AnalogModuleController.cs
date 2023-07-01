using MediatR;
using Microsoft.AspNetCore.Mvc;
using Mt.ChangeLog.Logic.Features.AnalogModule;
using Mt.ChangeLog.TransferObjects.AnalogModule;
using Mt.ChangeLog.TransferObjects.Other;
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
        public Task<IEnumerable<AnalogModuleShortModel>> GetShortModels(CancellationToken token)
        {
            var query = new GetShorts.Query();
            return this.mediator.Send(query, token);
        }

        /// <summary>
        /// Получить полный перечень моделей аналоговых модулей применяемых в блоках БМРЗ для табличного представления.
        /// </summary>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpGet]
        [Route("table")]
        [SwaggerResponse(StatusCodes.Status200OK, "Полный перечень моделей аналогового модуля для табличного представления.", typeof(IEnumerable<AnalogModuleTableModel>))]
        public Task<IEnumerable<AnalogModuleTableModel>> GetTableModels(CancellationToken token)
        {
            var query = new GetTables.Query();
            return this.mediator.Send(query, token);
        }

        /// <summary>
        /// Получить шаблон модели аналогового модуля применяемого в блоках БМРЗ.
        /// </summary>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpGet]
        [Route("template")]
        [SwaggerResponse(StatusCodes.Status200OK, "Шаблон полной модели аналогового модуля.", typeof(AnalogModuleModel))]
        public Task<AnalogModuleModel> GetTemplateModel(CancellationToken token)
        {
            var query = new GetTemplate.Query();
            return this.mediator.Send(query, token);
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
        public Task<AnalogModuleModel> GetModel([FromRoute] Guid id, CancellationToken token)
        {
            var query = new GetById.Query(new BaseModel() { Id = id });
            return this.mediator.Send(query, token);
        }

        /// <summary>
        /// Добавить новый аналоговый модуль в систему.
        /// </summary>
        /// <param name="model">Модель.</param>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpPost]
        [SwaggerResponse(StatusCodes.Status200OK, "Модель аналогового модуля добавлена в систему, ID модели в системе.", typeof(MessageModel))]
        public Task<MessageModel> PostModel([FromBody] AnalogModuleModel model, CancellationToken token)
        {
            var command = new Add.Command(model);
            return this.mediator.Send(command, token);
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
        [SwaggerResponse(StatusCodes.Status200OK, "Модель аналогового модуля обновлена в системе.", typeof(MessageModel))]
        public Task<MessageModel> PutModel([FromRoute] Guid id, [FromBody] AnalogModuleModel model, CancellationToken token)
        {
            this.CheckGuids(id, model.Id);
            var command = new Update.Command(model);
            return this.mediator.Send(command, token);
        }

        /// <summary>
        /// Удалить аналоговый модуль из системы.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpDelete]
        [Route("{id:guid}")]
        [SwaggerResponse(StatusCodes.Status200OK, "Модель аналогового модуля удалена из системы.", typeof(MessageModel))]
        public Task<MessageModel> DeleteModel([FromRoute] Guid id, CancellationToken token)
        {
            var command = new Delete.Command(new BaseModel() { Id = id });
            return this.mediator.Send(command, token);
        }
    }
}