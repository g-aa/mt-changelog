using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;

namespace Mt.ChangeLog.WebAPI.Controllers
{
    /// <summary>
    /// ������ ������ � �����������.
    /// </summary>
    [ApiController]
    [Route("api/about")]
    [Produces("application/json")]
    public class AboutController : ControllerBase
    {
        /// <summary>
        /// �������� ������ ����������.
        /// </summary>
        /// <returns>������ ����������.</returns>
        [HttpGet]
        [Route("version")]
        [SwaggerResponse(StatusCodes.Status200OK, "������ ����������.", typeof(string))]
        public async Task<IActionResult> Version()
        {
            return await Task.FromResult(this.Ok(Program.AppName));
        }
    }
}