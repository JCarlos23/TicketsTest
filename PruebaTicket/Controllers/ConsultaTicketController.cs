using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PruebaTicket.Core.Dto;
using PruebaTicket.Core.Interface;
using System;
using System.Threading.Tasks;

namespace PruebaTicket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultaTicketController : ControllerBase
    {
        private IDatosTicketUseCase _useCase;
        public ConsultaTicketController(IDatosTicketUseCase useCase)
        {
            _useCase = useCase ?? throw new ArgumentNullException(nameof(useCase));

        }

        [HttpGet]
        [Route("~/api/LecturaArchivo")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(StatusCode))]
        [ProducesErrorResponseType(typeof(StatusCode))]
        public async Task<IActionResult> CuandoGeneroArchivo()
        {
            StatusCode response;
            response = await _useCase.LeerArchivo();
            if (response.code == System.Net.HttpStatusCode.OK)
                return Ok(response);
            else
                return StatusCode(StatusCodes.Status500InternalServerError, response);

        }

        [HttpPost]
        [Route("~/api/DetalleTickets")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(StatusCode))]
        [ProducesErrorResponseType(typeof(GenericResponse<string>))]
        public async Task<IActionResult> CuandoEnvioTickets([FromBody] ResponseDto datos)
        {
            StatusCode response;
            response = await _useCase.DetalleTicket(datos);
            if (response.code == System.Net.HttpStatusCode.OK)
                return Ok(response);
            else
                return StatusCode(StatusCodes.Status500InternalServerError, response);
        }      
    }
}
