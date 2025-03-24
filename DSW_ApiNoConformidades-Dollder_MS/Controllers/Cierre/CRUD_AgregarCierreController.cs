using DSW_ApiNoConformidades_Dollder_MS.Base;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using DSW_ApiNoConformidades_Dollder_MS.Aplication.Responses.Cierre;
using DSW_ApiNoConformidades_Dollder_MS.Aplication.Requests.Cierre;
using DSW_ApiNoConformidades_Dollder_MS.Aplication.Commands.Cierre;

namespace DSW_ApiNoConformidades_Dollder_MS.Controllers.Cierre
{
    [Route("api/[controller]")]
    [ApiController]
    public class CRUD_AgregarCierreController : BaseController<CRUD_AgregarCierreController>
    {

        private readonly IMediator _mediator;

        public CRUD_AgregarCierreController(IMediator mediator, ILogger<CRUD_AgregarCierreController> logger) : base(logger)
        {
            _mediator = mediator;
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpPost("Agregar_Cierre")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<IdCierreResponse>> Agregar_Cierre([FromBody] CierreRequest request)
        {
            _logger.LogInformation("Entrando al método que registra los valores de prueba");
            try
            {
                var command = new AgregarCierreCommand(request);
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
