using Microsoft.AspNetCore.Mvc;
using MediatR;
using DSW_ApiNoConformidades_Dollder_MS.Base;
using DSW_ApiNoConformidades_Dollder_MS.Application.Commands.Responsable;
using DSW_ApiNoConformidades_Dollder_MS.Application.Requests.Responsables;
using DSW_ApiNoConformidades_Dollder_MS.Application.Responses.Responsable;

namespace DSW_ApiNoConformidades_Dollder_MS.Controllers.Responsable
{
    [Route("api/[controller]")]
    [ApiController]
    public class CRUD_Actualizar_ResponsableController : BaseController<CRUD_Actualizar_ResponsableController>
    {

        private readonly IMediator _mediator;

        public CRUD_Actualizar_ResponsableController(IMediator mediator, ILogger<CRUD_Actualizar_ResponsableController> logger) : base(logger)
        {
            _mediator = mediator;
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpPut("Actualizar_Responsable")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<IdResponsableResponse>> Actualizar_Responsable([FromBody] ResponsableRequest request)
        {
            _logger.LogInformation("Entrando al método que registra los valores de prueba");
            try
            {
                var command = new ActualizarResponsableCommand(request);
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
