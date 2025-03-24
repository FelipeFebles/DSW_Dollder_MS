using DSW_ApiNoConformidades_Dollder_MS.Application.Commands.NoConformidad;
using DSW_ApiNoConformidades_Dollder_MS.Application.Commands.Reportes;
using DSW_ApiNoConformidades_Dollder_MS.Application.Requests.Reportes;
using DSW_ApiNoConformidades_Dollder_MS.Application.Responses.Reportes;
using DSW_ApiNoConformidades_Dollder_MS.Core.Entities;
using DSW_ApiNoConformidades_Dollder_MS.Infrastructure.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DSW_ApiNoConformidades_Dollder_MS.Application.Handlers.Commands.Reportes
{
    public class ActualizarReporteHandler : IRequestHandler<ActualizarReporteCommand, IdReporteResponse>
    {
        private readonly MediatR.IMediator _mediator;
        private readonly ApiDbContext _dbContext;
        private readonly ILogger<ActualizarReporteHandler> _logger;
        public ActualizarReporteHandler(ApiDbContext dbContext, ILogger<ActualizarReporteHandler> logger, MediatR.IMediator mediator)
        {
            _dbContext = dbContext;
            _logger = logger;
            _mediator = mediator;
        }

        public Task<IdReporteResponse> Handle(ActualizarReporteCommand request, CancellationToken cancellationToken)
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

        private async Task<IdReporteResponse> HandleAsync(ActualizarReporteCommand request)
        {
            var transaccion = _dbContext.BeginTransaction();
            try
            {
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                ///     Pregunto si el reporte existe
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                var reporte = _dbContext.Reporte.FirstOrDefault(c => c.Id == request._request.Id);
                if (reporte == null)
                {
                    throw new InvalidOperationException("Registro fallido: el reporte NO existe");
                }
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                ///     Actualizo el reporte
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                // Obtener las propiedades de ReporteEntity y ReporteRequest
                var propiedadesReporte = typeof(ReporteEntity).GetProperties();
                var propiedadesRequest = typeof(ReporteRequest).GetProperties();

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
                            propReporte.SetValue(reporte, valor);
                        }
                    }
                }

                // Guardar cambios
                _dbContext.ChangeEntityState(reporte, EntityState.Modified);
                await _dbContext.SaveEfContextChanges("APP");
                transaccion.Commit();



                if (request._request.noConformidad.Id!=null)
                {
                    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    ///     Pregunto si la No Conformidad existe
                    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                    var NoConformidad = _dbContext.NoConformidad.FirstOrDefault(c => c.Id == request._request.noConformidad.Id);
                    if (NoConformidad == null)
                    {
                        throw new InvalidOperationException("Registro fallido: la NoConformidad NO existe");
                    }
                    var command = new ActualizarNoConformidadCommand(request._request.noConformidad);
                    var response = await _mediator.Send(command);
                }
                
                _logger.LogInformation("Reporte actualizado correctamente. ID: {ReporteId}", reporte.Id);
                return new IdReporteResponse(reporte.Id);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error AgregarOperarioHandler.HandleAsync. {Mensaje}", ex.Message);
                throw;
            }

        }
    }
}
