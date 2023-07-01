using MediatR;
using Microsoft.AspNetCore.Mvc;
using Mt.ChangeLog.Logic.Features.History;
using Mt.ChangeLog.TransferObjects.Historical;
using Mt.ChangeLog.TransferObjects.Other;
using Swashbuckle.AspNetCore.Annotations;

namespace Mt.ChangeLog.WebAPI.Controllers.V1
{
    /// <summary>
    /// Контроллер для работы с историями изменений.
    /// </summary>
    [Route("api/history")]
    public class HistoryController : ApiControllerBase
    {
        /// <summary>
        /// Инициализация экземпляра класса <see cref="HistoryController"/>.
        /// </summary>
        /// <param name="mediator">Медиатор.</param>
        public HistoryController(IMediator mediator) : base(mediator)
        {
        }

        /// <summary>
        /// Получить статистику по имеющимся данным в системе.
        /// </summary>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpGet]
        [Route("statistics")]
        [SwaggerResponse(StatusCodes.Status200OK, "Получить статистику по имеющимся данным в системе.", typeof(StatisticsModel))]
        public Task<StatisticsModel> GetStatisticsModel(CancellationToken token)
        {
            var query = new GetStatistics.Query();
            return this.mediator.Send(query, token);
        }

        /// <summary>
        /// Получить историю изменения для версии проета.
        /// </summary>
        /// <param name="id">Идентификатор версии проекта.</param>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpGet]
        [Route("version/{id:guid}")]
        [SwaggerResponse(StatusCodes.Status200OK, "История изменения версии проекта.", typeof(ProjectVersionHistoryModel))]
        public Task<ProjectVersionHistoryModel> GetVersionHistoryModel([FromRoute] Guid id, CancellationToken token)
        {
            var query = new GetProjectVersionHistory.Query(new BaseModel() { Id = id });
            return this.mediator.Send(query, token);
        }

        /// <summary>
        /// Получить историю изменения для редакции проета.
        /// </summary>
        /// <param name="id">Идентификатор редакции проекта.</param>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpGet]
        [Route("revision/{id:guid}")]
        [SwaggerResponse(StatusCodes.Status200OK, "История изменения редакции проекта.", typeof(ProjectRevisionHistoryModel))]
        public Task<ProjectRevisionHistoryModel> GetRevisionHistoryModel([FromRoute] Guid id, CancellationToken token)
        {
            var query = new GetProjectRevisionHistory.Query(new BaseModel() { Id = id });
            return this.mediator.Send(query, token);
        }

        /// <summary>
        /// Получить дерево изменений проекта.
        /// </summary>
        /// <param name="title">Наименование версии проекта.</param>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpGet]
        [Route("tree/{title:length(2, 16)}")]
        [SwaggerResponse(StatusCodes.Status200OK, "Перечень моделий для дерева изменений.", typeof(IEnumerable<ProjectRevisionTreeModel>))]
        public Task<IEnumerable<ProjectRevisionTreeModel>> GetTreeModel([FromRoute] string title, CancellationToken token)
        {
            var query = new GetProjectTree.Query(title);
            return this.mediator.Send(query, token);
        }
    }
}