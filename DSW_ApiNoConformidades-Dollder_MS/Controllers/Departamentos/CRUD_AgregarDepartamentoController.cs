using DSW_ApiNoConformidades_Dollder_MS.Aplication.Commands.Departamento;
using DSW_ApiNoConformidades_Dollder_MS.Aplication.Responses.Departamento;
using DSW_ApiNoConformidades_Dollder_MS.Application.Requests.Departamento;
using DSW_ApiNoConformidades_Dollder_MS.Base;
using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace DSW_ApiNoConformidades_Dollder_MS.Controllers.Departamentos
{
    [Route("api/[controller]")]
    [ApiController]
    public class CRUD_AgregarDepartamentoController : BaseController<CRUD_AgregarDepartamentoController>
    {

        private readonly IMediator _mediator;

        public CRUD_AgregarDepartamentoController(IMediator mediator, ILogger<CRUD_AgregarDepartamentoController> logger) : base(logger)
        {
            _mediator = mediator;
        }

        [HttpPost("Agregar_Departamento")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<IdDepartamentoResponse>> Agregar_Departamento([FromBody] DepartamentoRequest request)
        {
            _logger.LogInformation("Entrando al método que registra los valores de prueba");
            try
            {
                var command = new AgregarDepartamentoCommand(request);
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
