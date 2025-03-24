using DSW_ApiNoConformidades_Dollder_MS.Aplication.Commands.Indicadores;
using DSW_ApiNoConformidades_Dollder_MS.Aplication.Mappers.Indicadores;
using DSW_ApiNoConformidades_Dollder_MS.Aplication.Responses.Indicadores;
using DSW_ApiNoConformidades_Dollder_MS.Infrastructure.Database;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DSW_ApiNoConformidades_Dollder_MS.Aplication.Handlers.Commands.Indicadores
{
    public class AgregarIndicadoresHandler : IRequestHandler<AgregarIndicadoresCommand, IdIndicadoresResponse>
    {
        private readonly ApiDbContext _dbContext;
        private readonly ILogger<AgregarIndicadoresHandler> _logger;
        public AgregarIndicadoresHandler(ApiDbContext dbContext, ILogger<AgregarIndicadoresHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }


        public Task<IdIndicadoresResponse> Handle(AgregarIndicadoresCommand request, CancellationToken cancellationToken)
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

        private async Task<IdIndicadoresResponse> HandleAsync(AgregarIndicadoresCommand request)
        {
            var transaccion = _dbContext.BeginTransaction();
            try
            {

                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                ///     Reviso si el Indicador ya no esta generado
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                var view = _dbContext.Indicadores.Count(n => n.cierre_Id == request._request.cierre_Id);

                if (view > 0) //Verifico que el Usuario exista
                {
                    throw new InvalidOperationException("El indicador ya fue registrado");
                }

                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                ///     Agrego el cierre
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                var response = IndicadoresMapper.MapRequestIndicadoresEntity(request._request);
                _dbContext.Indicadores.Add(response);
                await _dbContext.SaveEfContextChanges("APP");

                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                ///     Agrego la relacion con IndicadorCausa
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                foreach (var item in request._request.causa)
                {
                    var response2 = IndicadoresMapper.MapRequestR_Indicadores_CausasEntity((Guid)item.Id,(Guid)response.Id);
                    _dbContext.R_Indicadores_Causas.Add(response2);
                    await _dbContext.SaveEfContextChanges("APP");
                }

                transaccion.Commit();

                return new IdIndicadoresResponse(response.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error AgregarOperarioHandler.HandleAsync. {Mensaje}", ex.Message);
                throw;
            }

        }

    }
}

