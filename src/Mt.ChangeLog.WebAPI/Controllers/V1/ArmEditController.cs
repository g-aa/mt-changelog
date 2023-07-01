using MediatR;
using Microsoft.AspNetCore.Mvc;
using Mt.ChangeLog.Logic.Features.ArmEdit;
using Mt.ChangeLog.TransferObjects.ArmEdit;
using Mt.ChangeLog.TransferObjects.Other;
using Swashbuckle.AspNetCore.Annotations;

namespace Mt.ChangeLog.WebAPI.Controllers.V1
{
    /// <summary>
    /// Контроллер для работы с ArmEdit.
    /// </summary>
    [Route("api/arm-edit")]
    public sealed class ArmEditController : ApiControllerBase
    {
        /// <summary>
        /// Инициализация экземпляра класса <see cref="ArmEditController"/>.
        /// </summary>
        /// <param name="mediator">Медиатор.</param>
        public ArmEditController(IMediator mediator) : base(mediator)
        {
        }

        /// <summary>
        /// Получить полный перечень краткие модели Arm-Edit.
        /// </summary>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpGet]
        [Route("short")]
        [SwaggerResponse(StatusCodes.Status200OK, "Полный перечень кратких моделей ArmEdit.", typeof(IEnumerable<ArmEditShortModel>))]
        public Task<IEnumerable<ArmEditShortModel>> GetShortModels(CancellationToken token)
        {
            var query = new GetShorts.Query();
            return this.mediator.Send(query, token);
        }

        /// <summary>
        /// Получить полный перечень модели Arm-Edit для табличного представления.
        /// </summary>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpGet]
        [Route("table")]
        [SwaggerResponse(StatusCodes.Status200OK, "Полный перечень моделей ArmEdit для табличного представления.", typeof(IEnumerable<ArmEditTableModel>))]
        public Task<IEnumerable<ArmEditTableModel>> GetTableModels(CancellationToken token)
        {
            var query = new GetTables.Query();
            return this.mediator.Send(query, token);
        }

        /// <summary>
        /// Получить шаблон модели Arm-Edit.
        /// </summary>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpGet]
        [Route("template")]
        [SwaggerResponse(StatusCodes.Status200OK, "Шаблон полной модели ArmEdit.", typeof(ArmEditModel))]
        public Task<ArmEditModel> GetTemplateModel(CancellationToken token)
        {
            var query = new GetTemplate.Query();
            return this.mediator.Send(query, token);
        }

        /// <summary>
        /// Получить актуальную версию Arm-Edit.
        /// </summary>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpGet]
        [Route("actual")]
        [SwaggerResponse(StatusCodes.Status200OK, "Актуальная версия модели ArmEdit.", typeof(ArmEditModel))]
        public Task<ArmEditModel> GetActualModel(CancellationToken token)
        {
            var query = new GetActual.Query();
            return this.mediator.Send(query, token);
        }

        /// <summary>
        /// Получить полную модель Arm-Edit по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpGet]
        [Route("{id:guid}")]
        [SwaggerResponse(StatusCodes.Status200OK, "Модель ArmEdit.", typeof(ArmEditModel))]
        public Task<ArmEditModel> GetModel([FromRoute] Guid id, CancellationToken token)
        {
            var query = new GetById.Query(new BaseModel() { Id = id });
            return this.mediator.Send(query, token);
        }

        /// <summary>
        /// Добавить новый Arm-Edit в систему.
        /// </summary>
        /// <param name="model">Модель.</param>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpPost]
        [SwaggerResponse(StatusCodes.Status200OK, "Модель ArmEdit добавлена в систему, ID модели в системе.", typeof(MessageModel))]
        public Task<MessageModel> PostModel([FromBody] ArmEditModel model, CancellationToken token)
        {
            var command = new Add.Command(model);
            return this.mediator.Send(command, token);
        }

        /// <summary>
        /// Обновить Arm-Edit в системе.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <param name="model">Модель.</param>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpPut]
        [Route("{id:guid}")]
        [SwaggerResponse(StatusCodes.Status200OK, "Модель ArmEdit обновлена в системе.", typeof(MessageModel))]
        public Task<MessageModel> PutModel([FromRoute] Guid id, [FromBody] ArmEditModel model, CancellationToken token)
        {
            this.CheckGuids(id, model.Id);
            var command = new Update.Command(model);
            return this.mediator.Send(command, token);
        }

        /// <summary>
        /// Удалить Arm-Edit из системы.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpDelete]
        [Route("{id:guid}")]
        [SwaggerResponse(StatusCodes.Status200OK, "Модель ArmEdit удалена из системы.", typeof(MessageModel))]
        public Task<MessageModel> DeleteModel([FromRoute] Guid id, CancellationToken token)
        {
            var command = new Delete.Command(new BaseModel() { Id = id });
            return this.mediator.Send(command, token);
        }
    }
}