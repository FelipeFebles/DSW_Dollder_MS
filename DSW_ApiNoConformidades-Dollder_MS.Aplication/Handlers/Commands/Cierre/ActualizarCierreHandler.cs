using DSW_ApiNoConformidades_Dollder_MS.Aplication.Commands.Cierre;
using DSW_ApiNoConformidades_Dollder_MS.Aplication.Requests.Cierre;
using DSW_ApiNoConformidades_Dollder_MS.Aplication.Requests.Indicadores;
using DSW_ApiNoConformidades_Dollder_MS.Aplication.Responses.Cierre;
using DSW_ApiNoConformidades_Dollder_MS.Application.Commands.Responsable;
using DSW_ApiNoConformidades_Dollder_MS.Core.Entities;
using DSW_ApiNoConformidades_Dollder_MS.Infrastructure.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DSW_ApiCierrees_Dollder_MS.Aplication.Handlers.Commands.Cierre
{
    public class ActualizarCierreHandler : IRequestHandler<ActualizarCierreCommand, IdCierreResponse>
    {
        private readonly MediatR.IMediator _mediator;
        private readonly ApiDbContext _dbContext;
        private readonly ILogger<ActualizarCierreHandler> _logger;
        public ActualizarCierreHandler(ApiDbContext dbContext, ILogger<ActualizarCierreHandler> logger, MediatR.IMediator mediator)
        {
            _dbContext = dbContext;
            _logger = logger;
            _mediator = mediator;
        }

        public Task<IdCierreResponse> Handle(ActualizarCierreCommand request, CancellationToken cancellationToken)
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

        private async Task<IdCierreResponse> HandleAsync(ActualizarCierreCommand request)
        {
            var transaccion = _dbContext.BeginTransaction();
            try
            {
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                ///     Pregunto si el Cierre existe
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                var Cierre = _dbContext.Cierre.FirstOrDefault(c => c.Id == request._request.Id);
                if (Cierre == null)
                {
                    throw new InvalidOperationException("Registro fallido: el Cierre NO existe");
                }
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                ///     Actualizo el Cierre
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                // Obtener las propiedades de CierreEntity y CierreRequest
                var propiedadesCierre = typeof(CierreEntity).GetProperties();
                var propiedadesRequest = typeof(CierreRequest).GetProperties();

                // Actualizar propiedades solo si no están vacías o nulas
                foreach (var propRequest in propiedadesRequest)
                {
                    var valor = propRequest.GetValue(request._request);
                    if (valor != null)
                    {
                        // Buscar la propiedad correspondiente en CierreEntity
                        var propCierre = propiedadesCierre.FirstOrDefault(p => p.Name.Equals(propRequest.Name, StringComparison.OrdinalIgnoreCase));

                        if (propCierre != null && propCierre.CanWrite)
                        {
                            // Actualizar el valor de la propiedad en el Cierre
                            propCierre.SetValue(Cierre, valor);
                        }
                    }
                }

                // Guardar cambios
                _dbContext.ChangeEntityState(Cierre, EntityState.Modified);
                await _dbContext.SaveEfContextChanges("APP");

                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                ///     Actualizo el indicador
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                
                var Indicador = _dbContext.Indicadores.FirstOrDefault(c => c.cierre_Id == request._request.Id);
                // Obtener las propiedades de IndicadoresEntity y IndicadoresRequest
                var propiedadesIndicador = typeof(IndicadoresEntity).GetProperties();
                var propiedadesRequest2 = typeof(IndicadoresRequest).GetProperties();

                // Actualizar propiedades solo si no están vacías o nulas
                foreach (var propRequest in propiedadesRequest2)
                {
                    var valor = propRequest.GetValue(request._request.indicadores);
                    if (valor != null)
                    {
                        // Buscar la propiedad correspondiente en IndicadoresEntity
                        var prop = propiedadesIndicador.FirstOrDefault(p => p.Name.Equals(propRequest.Name, StringComparison.OrdinalIgnoreCase));

                        if (prop != null && prop.CanWrite)
                        {
                            // Actualizar el valor de la propiedad en el Indicadores
                            prop.SetValue(Indicador, valor);
                        }
                    }
                }


                //if (request._request.verificacionEfectividad != null)
                //{
                //    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                //    ///     Pregunto si la verificacionEfectividad existe
                //    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                //    var verificacionEfectividad = _dbContext.VerificacionEfectividad.FirstOrDefault(c => c.Id == request._request.verificacionEfectividad.Id);
                //    if (verificacionEfectividad == null)
                //    {
                //        throw new InvalidOperationException("Registro fallido: el Indicadores NO existe");
                //    }
                //    var command = new ActualizarVerificacionEfectividadCommand(request._request.verificacionEfectividad);
                //    var response = await _mediator.Send(command);
                //}




                _logger.LogInformation("Cierre actualizado correctamente. ID: {CierreId}", Cierre.Id);
                return new IdCierreResponse(Cierre.Id);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error AgregarOperarioHandler.HandleAsync. {Mensaje}", ex.Message);
                throw;
            }

        }
    }
}
