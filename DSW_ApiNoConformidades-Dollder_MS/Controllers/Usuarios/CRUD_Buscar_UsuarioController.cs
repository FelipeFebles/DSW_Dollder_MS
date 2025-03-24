using Microsoft.AspNetCore.Mvc;
using DSW_ApiNoConformidades_Dollder_MS.Application.Requests;
using MediatR;
using DSW_ApiNoConformidades_Dollder_MS.Base;
using DSW_ApiNoConformidades_Dollder_MS.Application.Responses.Usuarios;
using DSW_ApiNoConformidades_Dollder_MS.Application.Queries.Usuarios;
using DSW_ApiNoConformidades_Dollder_MS.Aplication.Queries.Usuarios;
using DSW_ApiNoConformidades_Dollder_MS.Aplication.Requests.Usuarios;

namespace DSW_ApiNoConformidades_Dollder_MS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CRUD_Buscar_UsuarioController : BaseController<CRUD_Buscar_UsuarioController>
    {

        private readonly IMediator _mediator;

        public CRUD_Buscar_UsuarioController(IMediator mediator, ILogger<CRUD_Buscar_UsuarioController> logger) : base(logger)
        {
            _mediator = mediator;
        }


        [HttpGet("BuscarUsuario")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<UsuarioResponse>>> BuscarUsuario()
        {
            _logger.LogInformation("Entrando al método que consulta los Usuario");

            try
            {
                var query = new BuscarUsuariosQuery();
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

        [HttpGet("BuscarUsuario_Usuario")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<UsuarioResponse>>> BuscarUsuario_Usuario([FromQuery] BuscarUsuarioRequest request)
        {
            _logger.LogInformation("Entrando al método que consulta los Usuario");

            try
            {
                var query = new BuscarUsuariosUsuarioQuery(request);
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

        [HttpGet("BuscarUsuario_Correo")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<UsuarioResponse>>> BuscarUsuario_Correo([FromQuery] BuscarUsuarioRequest request)
        {
            _logger.LogInformation("Entrando al método que consulta los Usuario");

            try
            {
                var query = new BuscarUsuariosCorreoQuery(request);
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

        [HttpGet("BuscarUsuario_ID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UsuarioResponse>> BuscarUsuario_ID([FromQuery] BuscarUsuarioIDRequest request)
        {
            _logger.LogInformation("Entrando al método que consulta los Usuario");

            try
            {
                var query = new BuscarUsuarioIdQuery(request);
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

        [HttpGet("BuscarUsuario_Login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UsuarioResponse>> BuscarUsuarioLogin([FromQuery] UsuarioLoginRequest request)
        {
            _logger.LogInformation("Entrando al método que consulta los Usuario");

            try
            {
                var query = new BuscarLoginQuery(request);
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
        [HttpGet("BuscarUsuario_Departamento")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<UsuarioResponse>>> BuscarUsuario_Departamento([FromQuery] BuscarUsuarioRequest request)
        {
            _logger.LogInformation("Entrando al método que consulta los Usuario");

            try
            {
                var query = new BuscarUsuariosDepartamentoQuery(request);
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