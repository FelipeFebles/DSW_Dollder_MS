using DSW_ApiNoConformidades_Dollder_MS.Aplication.Queries.Reporte;
using DSW_ApiNoConformidades_Dollder_MS.Aplication.Responses.Reportes;
using DSW_ApiNoConformidades_Dollder_MS.Infrastructure.Database;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DSW_ApiNoConformidades_Dollder_MS.Aplication.Handlers.Queries.Reporte
{
    public class BuscarReporteHandler : IRequestHandler<BuscarReporteQuery, ReporteResponse>
    {
        private readonly ApiDbContext _dbContext;
        private readonly ILogger<BuscarReporteHandler> _logger;
        public BuscarReporteHandler(ApiDbContext dbContext, ILogger<BuscarReporteHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public Task<ReporteResponse> Handle(BuscarReporteQuery request, CancellationToken cancellationToken)
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

        private async Task<ReporteResponse> HandleAsync(BuscarReporteQuery request)
        {
            try
            {
                // Crear una lista para almacenar los resultados
                var resp = _dbContext.Reporte
                    .Where(c => c.Id == request._request.Id)
                    .Select(c => new ReporteResponse
                    {
                        id_usuario = c.usuario_Id,
                        Id = c.Id,
                        detectado_por = c.detectado_por,
                        area = c.area,
                        titulo = c.titulo,
                        descripcion = c.descripcion,
                        estado = c.estado
                    }).FirstOrDefault();

                if (_dbContext.ImagenReporte.Where(x => x.reporte_Id == request._request.Id).Count() > 0) 
                {
                    foreach (var img in _dbContext.ImagenReporte.Where(x => x.reporte_Id == request._request.Id)) 
                    {
                        var imagen = new ImagenReporteResponse
                        {
                            imagen = img.imagen,
                            nombre_archivo = img.nombre_archivo
                        };
                        resp.imagenes.Add(imagen);
                    }
                }

                // Retornar la lista de no conformidades
                return resp;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en ConsultarUsuarioIdQueryHandler.HandleAsync");
                throw; // Relanzar la excepción para que sea manejada en un nivel superior
            }
        }
    }
}
