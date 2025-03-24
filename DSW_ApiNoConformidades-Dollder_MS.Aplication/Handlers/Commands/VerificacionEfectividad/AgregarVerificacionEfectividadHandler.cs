using DSW_ApiNoConformidades_Dollder_MS.Aplication.Commands.Seguimiento;
using DSW_ApiNoConformidades_Dollder_MS.Aplication.Commands.VerificacionEfectividad;
using DSW_ApiNoConformidades_Dollder_MS.Aplication.Handlers.Commands.Seguimiento;
using DSW_ApiNoConformidades_Dollder_MS.Aplication.Mappers.Calendario;
using DSW_ApiNoConformidades_Dollder_MS.Aplication.Mappers.Seguimiento;
using DSW_ApiNoConformidades_Dollder_MS.Aplication.Mappers.VerificacionEfectividad;
using DSW_ApiNoConformidades_Dollder_MS.Aplication.Responses.Seguimiento;
using DSW_ApiNoConformidades_Dollder_MS.Aplication.Responses.VerificacionEfectividad;
using DSW_ApiNoConformidades_Dollder_MS.Infrastructure.Database;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSW_ApiNoConformidades_Dollder_MS.Aplication.Handlers.Commands.VerificacionEfectividad
{
    public class AgregarVerificacionEfectividadHandler : IRequestHandler<AgregarVerificacionEfectividadCommand, IdVerificacionEfectividadResponse>
    {
        private readonly ApiDbContext _dbContext;
        private readonly ILogger<AgregarVerificacionEfectividadHandler> _logger;
        public AgregarVerificacionEfectividadHandler(ApiDbContext dbContext, ILogger<AgregarVerificacionEfectividadHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public Task<IdVerificacionEfectividadResponse> Handle(AgregarVerificacionEfectividadCommand request, CancellationToken cancellationToken)
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

        private async Task<IdVerificacionEfectividadResponse> HandleAsync(AgregarVerificacionEfectividadCommand request)
        {
            var transaccion = _dbContext.BeginTransaction();
            try
            {

                //Agrego la notificacion del calendario
                var efectividad = VerificacionEfectividadMapper.MapRequestSeguimientoEntity(request._request);

                _dbContext.VerificacionEfectividad.Add(efectividad);
                await _dbContext.SaveEfContextChanges("APP");

                transaccion.Commit();
                //Retorno ID
                return new IdVerificacionEfectividadResponse(efectividad.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error AgregarOperarioHandler.HandleAsync. {Mensaje}", ex.Message);
                throw;
            }

        }
    }
}
