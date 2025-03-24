using DSW_ApiNoConformidades_Dollder_MS.Aplication.Commands.VerificacionEfectividad;
using DSW_ApiNoConformidades_Dollder_MS.Aplication.Requests.VerificacionEfectividad;
using DSW_ApiNoConformidades_Dollder_MS.Aplication.Responses.VerificacionEfectividad;
using DSW_ApiNoConformidades_Dollder_MS.Base;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DSW_ApiNoConformidades_Dollder_MS.Controllers.VerificacionEfectividad
{
    [Route("api/[controller]")]
    [ApiController]
    public class CRUD_AgregarVerificacionEfectividadController : BaseController<CRUD_AgregarVerificacionEfectividadController>
    {

        private readonly IMediator _mediator;

        public CRUD_AgregarVerificacionEfectividadController(IMediator mediator, ILogger<CRUD_AgregarVerificacionEfectividadController> logger) : base(logger)
        {
            _mediator = mediator;
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpPost("Agregar_VerificacionEfectividad")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<IdVerificacionEfectividadResponse>> Agregar_VerificacionEfectividad([FromBody] VerificacionEfectividadRequest request)
        {
            _logger.LogInformation("Entrando al método que registra los valores de prueba");
            try
            {
                var command = new AgregarVerificacionEfectividadCommand(request);
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
