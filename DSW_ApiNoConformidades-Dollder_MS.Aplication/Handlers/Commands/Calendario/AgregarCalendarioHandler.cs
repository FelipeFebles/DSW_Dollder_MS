using DSW_ApiNoConformidades_Dollder_MS.Aplication.Commands.Calendario;
using DSW_ApiNoConformidades_Dollder_MS.Aplication.Mappers.Calendario;
using DSW_ApiNoConformidades_Dollder_MS.Aplication.Mappers.Indicadores;
using DSW_ApiNoConformidades_Dollder_MS.Aplication.Responses.Calendario;
using DSW_ApiNoConformidades_Dollder_MS.Infrastructure.Database;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DSW_ApiNoConformidades_Dollder_MS.Aplication.Handlers.Commands.Calendario
{
    public class AgregarCalendarioHandler : IRequestHandler<AgregarCalendarioCommand, IdCalendarioResponse>
    {
        private readonly ApiDbContext _dbContext;
        private readonly ILogger<AgregarCalendarioHandler> _logger;
        public AgregarCalendarioHandler(ApiDbContext dbContext, ILogger<AgregarCalendarioHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }


        public Task<IdCalendarioResponse> Handle(AgregarCalendarioCommand request, CancellationToken cancellationToken)
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

        private async Task<IdCalendarioResponse> HandleAsync(AgregarCalendarioCommand request)
        {
            var transaccion = _dbContext.BeginTransaction();
            try
            {
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                ///     Agrego la fecha
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                var response = CalendarioMapper.MapCalendarioEntity(request._request);
                _dbContext.Calendario.Add(response);
                await _dbContext.SaveEfContextChanges("APP");

                transaccion.Commit();

                return new IdCalendarioResponse(response.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error AgregarOperarioHandler.HandleAsync. {Mensaje}", ex.Message);
                throw;
            }

        }
    }
}
