using DSW_ApiNoConformidades_Dollder_MS.Base;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using DSW_ApiNoConformidades_Dollder_MS.Aplication.Responses.Calendario;
using DSW_ApiNoConformidades_Dollder_MS.Aplication.Queries.Calendario;

namespace DSW_ApiNoConformidades_Dollder_MS.Controllers.Calendario
{
    [Route("api/[controller]")]
    [ApiController]
    public class CRUD_Buscar_CalendarioController : BaseController<CRUD_Buscar_CalendarioController>
    {

        private readonly IMediator _mediator;

        public CRUD_Buscar_CalendarioController(IMediator mediator, ILogger<CRUD_Buscar_CalendarioController> logger) : base(logger)
        {
            _mediator = mediator;
        }


        [HttpGet("Buscar_Calendario")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<CalendarioResponse>>> Buscar_Calendario()
        {
            _logger.LogInformation("Entrando al método que consulta los NoConformidad");

            try
            {
                var query = new BuscarCalendarioQuery();
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
