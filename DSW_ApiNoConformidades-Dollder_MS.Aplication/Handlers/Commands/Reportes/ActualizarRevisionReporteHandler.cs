using DSW_ApiNoConformidades_Dollder_MS.Application.Commands.Reportes;
using DSW_ApiNoConformidades_Dollder_MS.Application.Requests.RevisionReporte;
using DSW_ApiNoConformidades_Dollder_MS.Application.Responses.RevisionReporte;
using DSW_ApiNoConformidades_Dollder_MS.Core.Entities;
using DSW_ApiNoConformidades_Dollder_MS.Infrastructure.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DSW_ApiNoConformidades_Dollder_MS.Application.Handlers.Commands.Reportes
{
    public class ActualizarRevisionReporteHandler : IRequestHandler<ActualizarRevisionReporteCommand, IdRevisionReporteResponse>
    {
        private readonly ApiDbContext _dbContext;
        private readonly ILogger<ActualizarRevisionReporteHandler> _logger;
        public ActualizarRevisionReporteHandler(ApiDbContext dbContext, ILogger<ActualizarRevisionReporteHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public Task<IdRevisionReporteResponse> Handle(ActualizarRevisionReporteCommand request, CancellationToken cancellationToken)
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

        private async Task<IdRevisionReporteResponse> HandleAsync(ActualizarRevisionReporteCommand request)
        {
            var transaccion = _dbContext.BeginTransaction();
            try
            {
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                ///     Pregunto si la revision reporte existe
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                var revision_reporte = _dbContext.RevisionReporte.FirstOrDefault(c => c.reporte_Id == request._request.id_reporte);
                if (revision_reporte == null)
                {
                    throw new InvalidOperationException("Registro fallido: el reporte NO existe");
                }
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                ///     Actualizo la revision reporte
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                // Obtener las propiedades de ReporteEntity y ReporteRequest
                var propiedadesReporte = typeof(RevisionReporteEntity).GetProperties();
                var propiedadesRequest = typeof(RevisionReporteRequest).GetProperties();

                // Actualizar propiedades solo si no están vacías o nulas
                foreach (var propRequest in propiedadesRequest)
                {
                    var valor = propRequest.GetValue(request._request);
                    if (valor != null)
                    {
                        // Buscar la propiedad correspondiente en ReporteEntity
                        var propReporte = propiedadesReporte.FirstOrDefault(p => p.Name.Equals(propRequest.Name, StringComparison.OrdinalIgnoreCase));

                        if (propReporte != null && propReporte.CanWrite)
                        {
                            // Actualizar el valor de la propiedad en el reporte
                            propReporte.SetValue(revision_reporte, valor);
                        }
                    }
                }

                // Guardar cambios
                _dbContext.ChangeEntityState(revision_reporte, EntityState.Modified);
                await _dbContext.SaveEfContextChanges("APP");
                transaccion.Commit();

                _logger.LogInformation("Reporte actualizado correctamente. ID: {ReporteId}", revision_reporte.Id);
                return new IdRevisionReporteResponse(revision_reporte.Id, revision_reporte.reporte_Id);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error AgregarOperarioHandler.HandleAsync. {Mensaje}", ex.Message);
                throw;
            }

        }
    }
}
