
using DSW_ApiNoConformidades_Dollder_MS.Base;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using DSW_ApiNoConformidades_Dollder_MS.Aplication.Responses.Indicadores;
using DSW_ApiNoConformidades_Dollder_MS.Aplication.Queries.Indicadores;

namespace DSW_ApiNoConformidades_Dollder_MS.Controllers.Indicador
{
    [Route("api/[controller]")]
    [ApiController]
    public class CRUD_BuscarIndicadoresController : BaseController<CRUD_BuscarIndicadoresController>
    {

        private readonly IMediator _mediator;

        public CRUD_BuscarIndicadoresController(IMediator mediator, ILogger<CRUD_BuscarIndicadoresController> logger) : base(logger)
        {
            _mediator = mediator;
        }


        [HttpGet("BuscarIndicadoresOrigen")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<IndicadorOrigenResponse>>> BuscarIndicadoresOrigen()
        {
            _logger.LogInformation("Entrando al método que consulta los Notificaciones");

            try
            {
                var query = new BuscarIndicadoresOrigenQuery();
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



        [HttpGet("BuscarIndicadoresCausa")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<IndicadorCausaResponse>>> BuscarIndicadoresCausa()
        {
            _logger.LogInformation("Entrando al método que consulta los Notificaciones");

            try
            {
                var query = new BuscarIndicadoresCausaQuery();
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
