using Microsoft.AspNetCore.Mvc;
using MediatR;
using DSW_ApiNoConformidades_Dollder_MS.Base;
using DSW_ApiNoConformidades_Dollder_MS.Application.Requests.AccionesCorrectivas;
using DSW_ApiNoConformidades_Dollder_MS.Application.Responses.Acciones;
using DSW_ApiNoConformidades_Dollder_MS.Aplication.Commands.Acciones;


namespace DSW_ApiNoConformidades_Dollder_MS.Controllers.Acciones
{
    [Route("api/[controller]")]
    [ApiController]
    public class CRUD_Actualizar_AccionesController : BaseController<CRUD_Actualizar_AccionesController>
    {

        private readonly IMediator _mediator;

        public CRUD_Actualizar_AccionesController(IMediator mediator, ILogger<CRUD_Actualizar_AccionesController> logger) : base(logger)
        {
            _mediator = mediator;
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpPut("Actualizar_Acciones")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<IdAccionesResponse>> Actualizar_Acciones([FromBody] AccionesRequest request)
        {
            _logger.LogInformation("Entrando al método que registra los valores de prueba");
            try
            {
                var command = new ActualizarAccionesCommand(request);
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
