using Microsoft.AspNetCore.Mvc;
using MediatR;
using DSW_ApiNoConformidades_Dollder_MS.Application.Requests;
using DSW_ApiNoConformidades_Dollder_MS.Base;
using DSW_ApiNoConformidades_Dollder_MS.Aplication.Responses.Responsable;
using DSW_ApiNoConformidades_Dollder_MS.Aplication.Queries.Responsable;

namespace DSW_ApiNoConformidades_Dollder_MS.Controllers.Responsable
{
    [Route("api/[controller]")]
    [ApiController]
    public class CRUD_Buscar_ResponsableController : BaseController<CRUD_Buscar_ResponsableController>
    {

        private readonly IMediator _mediator;

        public CRUD_Buscar_ResponsableController(IMediator mediator, ILogger<CRUD_Buscar_ResponsableController> logger) : base(logger)
        {
            _mediator = mediator;
        }


        [HttpGet("BuscarResponsable")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<ResponsableResponse>>> BuscarResponsable([FromBody] BuscarUsuarioIDRequest request)
        {
            _logger.LogInformation("Entrando al método que consulta los Responsable");

            try
            {
                var query = new BuscarResponsableQuery(request);
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
