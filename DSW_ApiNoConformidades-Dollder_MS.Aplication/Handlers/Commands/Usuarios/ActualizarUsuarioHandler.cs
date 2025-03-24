using DSW_ApiNoConformidades_Dollder_MS.Application.Commands.Usuario;
using DSW_ApiNoConformidades_Dollder_MS.Application.Requests.Usuarios;
using DSW_ApiNoConformidades_Dollder_MS.Application.Responses.Usuarios;
using DSW_ApiNoConformidades_Dollder_MS.Core.Entities;
using DSW_ApiNoConformidades_Dollder_MS.Infrastructure.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DSW_ApiNoConformidades_Dollder_MS.Application.Handlers.Commands.Usuarios
{
    public class ActualizarUsuarioHandler : IRequestHandler<ActualizarUsuarioCommand, IdUsuarioResponse>
    {
        private readonly ApiDbContext _dbContext;
        private readonly ILogger<ActualizarUsuarioHandler> _logger;
        public ActualizarUsuarioHandler(ApiDbContext dbContext, ILogger<ActualizarUsuarioHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public Task<IdUsuarioResponse> Handle(ActualizarUsuarioCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request is null) //Pregunto si el request es nulo
                {
                    _logger.LogWarning("AgregarOperarioHandler.Handle: Request nulo.");
                    throw new ArgumentNullException(nameof(request));

                }
                else
                {
                    return HandleAsync(request);
                }
            }
            catch (Exception)
            {
                _logger.LogWarning("AgregarOperarioHandler.Handle: ArgumentNullException");
                throw;
            }
        }

        private async Task<IdUsuarioResponse> HandleAsync(ActualizarUsuarioCommand request)
        {
            var transaccion = _dbContext.BeginTransaction();
            try
            {
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                ///     Pregunto si el usuario existe
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                var usuario = _dbContext.Usuario.FirstOrDefault(c => c.Id == request._request.Id);
                if (usuario == null)
                {
                    throw new InvalidOperationException("Registro fallido: el usuario NO existe");
                }
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                ///     Actualizo el Usuario
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                // Obtener las propiedades de UsuarioEntity y UsuarioRequest
                var propiedadesUsuario = typeof(UsuarioEntity).GetProperties();
                var propiedadesRequest = typeof(UsuarioRequest).GetProperties();

                // Actualizar propiedades solo si no están vacías o nulas
                foreach (var propRequest in propiedadesRequest)
                {
                    var valor = propRequest.GetValue(request._request);
                    if (valor != null && !(valor is string str && string.IsNullOrWhiteSpace(str)))
                    {
                        // Buscar la propiedad correspondiente en UsuarioEntity
                        var propUsuario = propiedadesUsuario.FirstOrDefault(p => p.Name.Equals(propRequest.Name, StringComparison.OrdinalIgnoreCase));

                        if (propUsuario != null && propUsuario.CanWrite)
                        {
                            // Actualizar el valor de la propiedad en el Usuario
                            propUsuario.SetValue(usuario, valor);
                        }
                    }
                }

                // Guardar cambios
                _dbContext.ChangeEntityState(usuario, EntityState.Modified);
                await _dbContext.SaveEfContextChanges("APP");
                transaccion.Commit();

                _logger.LogInformation("Usuario actualizado correctamente. ID: {UsuarioId}", usuario.Id);
                return new IdUsuarioResponse(usuario.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error AgregarOperarioHandler.HandleAsync. {Mensaje}", ex.Message);
                throw;
            }

        }
    }
}
