using MediatR;
using Microsoft.AspNetCore.Mvc;
using Mt.ChangeLog.Logic.Features.Communication;
using Mt.ChangeLog.TransferObjects.Communication;
using Mt.ChangeLog.TransferObjects.Other;
using Swashbuckle.AspNetCore.Annotations;

namespace Mt.ChangeLog.WebAPI.Controllers.V1
{
    /// <summary>
    /// Контроллер для работы с коммуникационным модулем.
    /// </summary>
    [Route("api/communication")]
    public sealed class CommunicationController : ApiControllerBase
    {
        /// <summary>
        /// Инициализация экземпляра класса <see cref="CommunicationController"/>.
        /// </summary>
        /// <param name="mediator">Медиатор.</param>
        public CommunicationController(IMediator mediator) : base(mediator)
        {
        }

        /// <summary>
        /// Получить полный перечень кратких моделей коммуникационных модулей.
        /// </summary>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpGet]
        [Route("short")]
        [SwaggerResponse(StatusCodes.Status200OK, "Полный перечень кратких моделей коммуникационного модуля.", typeof(IEnumerable<CommunicationShortModel>))]
        public Task<IEnumerable<CommunicationShortModel>> GetShortModels(CancellationToken token)
        {
            var query = new GetShorts.Query();
            return this.mediator.Send(query, token);
        }

        /// <summary>
        /// Получить полный перечень моделей коммуникационных модулей для табличного представления.
        /// </summary>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpGet]
        [Route("table")]
        [SwaggerResponse(StatusCodes.Status200OK, "Полный перечень моделей коммуникационного модуля для табличного представления.", typeof(IEnumerable<CommunicationTableModel>))]
        public Task<IEnumerable<CommunicationTableModel>> GetTableModels(CancellationToken token)
        {
            var query = new GetTables.Query();
            return this.mediator.Send(query, token);
        }

        /// <summary>
        /// Получить шаблон модели коммуникационного модуля.
        /// </summary>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpGet]
        [Route("template")]
        [SwaggerResponse(StatusCodes.Status200OK, "Шаблон полной модели коммуникационного модуля.", typeof(CommunicationModel))]
        public Task<CommunicationModel> GetTemplateModel(CancellationToken token)
        {
            var query = new GetTemplate.Query();
            return this.mediator.Send(query, token);
        }

        /// <summary>
        /// Получить полную модель коммуникационного модуля по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpGet]
        [Route("{id:guid}")]
        [SwaggerResponse(StatusCodes.Status200OK, "Модель коммуникационного модуля.", typeof(CommunicationModel))]
        public Task<CommunicationModel> GetModel([FromRoute] Guid id, CancellationToken token)
        {
            var query = new GetById.Query(new BaseModel() { Id = id });
            return this.mediator.Send(query, token);
        }

        /// <summary>
        /// Добавить новый коммуникационный модуль в систему.
        /// </summary>
        /// <param name="model">Модель.</param>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpPost]
        [SwaggerResponse(StatusCodes.Status200OK, "Модель коммуникационного модуля добавлена в систему, ID модели в системе.", typeof(MessageModel))]
        public Task<MessageModel> PostModel([FromBody] CommunicationModel model, CancellationToken token)
        {
            var command = new Add.Command(model);
            return this.mediator.Send(command, token);
        }

        /// <summary>
        /// Обновить коммуникационный модуль в системе.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <param name="model">Модель.</param>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpPut]
        [Route("{id:guid}")]
        [SwaggerResponse(StatusCodes.Status200OK, "Модель коммуникационного модуля обновлена в системе.", typeof(MessageModel))]
        public Task<MessageModel> PutModel([FromRoute] Guid id, [FromBody] CommunicationModel model, CancellationToken token)
        {
            this.CheckGuids(id, model.Id);
            var command = new Update.Command(model);
            return this.mediator.Send(command, token);
        }

        /// <summary>
        /// Удалить коммуникационный модуль из системы.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpDelete]
        [Route("{id:guid}")]
        [SwaggerResponse(StatusCodes.Status200OK, "Модель коммуникационного модуля удалена из системы.", typeof(MessageModel))]
        public Task<MessageModel> DeleteModel([FromRoute] Guid id, CancellationToken token)
        {
            var command = new Delete.Command(new BaseModel() { Id = id });
            return this.mediator.Send(command, token);
        }
    }
}