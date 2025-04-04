﻿using Microsoft.AspNetCore.Mvc;
using MediatR;
using DSW_ApiNoConformidades_Dollder_MS.Base;
using DSW_ApiNoConformidades_Dollder_MS.Application.Responses.Usuarios;
using DSW_ApiNoConformidades_Dollder_MS.Application.Requests.Usuarios;
using DSW_ApiNoConformidades_Dollder_MS.Application.Commands.Usuario;

namespace DSW_ApiNoConformidades_Dollder_MS.Controllers.Usuarios
{
    [Route("api/[controller]")]
    [ApiController]
    public class CRUD_Actualizar_UsuarioController : BaseController<CRUD_Actualizar_UsuarioController>
    {

        private readonly IMediator _mediator;

        public CRUD_Actualizar_UsuarioController(IMediator mediator, ILogger<CRUD_Actualizar_UsuarioController> logger) : base(logger)
        {
            _mediator = mediator;
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpPut("Actualizar_Usuario")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IdUsuarioResponse>> ActualizarUsuario([FromBody] UsuarioRequest request)
        {
            _logger.LogInformation("Entrando al método que registra los valores de prueba");
            try
            {
                var command = new ActualizarUsuarioCommand(request);
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
