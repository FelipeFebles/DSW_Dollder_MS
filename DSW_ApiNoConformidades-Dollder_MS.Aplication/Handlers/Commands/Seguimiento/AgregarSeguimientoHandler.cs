using DSW_ApiNoConformidades_Dollder_MS.Aplication.Commands.Seguimiento;
using DSW_ApiNoConformidades_Dollder_MS.Aplication.Mappers.Calendario;
using DSW_ApiNoConformidades_Dollder_MS.Aplication.Mappers.Seguimiento;
using DSW_ApiNoConformidades_Dollder_MS.Aplication.Responses.Seguimiento;
using DSW_ApiNoConformidades_Dollder_MS.Infrastructure.Database;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DSW_ApiNoConformidades_Dollder_MS.Aplication.Handlers.Commands.Seguimiento
{
    public class AgregarSeguimientoHandler : IRequestHandler<AgregarSeguimientoCommand, IdSeguimientoReponse>
    {
        private readonly ApiDbContext _dbContext;
        private readonly ILogger<AgregarSeguimientoHandler> _logger;
        public AgregarSeguimientoHandler(ApiDbContext dbContext, ILogger<AgregarSeguimientoHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public Task<IdSeguimientoReponse> Handle(AgregarSeguimientoCommand request, CancellationToken cancellationToken)
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

        private async Task<IdSeguimientoReponse> HandleAsync(AgregarSeguimientoCommand request)
        {
            var transaccion = _dbContext.BeginTransaction();
            try
            {
                // Crear una instancia de Responsable con los datos del request
                var entity = SeguimientoMapper.MapRequestSeguimientoEntity(request._request);

                // Agregar el usuario al DbContext
                _dbContext.Seguimiento.Add(entity);
                await _dbContext.SaveEfContextChanges("APP");


                //Agrego la notificacion del calendario
                var calendario = CalendarioMapper.MapCalendarioEntity(request._request.fecha_seguimiento);

                _dbContext.Calendario.Add(calendario);
                await _dbContext.SaveEfContextChanges("APP");

                transaccion.Commit();
                //Retorno ID
                return new IdSeguimientoReponse(entity.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error AgregarOperarioHandler.HandleAsync. {Mensaje}", ex.Message);
                throw;
            }

        }
    }
}
