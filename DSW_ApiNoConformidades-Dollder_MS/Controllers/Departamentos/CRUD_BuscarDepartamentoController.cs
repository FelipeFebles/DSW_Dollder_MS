using DSW_ApiNoConformidades_Dollder_MS.Base;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using DSW_ApiNoConformidades_Dollder_MS.Aplication.Queries.Departamento;
using DSW_ApiNoConformidades_Dollder_MS.Application.Responses.Departamento;
using DSW_ApiNoConformidades_Dollder_MS.Aplication.Requests.Departamento;


namespace DSW_ApiNoConformidades_Dollder_MS.Controllers.Departamentos
{
    [Route("api/[controller]")]
    [ApiController]
    public class CRUD_BuscarDepartamentoController : BaseController<CRUD_BuscarDepartamentoController>
    {

        private readonly IMediator _mediator;

        public CRUD_BuscarDepartamentoController(IMediator mediator, ILogger<CRUD_BuscarDepartamentoController> logger) : base(logger)
        {
            _mediator = mediator;
        }

        [HttpGet("BuscarIdDepartamentos")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<DepartamentoResponse>> BuscarIdDepartamentos([FromQuery] IdDepartamentoRequest request)
        {
            _logger.LogInformation("Entrando al método que consulta los Notificaciones");

            try
            {
                var query = new BuscarIdDepartamentoQuery(request);
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

        [HttpGet("BuscarDepartamentos")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<DepartamentoResponse>>> BuscarDepartamentos()
        {
            _logger.LogInformation("Entrando al método que consulta los Notificaciones");

            try
            {
                var query = new BuscarDepartamentoQuery();
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
