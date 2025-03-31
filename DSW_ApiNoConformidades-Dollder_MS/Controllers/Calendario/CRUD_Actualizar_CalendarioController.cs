using DSW_ApiNoConformidades_Dollder_MS.Base;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using DSW_ApiNoConformidades_Dollder_MS.Aplication.Requests.Calendario;
using DSW_ApiNoConformidades_Dollder_MS.Aplication.Responses.Calendario;
using DSW_ApiNoConformidades_Dollder_MS.Aplication.Commands.Calendario;

namespace DSW_ApiNoConformidades_Dollder_MS.Controllers.Calendario
{
    [Route("api/[controller]")]
    [ApiController]
    public class CRUD_Actualizar_CalendarioController : BaseController<CRUD_Actualizar_CalendarioController>
    {

        private readonly IMediator _mediator;

        public CRUD_Actualizar_CalendarioController(IMediator mediator, ILogger<CRUD_Actualizar_CalendarioController> logger) : base(logger)
        {
            _mediator = mediator;
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpPut("Actualizar_Calendario")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<IdCalendarioResponse>> Actualizar_Calendario([FromBody] CalendarioRequest request)
        {
            _logger.LogInformation("Entrando al método que registra los valores de prueba");
            try
            {
                var command = new ActualizarCalendarioCommand(request);
                var response = await _mediator.Send(command);
                return Response200(NewResponseOperation(), response);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocurrio un error al intentar registrar un valor de prueba. Exception: " + ex);
                return Response400(NewResponseOperation(), ex.Message,
                    "Ocurrio un error al intentar registrar un valor de prueba", ex.InnerException?.ToString());
            }
        }
    }
}
