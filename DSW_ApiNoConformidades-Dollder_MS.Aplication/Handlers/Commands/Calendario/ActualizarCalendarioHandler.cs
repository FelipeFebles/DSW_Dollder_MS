using DSW_ApiNoConformidades_Dollder_MS.Aplication.Commands.Calendario;
using DSW_ApiNoConformidades_Dollder_MS.Aplication.Requests.Calendario;
using DSW_ApiNoConformidades_Dollder_MS.Aplication.Responses.Calendario;
using DSW_ApiNoConformidades_Dollder_MS.Core.Entities;
using DSW_ApiNoConformidades_Dollder_MS.Infrastructure.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DSW_ApiNoConformidades_Dollder_MS.Aplication.Handlers.Commands.Calendario
{
    public class ActualizarCalendarioHandler : IRequestHandler<ActualizarCalendarioCommand, IdCalendarioResponse>
    {
        private readonly ApiDbContext _dbContext;
        private readonly ILogger<ActualizarCalendarioHandler> _logger;
        public ActualizarCalendarioHandler(ApiDbContext dbContext, ILogger<ActualizarCalendarioHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public Task<IdCalendarioResponse> Handle(ActualizarCalendarioCommand request, CancellationToken cancellationToken)
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

        private async Task<IdCalendarioResponse> HandleAsync(ActualizarCalendarioCommand request)
        {
            var transaccion = _dbContext.BeginTransaction();
            try
            {
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                ///     Pregunto si el Departamento existe
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                var cal = _dbContext.Calendario.FirstOrDefault(c => c.Id == request._request.Id);
                if (cal == null)
                {
                    throw new InvalidOperationException("Registro fallido: el Calendario NO existe");
                }
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                ///     Actualizo la Departamento
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                // Obtener las propiedades de DepartamentoEntity y DepartamentoRequest
                var propiedadesDepartamento = typeof(CalendarioEntity).GetProperties();
                var propiedadesRequest = typeof(CalendarioRequest).GetProperties();

                // Actualizar propiedades solo si no están vacías o nulas
                foreach (var propRequest in propiedadesRequest)
                {
                    var valor = propRequest.GetValue(request._request);
                    if (valor != null)
                    {
                        // Buscar la propiedad correspondiente en DepartamentoEntity
                        var propDepartamento = propiedadesDepartamento.FirstOrDefault(p => p.Name.Equals(propRequest.Name, StringComparison.OrdinalIgnoreCase));

                        if (propDepartamento != null && propDepartamento.CanWrite)
                        {
                            // Actualizar el valor de la propiedad en el Departamento
                            propDepartamento.SetValue(cal, valor);
                        }
                    }
                }

                // Guardar cambios
                _dbContext.ChangeEntityState(cal, EntityState.Modified);
                await _dbContext.SaveEfContextChanges("APP");
                transaccion.Commit();

                _logger.LogInformation("Departamento actualizado correctamente. ID: {DepartamentoId}", cal.Id);
                return new IdCalendarioResponse(cal.Id);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error AgregarOperarioHandler.HandleAsync. {Mensaje}", ex.Message);
                throw;
            }

        }
    }
}

