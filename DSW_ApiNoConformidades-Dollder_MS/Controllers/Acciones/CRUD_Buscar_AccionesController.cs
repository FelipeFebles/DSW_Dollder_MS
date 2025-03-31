using DSW_ApiNoConformidades_Dollder_MS.Base;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using DSW_ApiNoConformidades_Dollder_MS.Aplication.Queries.Acciones;
using DSW_ApiNoConformidades_Dollder_MS.Aplication.Responses.Acciones;
using DSW_ApiNoConformidades_Dollder_MS.Application.Requests;


namespace DSW_ApiNoConformidades_Dollder_MS.Controllers.Acciones
{
    [Route("api/[controller]")]
    [ApiController]
    public class CRUD_Buscar_AccionesController : BaseController<CRUD_Buscar_AccionesController>
    {

        private readonly IMediator _mediator;

        public CRUD_Buscar_AccionesController(IMediator mediator, ILogger<CRUD_Buscar_AccionesController> logger) : base(logger)
        {
            _mediator = mediator;
        }


        [HttpGet("Buscar_AccionesPreventivas")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<AccionesResponse>>> Buscar_AccionesPreventivas([FromQuery] BuscarUsuarioIDRequest request)
        {
            _logger.LogInformation("Entrando al método que consulta los NoConformidad");

            try
            {
                var query = new BuscarIdAccionesPreventivasQuery(request);
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

        [HttpGet("Buscar_AccionesCorrectivas")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<AccionesResponse>>> Buscar_AccionesCorrectivas([FromQuery] BuscarUsuarioIDRequest request)
        {
            _logger.LogInformation("Entrando al método que consulta los NoConformidad");

            try
            {
                var query = new BuscarIdAccionesCorrectivasQuery(request);
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
