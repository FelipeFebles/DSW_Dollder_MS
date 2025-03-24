using DSW_ApiNoConformidades_Dollder_MS.Aplication.Queries.Reporte;
using DSW_ApiNoConformidades_Dollder_MS.Application.Requests;
using DSW_ApiNoConformidades_Dollder_MS.Base;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using DSW_ApiNoConformidades_Dollder_MS.Aplication.Responses.Reportes;
using DSW_ApiNoConformidades_Dollder_MS.Application.Requests.Reportes;
using DSW_ApiNoConformidades_Dollder_MS.Aplication.Responses.RevisionReporte;
using DSW_ApiNoConformidades_Dollder_MS.Application.Requests.RevisionReporte;

namespace DSW_ApiNoConformidades_Dollder_MS.Controllers.Reporte
{
    [Route("api/[controller]")]
    [ApiController]
    public class CRUD_Buscar_ReporteController : BaseController<CRUD_Buscar_ReporteController>
    {

        private readonly IMediator _mediator;

        public CRUD_Buscar_ReporteController(IMediator mediator, ILogger<CRUD_Buscar_ReporteController> logger) : base(logger)
        {
            _mediator = mediator;
        }


        [HttpGet("BuscarReportesID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<ReporteResponse>>> BuscarReportesID([FromQuery] BuscarUsuarioIDRequest request)
        {
            _logger.LogInformation("Entrando al método que consulta los Reporte");

            try
            {
                var query = new BuscarReportesIDQuery(request);
                var response = await _mediator.Send(query);
                return Response200(NewResponseOperation(), response);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocurrio un error al intentar registrar un valor de prueba. Exception: " + ex);
                return Response400(NewResponseOperation(), ex.Message,
                    "Ocurrio un error al intentar registrar un valor de prueba", ex.InnerException?.ToString());
            }
        }
        [HttpGet("BuscarReporteID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Guid>> BuscarReporteID([FromQuery] Guid request)
        {
            _logger.LogInformation("Entrando al método que consulta los Reporte");

            try
            {
                var query = new BuscarReporteIdQuery(request);
                var response = await _mediator.Send(query);
                return Response200(NewResponseOperation(), response);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocurrio un error al intentar registrar un valor de prueba. Exception: " + ex);
                return Response400(NewResponseOperation(), ex.Message,
                    "Ocurrio un error al intentar registrar un valor de prueba", ex.InnerException?.ToString());
            }
        }

        [HttpGet("BuscarReporte")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ReporteResponse>> BuscarReporte([FromQuery] ReporteRequest request)
        {
            _logger.LogInformation("Entrando al método que consulta los Reporte");

            try
            {
                var query = new BuscarReporteQuery(request);
                var response = await _mediator.Send(query);
                return Response200(NewResponseOperation(), response);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocurrio un error al intentar registrar un valor de prueba. Exception: " + ex);
                return Response400(NewResponseOperation(), ex.Message,
                    "Ocurrio un error al intentar registrar un valor de prueba", ex.InnerException?.ToString());
            }
        }

        [HttpGet("BuscarRevisionReporte")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<RevisionReporteResponse>> BuscarRevisionReporte([FromQuery] RevisionReporteRequest request)
        {
            _logger.LogInformation("Entrando al método que consulta los Reporte");

            try
            {
                var query = new BuscarRevisionReporteQuery(request);
                var response = await _mediator.Send(query);
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
