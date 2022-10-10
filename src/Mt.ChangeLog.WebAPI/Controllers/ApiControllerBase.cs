using MediatR;
using Microsoft.AspNetCore.Mvc;
using Mt.Utilities;
using Mt.Utilities.Exceptions;
using System;
using System.Threading.Tasks;

namespace Mt.ChangeLog.WebAPI.Controllers
{
    /// <summary>
    /// Базовый API-контроллер.
    /// </summary>
    [ApiController]
    [Produces("application/json")]
    public abstract class ApiControllerBase : ControllerBase
    {
        /// <summary>
        /// Медиатор.
        /// </summary>
        protected readonly IMediator mediator;

        /// <summary>
        /// Инициализация экземпляра класса <see cref="ApiControllerBase"/>.
        /// </summary>
        /// <param name="mediator">Медиатор.</param>
        protected ApiControllerBase(IMediator mediator)
        {
            this.mediator = Check.NotNull(mediator, nameof(mediator));
        }

        /// <summary>
        /// Метод проверки идентификаторов (uuid) из URL и модели в теле запроса на равенство.
        /// </summary>
        /// <param name="queryId">Идентификаторов полученый из URL.</param>
        /// <param name="bodyId">Идентификаторов полученый из модели в теле запроса.</param>
        /// <exception cref="ArgumentException">Срабатывает если uuids не равны между собой.</exception>
        protected async Task CheckGuidsAsync(Guid queryId, Guid bodyId)
        {
            if (!queryId.Equals(bodyId))
            {
                await Task.FromException(new MtException(ErrorCode.EntityValidation, $"Идентификатор из URL: '{queryId}' не равен идентификатору в модели из тела запроса: '{bodyId}'."));
            }
        }
    }
}