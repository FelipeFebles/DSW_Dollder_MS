using DSW_ApiNoConformidades_Dollder_MS.Aplication.Commands.Cierre;
using DSW_ApiNoConformidades_Dollder_MS.Aplication.Commands.NoConformidad;
using DSW_ApiNoConformidades_Dollder_MS.Aplication.Mappers.Cierre;
using DSW_ApiNoConformidades_Dollder_MS.Aplication.Mappers.Indicadores;
using DSW_ApiNoConformidades_Dollder_MS.Aplication.Responses.Cierre;
using DSW_ApiNoConformidades_Dollder_MS.Application.Commands.Responsable;
using DSW_ApiNoConformidades_Dollder_MS.Application.Requests.NoConformidad;
using DSW_ApiNoConformidades_Dollder_MS.Infrastructure.Database;
using MassTransit.Mediator;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DSW_ApiNoConformidades_Dollder_MS.Aplication.Handlers.Commands.Cierre
{
    public class AgregarCierreHandler : IRequestHandler<AgregarCierreCommand, IdCierreResponse>
    {
        private readonly ApiDbContext _dbContext;
        private readonly ILogger<AgregarCierreHandler> _logger;
        private readonly MediatR.IMediator _mediator;

        public AgregarCierreHandler(ApiDbContext dbContext, ILogger<AgregarCierreHandler> logger, MediatR.IMediator mediator)
        {
            _dbContext = dbContext;
            _logger = logger;
            _mediator = mediator;
        }


        public Task<IdCierreResponse> Handle(AgregarCierreCommand request, CancellationToken cancellationToken)
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

        private async Task<IdCierreResponse> HandleAsync(AgregarCierreCommand request)
        {
            var transaccion = _dbContext.BeginTransaction();
            try
            {

                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                ///     Reviso si el Cierre ya no esta generado
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                var view = _dbContext.Cierre.Count(n => n.noConformidad_Id == request._request.noConformidad_Id);

                if (view > 0) //Verifico que el Usuario exista
                {
                    throw new InvalidOperationException("El reporte ya fue registrado");
                }

                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                ///     Agrego el cierre
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                var response = CierreMapper.MapRequestCierreEntity(request._request);
                _dbContext.Cierre.Add(response);
                await _dbContext.SaveEfContextChanges("APP");

                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                ///     Agrego los indicadores
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                
                request._request.indicadores.cierre_Id = response.Id;
                var response2 = IndicadoresMapper.MapRequestIndicadoresEntity(request._request.indicadores);
                _dbContext.Indicadores.Add(response2);
                await _dbContext.SaveEfContextChanges("APP");
                transaccion.Commit();


                //Cambio el estado de la NC
                var command = new ActualizarEstadoNoConformidadCommand(new NoConformidadRequest { Id = request._request.noConformidad_Id , estado= "Cerrada" });
                var result = await _mediator.Send(command);



                return new IdCierreResponse(response.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error AgregarOperarioHandler.HandleAsync. {Mensaje}", ex.Message);
                throw;
            }

        }
        
    }
}
