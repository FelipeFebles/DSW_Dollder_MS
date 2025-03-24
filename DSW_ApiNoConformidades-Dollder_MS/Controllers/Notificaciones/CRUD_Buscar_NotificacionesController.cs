using Microsoft.AspNetCore.Mvc;
using MediatR;
using DSW_ApiNoConformidades_Dollder_MS.Base;
using DSW_ApiNoConformidades_Dollder_MS.Aplication.Responses.Notificaciones;
using DSW_ApiNoConformidades_Dollder_MS.Application.Requests;
using DSW_ApiNoConformidades_Dollder_MS.Aplication.Queries.Notificaciones;

namespace DSW_ApiNoConformidades_Dollder_MS.Controllers.Notificaciones
{
    [Route("api/[controller]")]
    [ApiController]
    public class CRUD_Buscar_NotificacionesController : BaseController<CRUD_Buscar_NotificacionesController>
    {

        private readonly IMediator _mediator;

        public CRUD_Buscar_NotificacionesController(IMediator mediator, ILogger<CRUD_Buscar_NotificacionesController> logger) : base(logger)
        {
            _mediator = mediator;
        }


        [HttpGet("BuscarNotificaciones")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<NotificacionesResponse>>> BuscarNotificaciones([FromQuery] BuscarUsuarioIDRequest request)
        {
            _logger.LogInformation("Entrando al método que consulta los Notificaciones");

            try
            {
                var query = new BuscarNotificacionesQuery(request);
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
