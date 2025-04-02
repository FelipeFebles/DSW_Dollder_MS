using DSW_ApiNoConformidades_Dollder_MS.Aplication.Commands.Acciones;
using DSW_ApiNoConformidades_Dollder_MS.Aplication.Mappers.Calendario;
using DSW_ApiNoConformidades_Dollder_MS.Aplication.Queries.NoConformidades;
using DSW_ApiNoConformidades_Dollder_MS.Aplication.Requests.NoConformidad;
using DSW_ApiNoConformidades_Dollder_MS.Application.Requests.AccionesCorrectivas;
using DSW_ApiNoConformidades_Dollder_MS.Application.Responses.Acciones;
using DSW_ApiNoConformidades_Dollder_MS.Core.Entities;
using DSW_ApiNoConformidades_Dollder_MS.Infrastructure.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;


namespace DSW_ApiAccioneses_Dollder_MS.Aplication.Handlers.Commands.Acciones
{
    public class ActualizarAccionesHandler : IRequestHandler<ActualizarAccionesCommand, IdAccionesResponse>
    {
        private readonly ApiDbContext _dbContext;
        private readonly ILogger<ActualizarAccionesHandler> _logger;
        private readonly MediatR.IMediator _mediator;

        public ActualizarAccionesHandler(ApiDbContext dbContext, ILogger<ActualizarAccionesHandler> logger, MediatR.IMediator mediator)
        {
            _dbContext = dbContext;
            _logger = logger;
            _mediator = mediator;
        }

        public Task<IdAccionesResponse> Handle(ActualizarAccionesCommand request, CancellationToken cancellationToken)
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

        private async Task<IdAccionesResponse> HandleAsync(ActualizarAccionesCommand request)
        {
            var transaccion = _dbContext.BeginTransaction();
            try
            {
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                ///     Pregunto si las Acciones existen
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                var Acciones = _dbContext.Acciones.FirstOrDefault(c => c.Id == request._request.Id);
                if (Acciones == null)
                {
                    throw new InvalidOperationException("Registro fallido: La Accion NO existe");
                }

                if (request._request.fecha_compromiso != null) 
                {
                    var query = new BuscarNoConformidadCompletaQuery(new IdNoConformidadRequest { Data = Acciones.Id });
                    var response = await _mediator.Send(query);
                    var calendario = CalendarioMapper.MapCalendarioEntity2(request._request.fecha_compromiso, "Se establecio una fecha de compromiso de una acción, de la no conformidad: "+ response.noConformidad.numero_expedicion);
                    _dbContext.Calendario.Add(calendario);
                    await _dbContext.SaveEfContextChanges("APP");
                }
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                ///     Actualizo el Acciones
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                // Obtener las propiedades de AccionesEntity y AccionesRequest
                var propiedadesAcciones = typeof(AccionesEntity).GetProperties();
                var propiedadesRequest = typeof(AccionesRequest).GetProperties();

                // Actualizar propiedades solo si no están vacías o nulas
                foreach (var propRequest in propiedadesRequest)
                {
                    var valor = propRequest.GetValue(request._request);
                    if (valor != null)
                    {
                        // Buscar la propiedad correspondiente en AccionesEntity
                        var propAcciones = propiedadesAcciones.FirstOrDefault(p => p.Name.Equals(propRequest.Name, StringComparison.OrdinalIgnoreCase));

                        if (propAcciones != null && propAcciones.CanWrite)
                        {
                            // Actualizar el valor de la propiedad en el Acciones
                            propAcciones.SetValue(Acciones, valor);
                        }
                    }
                }

                // Guardar cambios
                _dbContext.ChangeEntityState(Acciones, EntityState.Modified);
                await _dbContext.SaveEfContextChanges("APP");
                transaccion.Commit();

                _logger.LogInformation("Acciones actualizado correctamente. ID: {AccionesId}", Acciones.Id);
                return new IdAccionesResponse(Acciones.Id);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error AgregarOperarioHandler.HandleAsync. {Mensaje}", ex.Message);
                throw;
            }

        }
    }
}
