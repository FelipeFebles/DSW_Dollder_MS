using DSW_ApiNoConformidades_Dollder_MS.Base;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using DSW_ApiNoConformidades_Dollder_MS.Aplication.Responses.Departamento;
using DSW_ApiNoConformidades_Dollder_MS.Application.Requests.Departamento;
using DSW_ApiNoConformidades_Dollder_MS.Aplication.Commands.Departamento;

namespace DSW_ApiNoConformidades_Dollder_MS.Controllers.Departamentos
{
    [Route("api/[controller]")]
    [ApiController]
    public class CRUD_ActualizarDepartamentoController : BaseController<CRUD_ActualizarDepartamentoController>
    {

        private readonly IMediator _mediator;

        public CRUD_ActualizarDepartamentoController(IMediator mediator, ILogger<CRUD_ActualizarDepartamentoController> logger) : base(logger)
        {
            _mediator = mediator;
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpPut("Actualizar_Departamento")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IdDepartamentoResponse>> ActualizarDepartamento([FromBody] DepartamentoRequest request)
        {
            _logger.LogInformation("Entrando al método que registra los valores de prueba");
            try
            {
                var command = new ActualizarDepartamentoCommand(request);
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
