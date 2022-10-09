using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mt.ChangeLog.WebAPI.Infrastracture;
using Mt.Results;
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
        [SwaggerResponse(StatusCodes.Status200OK, "������ ����������.", typeof(MtMessageResult))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "������ � ������ ����������, ������ ���������.", typeof(MtProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "���������� ������ �������.", typeof(MtProblemDetails))]
        public async Task<IActionResult> Version()
        {
            return await Task.FromResult(this.Ok(new MtMessageResult(Program.AppName)));
        }

        /// <summary>
        /// �������� �������� ����������.
        /// </summary>
        /// <returns>�������� ����������.</returns>
        [HttpGet]
        [Route("description")]
        [SwaggerResponse(StatusCodes.Status200OK, "������ ����������.", typeof(MtAppDescriptionModel))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "������ � ������ ����������, ������ ���������.", typeof(MtProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "���������� ������ �������.", typeof(MtProblemDetails))]
        public async Task<IActionResult> Description()
        {
            return await Task.FromResult(this.Ok(new MtAppDescriptionModel()
            {
                Version = Program.AppName,
                Copyright = "��� ������������� 1993 � 2022.",
                Repository = "https://github.com/g-aa/mt-changelog",
                Description = "���������� ������������� ��� "
                    + "������������ � ����������� ���������, "
                    + "� ����������� ����������� ��������� ������������� "
                    + "(����-100/120/150/160/M4) "
                    + "��������������������� ������� (���).",
            }));
        }
    }
}