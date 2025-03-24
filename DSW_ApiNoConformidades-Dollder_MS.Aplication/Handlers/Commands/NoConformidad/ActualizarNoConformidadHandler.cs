using DSW_ApiNoConformidades_Dollder_MS.Aplication.Commands.Cierre;
using DSW_ApiNoConformidades_Dollder_MS.Aplication.Commands.NoConformidad;
using DSW_ApiNoConformidades_Dollder_MS.Aplication.Commands.Seguimiento;
using DSW_ApiNoConformidades_Dollder_MS.Application.Commands.NoConformidad;
using DSW_ApiNoConformidades_Dollder_MS.Application.Commands.Responsable;
using DSW_ApiNoConformidades_Dollder_MS.Application.Requests.NoConformidad;
using DSW_ApiNoConformidades_Dollder_MS.Application.Responses.NoConformidad;
using DSW_ApiNoConformidades_Dollder_MS.Core.Entities;
using DSW_ApiNoConformidades_Dollder_MS.Infrastructure.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DSW_ApiNoConformidades_Dollder_MS.Application.Handlers.Commands.NoConformidad
{
    public class ActualizarNoConformidadHandler : IRequestHandler<ActualizarNoConformidadCommand, IdNoConformidadResponse>
    {
        private readonly ApiDbContext _dbContext;
        private readonly ILogger<ActualizarNoConformidadHandler> _logger;
        private readonly MediatR.IMediator _mediator;
        public ActualizarNoConformidadHandler(ApiDbContext dbContext, ILogger<ActualizarNoConformidadHandler> logger, MediatR.IMediator mediator)
        {
            _dbContext = dbContext;
            _logger = logger;
            _mediator = mediator;
        }

        public Task<IdNoConformidadResponse> Handle(ActualizarNoConformidadCommand request, CancellationToken cancellationToken)
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

        private async Task<IdNoConformidadResponse> HandleAsync(ActualizarNoConformidadCommand request)
        {
            var transaccion = _dbContext.BeginTransaction();
            try
            {
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                ///     Pregunto si el NoConformidad existe
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                var NoConformidad = _dbContext.NoConformidad.FirstOrDefault(c => c.Id == request._request.Id);
                if (NoConformidad == null)
                {
                    throw new InvalidOperationException("Registro fallido: el NoConformidad NO existe");
                }
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                ///     Actualizo el NoConformidad
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                
                // Obtener las propiedades de NoConformidadEntity y NoConformidadRequest
                var propiedadesNoConformidad = typeof(NoConformidadEntity).GetProperties();
                var propiedadesRequest = typeof(NoConformidadRequest).GetProperties();

                // Actualizar propiedades solo si no están vacías o nulas
                foreach (var propRequest in propiedadesRequest)
                {
                    var valor = propRequest.GetValue(request._request);
                    if (valor != null)
                    {
                        // Buscar la propiedad correspondiente en NoConformidadEntity
                        var propNoConformidad = propiedadesNoConformidad.FirstOrDefault(p => p.Name.Equals(propRequest.Name, StringComparison.OrdinalIgnoreCase));

                        if (propNoConformidad != null && propNoConformidad.CanWrite && propNoConformidad.Name!="estado")
                        {
                            // Actualizar el valor de la propiedad en el NoConformidad
                            propNoConformidad.SetValue(NoConformidad, valor);
                        }
                    }
                }

                // Guardar cambios
                _dbContext.ChangeEntityState(NoConformidad, EntityState.Modified);
                await _dbContext.SaveEfContextChanges("APP");
                transaccion.Commit();

                //Primero cambio el estado
                if (request._request.estado != null)
                {
                    var command = new ActualizarEstadoNoConformidadCommand(request._request);
                    var response = await _mediator.Send(command);
                }

                if (request._request.responsables.Count() != 0)
                {
                    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    ///     Pregunto si los responsables existen
                    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                    foreach (var resp in request._request.responsables)
                    {
                        var Responsable = _dbContext.Responsable.FirstOrDefault(c => c.Id == resp.Id);
                        if (Responsable == null)
                        {
                            throw new InvalidOperationException("Registro fallido: el Responsable NO existe");
                        }
                        var command = new ActualizarResponsableCommand(resp);
                        var response = await _mediator.Send(command);
                    }
                }

                if (request._request.seguimientos.Count() !=0)
                {
                    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    ///     Pregunto si los Seguimientos existen
                    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    
                    foreach(var seg in request._request.seguimientos)
                    {
                        var Seguimiento = _dbContext.Seguimiento.FirstOrDefault(c => c.Id == seg.Id);
                        if (Seguimiento == null)
                        {
                            throw new InvalidOperationException("Registro fallido: el Seguimiento NO existe");
                        }
                        var command = new ActualizarSeguimientoCommand(seg);
                        var response = await _mediator.Send(command);
                    }
                }

                if (request._request.cierre.Id != null)
                {
                    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    ///     Pregunto si el Cierre existe
                    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    var Cierre = _dbContext.Cierre.FirstOrDefault(c => c.Id == request._request.cierre.Id);
                    if (Cierre == null)
                    {
                        throw new InvalidOperationException("Registro fallido: el Cierre NO existe");
                    }
                    var command = new ActualizarCierreCommand(request._request.cierre);
                    var response = await _mediator.Send(command);
                }


                _logger.LogInformation("NoConformidad actualizado correctamente. ID: {NoConformidadId}", NoConformidad.Id);
                return new IdNoConformidadResponse(NoConformidad.Id);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error AgregarOperarioHandler.HandleAsync. {Mensaje}", ex.Message);
                throw;
            }

        }
    }
}
