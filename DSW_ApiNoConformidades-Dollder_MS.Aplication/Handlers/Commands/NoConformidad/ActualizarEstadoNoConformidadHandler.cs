using DSW_ApiNoConformidades_Dollder_MS.Aplication.Commands.NoConformidad;
using DSW_ApiNoConformidades_Dollder_MS.Aplication.Queries.NoConformidades;
using DSW_ApiNoConformidades_Dollder_MS.Aplication.Requests.NoConformidad;
using DSW_ApiNoConformidades_Dollder_MS.Application.Responses.NoConformidad;
using DSW_ApiNoConformidades_Dollder_MS.Infrastructure.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DSW_ApiNoConformidades_Dollder_MS.Aplication.Handlers.Commands.NoConformidad
{
    public class ActualizarEstadoNoConformidadHandler : IRequestHandler<ActualizarEstadoNoConformidadCommand, IdNoConformidadResponse>
    {
        private readonly ApiDbContext _dbContext;
        private readonly ILogger<ActualizarEstadoNoConformidadHandler> _logger;
        private readonly MediatR.IMediator _mediator;
        public ActualizarEstadoNoConformidadHandler(ApiDbContext dbContext, ILogger<ActualizarEstadoNoConformidadHandler> logger, MediatR.IMediator mediator)
        {
            _dbContext = dbContext;
            _logger = logger;
            _mediator = mediator;
        }

        public Task<IdNoConformidadResponse> Handle(ActualizarEstadoNoConformidadCommand request, CancellationToken cancellationToken)
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

        private async Task<IdNoConformidadResponse> HandleAsync(ActualizarEstadoNoConformidadCommand request)
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
                ///     Traigo dinamicamente los campos del reporte
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                var query = new BuscarNoConformidadCompletaQuery(new IdNoConformidadRequest {Data= (Guid)request._request.Id });
                var response = await _mediator.Send(query);

                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                ///     Empiezo a preguntar para actualizar la NoConformidad
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                if (request._request.estado !=null) 
                {
                    NoConformidad.estado = Enum.TryParse<Estado>(request._request.estado, out var estado) ? estado : Estado.En_Proceso; // Asignar el valor del enum
                }

                // Guardar cambios
                _dbContext.ChangeEntityState(NoConformidad, EntityState.Modified);
                await _dbContext.SaveEfContextChanges("APP");
                transaccion.Commit();

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
