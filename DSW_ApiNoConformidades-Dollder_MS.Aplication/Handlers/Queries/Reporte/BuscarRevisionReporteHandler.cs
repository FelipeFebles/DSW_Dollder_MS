using DSW_ApiNoConformidades_Dollder_MS.Aplication.Queries.Reporte;
using DSW_ApiNoConformidades_Dollder_MS.Aplication.Responses.RevisionReporte;
using DSW_ApiNoConformidades_Dollder_MS.Infrastructure.Database;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DSW_ApiNoConformidades_Dollder_MS.Aplication.Handlers.Queries.Reporte
{
    public class BuscarRevisionReporteHandler : IRequestHandler<BuscarRevisionReporteQuery, RevisionReporteResponse>
    {
        private readonly ApiDbContext _dbContext;
        private readonly ILogger<BuscarRevisionReporteHandler> _logger;
        public BuscarRevisionReporteHandler(ApiDbContext dbContext, ILogger<BuscarRevisionReporteHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public Task<RevisionReporteResponse> Handle(BuscarRevisionReporteQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (request is null) //Pregunto si el request es nulo
                {
                    _logger.LogWarning("ConsultarUsuarioIdQueryHandler.Handle: Request nulo.");
                    throw new ArgumentNullException(nameof(request));

                }
                else
                {
                    return HandleAsync(request);
                }
            }
            catch (Exception)
            {
                _logger.LogWarning("ConsultarUsuariosQueryHandler.Handle: ArgumentNullException");
                throw;
            }
        }

        private async Task<RevisionReporteResponse> HandleAsync(BuscarRevisionReporteQuery request)
        {
            try
            {
                var list = _dbContext.RevisionReporte.Where(c => c.usuario_Id == request._request.id_usuario    &&    c.reporte_Id == request._request.id_reporte ).Select(c => new RevisionReporteResponse
                {
                    Id = c.Id,
                    id_reporte = c.Id,
                    nombre = c.nombre,
                }).FirstOrDefault();


                // Retornar la lista de no conformidades
                return list;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en ConsultarUsuarioIdQueryHandler.HandleAsync");
                throw; // Relanzar la excepción para que sea manejada en un nivel superior
            }
        }
    }
}