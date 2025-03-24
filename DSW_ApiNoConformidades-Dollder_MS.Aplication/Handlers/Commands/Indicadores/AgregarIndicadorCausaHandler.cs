using DSW_ApiNoConformidades_Dollder_MS.Aplication.Commands.Indicadores;
using DSW_ApiNoConformidades_Dollder_MS.Aplication.Mappers.Indicadores;
using DSW_ApiNoConformidades_Dollder_MS.Aplication.Responses.Indicadores;
using DSW_ApiNoConformidades_Dollder_MS.Infrastructure.Database;
using MediatR;
using Microsoft.Extensions.Logging;


namespace DSW_ApiNoConformidades_Dollder_MS.Aplication.Handlers.Commands.Indicadores
{
    public class AgregarIndicadorCausaHandler : IRequestHandler<AgregarIndicadorCausaCommand, IdIndicadoresResponse>
    {
        private readonly ApiDbContext _dbContext;
        private readonly ILogger<AgregarIndicadorCausaHandler> _logger;
        public AgregarIndicadorCausaHandler(ApiDbContext dbContext, ILogger<AgregarIndicadorCausaHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }


        public Task<IdIndicadoresResponse> Handle(AgregarIndicadorCausaCommand request, CancellationToken cancellationToken)
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

        private async Task<IdIndicadoresResponse> HandleAsync(AgregarIndicadorCausaCommand request)
        {
            var transaccion = _dbContext.BeginTransaction();
            try
            {

                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                ///     Reviso si el Indicador ya no esta generado
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                var view = _dbContext.IndicadorCausa.Count(n => n.Id == request._request.Id);

                if (view > 0) //Verifico que el Usuario exista
                {
                    throw new InvalidOperationException("El indicador ya fue registrado");
                }

                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                ///     Agrego el cierre
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                var response = IndicadoresMapper.MapRequestIndicadorCausaEntity(request._request);
                _dbContext.IndicadorCausa.Add(response);
                await _dbContext.SaveEfContextChanges("APP");

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
