using DSW_ApiNoConformidades_Dollder_MS.Aplication.Commands.Acciones;
using DSW_ApiNoConformidades_Dollder_MS.Application.Commands.Responsable;
using DSW_ApiNoConformidades_Dollder_MS.Application.Requests.Responsables;
using DSW_ApiNoConformidades_Dollder_MS.Application.Responses.Responsable;
using DSW_ApiNoConformidades_Dollder_MS.Core.Entities;
using DSW_ApiNoConformidades_Dollder_MS.Infrastructure.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DSW_ApiNoConformidades_Dollder_MS.Application.Handlers.Commands.Responsable
{
    public class ActualizarResponsableHandler : IRequestHandler<ActualizarResponsableCommand, IdResponsableResponse>
    {
        private readonly ApiDbContext _dbContext;
        private readonly ILogger<ActualizarResponsableHandler> _logger;
        private readonly MediatR.IMediator _mediator;

        public ActualizarResponsableHandler(ApiDbContext dbContext, ILogger<ActualizarResponsableHandler> logger, MediatR.IMediator mediator)
        {
            _dbContext = dbContext;
            _logger = logger;
            _mediator = mediator;
        }

        public Task<IdResponsableResponse> Handle(ActualizarResponsableCommand request, CancellationToken cancellationToken)
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

        private async Task<IdResponsableResponse> HandleAsync(ActualizarResponsableCommand request)
        {
            var transaccion = _dbContext.BeginTransaction();
            try
            {
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                ///     Pregunto si el Responsable existe
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                var Responsable = _dbContext.Responsable.FirstOrDefault(c => c.Id == request._request.Id);
                if (Responsable == null)
                {
                    throw new InvalidOperationException("Registro fallido: el Responsable NO existe");
                }
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                ///     Actualizo el Responsable
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                // Obtener las propiedades de ResponsableEntity y ResponsableRequest
                var propiedadesResponsable = typeof(ResponsableEntity).GetProperties();
                var propiedadesRequest = typeof(ResponsableRequest).GetProperties();

                // Actualizar propiedades solo si no están vacías o nulas
                foreach (var propRequest in propiedadesRequest)
                {
                    var valor = propRequest.GetValue(request._request);
                    if (valor != null)
                    {
                        // Buscar la propiedad correspondiente en ResponsableEntity
                        var propResponsable = propiedadesResponsable.FirstOrDefault(p => p.Name.Equals(propRequest.Name, StringComparison.OrdinalIgnoreCase));

                        if (propResponsable != null && propResponsable.CanWrite)
                        {
                            // Actualizar el valor de la propiedad en el Responsable
                            propResponsable.SetValue(Responsable, valor);
                        }
                    }
                }

                // Guardar cambios
                _dbContext.ChangeEntityState(Responsable, EntityState.Modified);
                await _dbContext.SaveEfContextChanges("APP");
                transaccion.Commit();


                if (request._request.acciones.Count() != 0)
                {
                    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    ///     Pregunto si las acciones existen
                    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                    foreach (var resp in request._request.acciones)
                    {
                        var acciones = _dbContext.Acciones.FirstOrDefault(c => c.Id == resp.Id);
                        if (acciones == null)
                        {
                            throw new InvalidOperationException("Registro fallido: el Responsable NO existe");
                        }
                        var command = new ActualizarAccionesCommand(resp);
                        var response = await _mediator.Send(command);
                    }
                }






                _logger.LogInformation("Responsable actualizado correctamente. ID: {ResponsableId}", Responsable.Id);
                return new IdResponsableResponse(Responsable.Id);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error AgregarOperarioHandler.HandleAsync. {Mensaje}", ex.Message);
                throw;
            }

        }
    }
}
