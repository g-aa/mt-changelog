using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mt.ChangeLog.Logic.Features.Tree;
using Mt.ChangeLog.TransferObjects.Historical;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Mt.ChangeLog.WebAPI.Controllers.V1
{
    /// <summary>
    /// Контроллер для работы с деревьями изменений.
    /// </summary>
    [Route("api/tree")]
    public sealed class TreeController : ApiControllerBase
    {
        /// <summary>
        /// Инициализация экземпляра класса <see cref="TreeController"/>.
        /// </summary>
        /// <param name="mediator">Медиатор.</param>
        public TreeController(IMediator mediator) : base(mediator)
        {
        }

        /// <summary>
        /// Получить перечень наименование версий проектов.
        /// </summary>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpGet]
        [Route("title")]
        [SwaggerResponse(StatusCodes.Status200OK, "полный перечень наименование версий проектов.", typeof(IEnumerable<string>))]
        public async Task<IActionResult> GetProjectTitles(CancellationToken token = default)
        {
            var query = new GetProjectTitles.Query();
            var result = await this.mediator.Send(query, token);
            return this.Ok(result);
        }

        /// <summary>
        /// Получить дерево изменений проекта.
        /// </summary>
        /// <param name="title">Наименование версии проекта.</param>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Результат действия.</returns>
        [HttpGet]
        [Route("{title:string}")]
        [SwaggerResponse(StatusCodes.Status200OK, "Перечень моделий для дерева изменений.", typeof(IEnumerable<ProjectRevisionTreeModel>))]
        public async Task<IActionResult> GetProjectTreeModel([FromRoute] string title, CancellationToken token = default)
        {
            var query = new GetProjectTree.Query(title);
            var result = await this.mediator.Send(query, token);
            return this.Ok(result);
        }
    }
}