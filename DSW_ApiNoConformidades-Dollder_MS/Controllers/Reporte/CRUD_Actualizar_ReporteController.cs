using Microsoft.AspNetCore.Mvc;
using MediatR;
using DSW_ApiNoConformidades_Dollder_MS.Base;
using DSW_ApiNoConformidades_Dollder_MS.Application.Requests.Reportes;
using DSW_ApiNoConformidades_Dollder_MS.Application.Responses.Reportes;
using DSW_ApiNoConformidades_Dollder_MS.Application.Commands.Reportes;
using DSW_ApiNoConformidades_Dollder_MS.Application.Responses.RevisionReporte;
using DSW_ApiNoConformidades_Dollder_MS.Application.Requests.RevisionReporte;

namespace ApiMS.Controllers.Reporte
{
    [Route("api/[controller]")]
    [ApiController]
    public class CRUD_Actualizar_ReporteController : BaseController<CRUD_Actualizar_ReporteController>
    {

        private readonly IMediator _mediator;

        public CRUD_Actualizar_ReporteController(IMediator mediator, ILogger<CRUD_Actualizar_ReporteController> logger) : base(logger)
        {
            _mediator = mediator;
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpPut("Actualizar_Reporte")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IdReporteResponse>> ActualizarReporte([FromBody] ReporteRequest request)
        {
            _logger.LogInformation("Entrando al método que registra los valores de prueba");
            try
            {
                var command = new ActualizarReporteCommand(request);
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

        [HttpPut("Actualizar_RevisionReporte")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IdRevisionReporteResponse>> ActualizarRevisionReporte([FromBody] RevisionReporteRequest request)
        {
            _logger.LogInformation("Entrando al método que registra los valores de prueba");
            try
            {
                var command = new ActualizarRevisionReporteCommand(request);
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
