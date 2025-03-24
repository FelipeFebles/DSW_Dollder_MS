using DSW_ApiNoConformidades_Dollder_MS.Aplication.Responses.Seguimiento;
using DSW_ApiNoConformidades_Dollder_MS.Base;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using DSW_ApiNoConformidades_Dollder_MS.Aplication.Commands.Seguimiento;
using DSW_ApiNoConformidades_Dollder_MS.Aplication.Requests.Seguimiento;

namespace DSW_ApiNoConformidades_Dollder_MS.Controllers.Seguimiento
{
    [Route("api/[controller]")]
    [ApiController]
    public class CRUD_AgregarSeguimientoController : BaseController<CRUD_AgregarSeguimientoController>
    {

        private readonly IMediator _mediator;

        public CRUD_AgregarSeguimientoController(IMediator mediator, ILogger<CRUD_AgregarSeguimientoController> logger) : base(logger)
        {
            _mediator = mediator;
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpPost("Agregar_Seguimiento")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<IdSeguimientoReponse>> Agregar_Seguimiento([FromBody] SeguimientoRequest request)
        {
            _logger.LogInformation("Entrando al método que registra los valores de prueba");
            try
            {
                var command = new AgregarSeguimientoCommand(request);
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
