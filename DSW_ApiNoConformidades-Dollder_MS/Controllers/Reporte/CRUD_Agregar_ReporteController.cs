using Microsoft.AspNetCore.Mvc;
using MediatR;
using DSW_ApiNoConformidades_Dollder_MS.Base;
using DSW_ApiNoConformidades_Dollder_MS.Application.Responses.Reportes;
using DSW_ApiNoConformidades_Dollder_MS.Application.Requests.Reportes;
using DSW_ApiNoConformidades_Dollder_MS.Application.Commands.Reportes;

namespace DSW_ApiNoConformidades_Dollder_MS.Controllers.Reporte
{
    [Route("api/[controller]")]
    [ApiController]
    public class CRUD_Agregar_ReporteController : BaseController<CRUD_Agregar_ReporteController>
    {

        private readonly IMediator _mediator;

        public CRUD_Agregar_ReporteController(IMediator mediator, ILogger<CRUD_Agregar_ReporteController> logger) : base(logger)
        {
            _mediator = mediator;
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpPost("Agregar_Reporte")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<IdReporteResponse>> AgregarReporte([FromBody] ReporteRequest request)
        {
            _logger.LogInformation("Entrando al método que registra los valores de prueba");
            try
            {
                var command = new AgregarReporteCommand(request);
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
