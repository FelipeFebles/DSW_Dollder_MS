using DSW_ApiNoConformidades_Dollder_MS.Base;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using DSW_ApiNoConformidades_Dollder_MS.Aplication.Queries.NoConformidades;
using DSW_ApiNoConformidades_Dollder_MS.Aplication.Responses.NoConformidad;
using DSW_ApiNoConformidades_Dollder_MS.Aplication.Requests.NoConformidad;

namespace DSW_ApiNoConformidades_Dollder_MS.Controllers.NoConformidad
{
    [Route("api/[controller]")]
    [ApiController]
    public class CRUD_Buscar_NoConformidadController : BaseController<CRUD_Buscar_NoConformidadController>
    {

        private readonly IMediator _mediator;

        public CRUD_Buscar_NoConformidadController(IMediator mediator, ILogger<CRUD_Buscar_NoConformidadController> logger) : base(logger)
        {
            _mediator = mediator;
        }


        [HttpGet("BuscarTodasNoConformidades")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<NoConformidadesResponse>>> BuscarTodasNoConformidades()
        {
            _logger.LogInformation("Entrando al método que consulta los NoConformidad");

            try
            {
                var query = new BuscarNoConformidadesQuery();
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

        [HttpGet("BuscarNoConformidadCompleta")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<NoConformidadesResponse>> BuscarNoConformidadCompleta([FromQuery] IdNoConformidadRequest request)
        {
            _logger.LogInformation("Entrando al método que consulta los NoConformidad");

            try
            {
                var query = new BuscarNoConformidadCompletaQuery(request);
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

        [HttpGet("BuscarNoConformidad")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<NoConformidadesResponse>> BuscarNoConformidad([FromQuery] IdNoConformidadRequest request)
        {
            _logger.LogInformation("Entrando al método que consulta los NoConformidad");

            try
            {
                var query = new BuscarNoConformidadQuery(request);
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
