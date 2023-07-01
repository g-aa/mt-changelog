using MediatR;
using Microsoft.AspNetCore.Mvc;
using Mt.ChangeLog.Logic.Features.Protocol;
using Mt.ChangeLog.TransferObjects.Other;
using Mt.ChangeLog.TransferObjects.Protocol;
using Swashbuckle.AspNetCore.Annotations;

namespace Mt.ChangeLog.WebAPI.Controllers.V1
{
    /// <summary>
    /// Контроллер для работы с протоколами.
    /// </summary>
    [Route("api/protocol")]
    public sealed class ProtocolController : ApiControllerBase
    {
        /// <summary>
        /// Инициализация экземпляра класса <see cref="ProtocolController"/>.
        /// </summary>
        /// <param name="mediator">Медиатор.</param>
        public ProtocolController(IMediator mediator) : base(mediator)
        {
        }

        /// <summary>
        /// Получить полный перечень кратких моделей протокола инф. обмена.
        /// </summary>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpGet]
        [Route("short")]
        [SwaggerResponse(StatusCodes.Status200OK, "Полный перечень кратких моделей протоколов.", typeof(IEnumerable<ProtocolShortModel>))]
        public Task<IEnumerable<ProtocolShortModel>> GetShortModels(CancellationToken token)
        {
            var query = new GetShorts.Query();
            return this.mediator.Send(query, token);
        }

        /// <summary>
        /// Получить полный перечень модели протокола инф. обмена для табличного представления.
        /// </summary>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpGet]
        [Route("table")]
        [SwaggerResponse(StatusCodes.Status200OK, "Полный перечень моделей протоколов для табличного представления.", typeof(IEnumerable<ProtocolTableModel>))]
        public Task<IEnumerable<ProtocolTableModel>> GetTableModels(CancellationToken token)
        {
            var query = new GetTables.Query();
            return this.mediator.Send(query, token);
        }

        /// <summary>
        /// Получить шаблон модели протокола инф. обмена.
        /// </summary>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpGet]
        [Route("template")]
        [SwaggerResponse(StatusCodes.Status200OK, "Шаблон полной модели протокола.", typeof(ProtocolModel))]
        public Task<ProtocolModel> GetTemplateModel(CancellationToken token)
        {
            var query = new GetTemplate.Query();
            return this.mediator.Send(query, token);
        }

        /// <summary>
        /// Получить полную модель протокола инф. обмена по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpGet]
        [Route("{id:guid}")]
        [SwaggerResponse(StatusCodes.Status200OK, "Модель протокола.", typeof(ProtocolModel))]
        public Task<ProtocolModel> GetModel([FromRoute] Guid id, CancellationToken token)
        {
            var query = new GetById.Query(new BaseModel() { Id = id });
            return this.mediator.Send(query, token);
        }

        /// <summary>
        /// Добавить новый протокол инф. обмена в систему.
        /// </summary>
        /// <param name="model">Модель.</param>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpPost]
        [SwaggerResponse(StatusCodes.Status200OK, "Модель протокола добавлена в систему, ID модели в системе.", typeof(MessageModel))]
        public Task<MessageModel> PostModel([FromBody] ProtocolModel model, CancellationToken token)
        {
            var command = new Add.Command(model);
            return this.mediator.Send(command, token);
        }

        /// <summary>
        /// Обновить протокол инф. обмена в системе.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <param name="model">Модель.</param>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpPut]
        [Route("{id:guid}")]
        [SwaggerResponse(StatusCodes.Status200OK, "Модель протокола обновлена в системе.", typeof(MessageModel))]
        public Task<MessageModel> PutModel([FromRoute] Guid id, [FromBody] ProtocolModel model, CancellationToken token)
        {
            this.CheckGuids(id, model.Id);
            var command = new Update.Command(model);
            return this.mediator.Send(command, token);
        }

        /// <summary>
        /// Удалить протокол инф. обмена из системы.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpDelete]
        [Route("{id:guid}")]
        [SwaggerResponse(StatusCodes.Status200OK, "Модель протокола удалена из системы.", typeof(MessageModel))]
        public Task<MessageModel> DeleteModel([FromRoute] Guid id, CancellationToken token)
        {
            var command = new Delete.Command(new BaseModel() { Id = id });
            return this.mediator.Send(command, token);
        }
    }
}