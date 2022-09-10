using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;

namespace Mt.ChangeLog.WebAPI.Controllers
{
    /// <summary>
    /// Методы работы с квитанциями.
    /// </summary>
    [ApiController]
    [Route("api/about")]
    [Produces("application/json")]
    public class AboutController : ControllerBase
    {
        /// <summary>
        /// Получить версию приложения.
        /// </summary>
        /// <returns>Версия приложения.</returns>
        [HttpGet("version")]
        [SwaggerResponse(StatusCodes.Status200OK, "Версия приложения.", typeof(string))]
        public async Task<IActionResult> Version()
        {
            var result = await Task.Run(() => this.Ok(Program.AppName));
            return result;
        }
    }
}