using DSW_ApiNoConformidades_Dollder_MS.Aplication.Commands.Seguimiento;
using DSW_ApiNoConformidades_Dollder_MS.Aplication.Requests.Seguimiento;
using DSW_ApiNoConformidades_Dollder_MS.Aplication.Responses.Seguimiento;
using DSW_ApiNoConformidades_Dollder_MS.Core.Entities;
using DSW_ApiNoConformidades_Dollder_MS.Infrastructure.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DSW_ApiNoConformidades_Dollder_MS.Aplication.Handlers.Commands.Seguimiento
{
    public class ActualizarSeguimientoHandler : IRequestHandler<ActualizarSeguimientoCommand, IdSeguimientoReponse>
    {
        private readonly ApiDbContext _dbContext;
        private readonly ILogger<ActualizarSeguimientoHandler> _logger;
        public ActualizarSeguimientoHandler(ApiDbContext dbContext, ILogger<ActualizarSeguimientoHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public Task<IdSeguimientoReponse> Handle(ActualizarSeguimientoCommand request, CancellationToken cancellationToken)
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

        private async Task<IdSeguimientoReponse> HandleAsync(ActualizarSeguimientoCommand request)
        {
            var transaccion = _dbContext.BeginTransaction();
            try
            {
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                ///     Pregunto si el Seguimiento existe
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                var Seguimiento = _dbContext.Seguimiento.FirstOrDefault(c => c.Id == request._request.Id);
                if (Seguimiento == null)
                {
                    throw new InvalidOperationException("Registro fallido: el Seguimiento NO existe");
                }
                request._request.noConformidad_Id = Seguimiento.noConformidad_Id;
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                ///     Actualizo el Seguimiento
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                // Obtener las propiedades de SeguimientoEntity y SeguimientoRequest
                var propiedadesSeguimiento = typeof(SeguimientoEntity).GetProperties();
                var propiedadesRequest = typeof(SeguimientoRequest).GetProperties();

                // Actualizar propiedades solo si no están vacías o nulas
                foreach (var propRequest in propiedadesRequest)
                {
                    var valor = propRequest.GetValue(request._request);
                    if (valor != null)
                    {
                        // Buscar la propiedad correspondiente en SeguimientoEntity
                        var propSeguimiento = propiedadesSeguimiento.FirstOrDefault(p => p.Name.Equals(propRequest.Name, StringComparison.OrdinalIgnoreCase));

                        if (propSeguimiento != null && propSeguimiento.CanWrite)
                        {
                            // Actualizar el valor de la propiedad en el Seguimiento
                            propSeguimiento.SetValue(Seguimiento, valor);
                        }
                    }
                }

                // Guardar cambios
                _dbContext.ChangeEntityState(Seguimiento, EntityState.Modified);
                await _dbContext.SaveEfContextChanges("APP");
                transaccion.Commit();

                _logger.LogInformation("Seguimiento actualizado correctamente. ID: {SeguimientoId}", Seguimiento.Id);
                return new IdSeguimientoReponse(Seguimiento.Id);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error AgregarOperarioHandler.HandleAsync. {Mensaje}", ex.Message);
                throw;
            }

        }
    }
}
