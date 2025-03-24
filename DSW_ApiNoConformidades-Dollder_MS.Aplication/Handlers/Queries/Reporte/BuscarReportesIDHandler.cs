using DSW_ApiNoConformidades_Dollder_MS.Aplication.Queries.Reporte;
using DSW_ApiNoConformidades_Dollder_MS.Aplication.Responses.Reportes;
using DSW_ApiNoConformidades_Dollder_MS.Infrastructure.Database;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DSW_ApiNoConformidades_Dollder_MS.Aplication.Handlers.Queries.Reporte
{
    public class BuscarReportesHandler : IRequestHandler<BuscarReportesIDQuery, List<ReporteResponse>>
    {
        private readonly ApiDbContext _dbContext;
        private readonly ILogger<BuscarReportesHandler> _logger;
        public BuscarReportesHandler(ApiDbContext dbContext, ILogger<BuscarReportesHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public Task<List<ReporteResponse>> Handle(BuscarReportesIDQuery request, CancellationToken cancellationToken)
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

        private async Task<List<ReporteResponse>> HandleAsync(BuscarReportesIDQuery request)
        {
            try
            {
                _logger.LogInformation("ConsultarUsuarioIdQueryHandler.HandleAsync");
                var usuario = _dbContext.Usuario.Where(c=> c.Id==request._request.data).FirstOrDefault();
                var dep = _dbContext.Departamento.Where(c => c.Id == usuario.departamento_Id).FirstOrDefault();
                // Crear una lista para almacenar los resultados
                var list = _dbContext.Reporte.Where(c => c.departamento_emisor == dep.nombre).Select(c => new ReporteResponse
                {
                    Id = c.Id,
                    CreatedAt = c.CreatedAt,

                    departamento_emisor = c.departamento_emisor,
                    id_usuario = c.usuario_Id,
                    detectado_por = c.detectado_por,
                    area = c.area,
                    titulo = c.titulo,
                    descripcion = c.descripcion,
                    estado = c.estado
                    
                }).OrderBy(c => c.CreatedAt).ToList();

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
