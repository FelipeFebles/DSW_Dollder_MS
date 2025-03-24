using DSW_ApiNoConformidades_Dollder_MS.Aplication.Queries.NoConformidades;
using DSW_ApiNoConformidades_Dollder_MS.Aplication.Responses.NoConformidad;
using DSW_ApiNoConformidades_Dollder_MS.Infrastructure.Database;
using MediatR;
using Microsoft.Extensions.Logging;


namespace DSW_ApiNoConformidades_Dollder_MS.Aplication.Handlers.Queries.NoConformidades
{
    public class BuscarNoConformidadHandler : IRequestHandler<BuscarNoConformidadQuery, NoConformidadesResponse>
    {
        private readonly ApiDbContext _dbContext;
        private readonly ILogger<BuscarNoConformidadHandler> _logger;
        public BuscarNoConformidadHandler(ApiDbContext dbContext, ILogger<BuscarNoConformidadHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public Task<NoConformidadesResponse> Handle(BuscarNoConformidadQuery request, CancellationToken cancellationToken)
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

        private async Task<NoConformidadesResponse> HandleAsync(BuscarNoConformidadQuery request)
        {
            try
            {
                _logger.LogInformation("ConsultarUsuarioIdQueryHandler.HandleAsync");

                // Crear una lista para almacenar los resultados
                var list = _dbContext.NoConformidad
                    .Where(x => x.Id == request._request.Data)
                    .Select(x => new NoConformidadesResponse
                    {
                        Id = x.Id,
                        CreatedAt = x.CreatedAt,
                        CreatedBy = x.CreatedBy,
                        UpdatedAt = x.UpdatedAt,
                        UpdatedBy = x.UpdatedBy,

                        reporte_Id = x.reporte_Id,
                        revisado_por = x.revisado_por,
                        numero_expedicion = x.numero_expedicion,
                        responsables_cargo = x.responsables_cargo,
                        areas_involucradas = x.areas_involucradas,
                        prioridad = x.prioridad,
                        titulo = x.reporte.titulo,
                        area = x.reporte.area,
                        estado = ((Estado)x.estado).ToString() ?? "Sin estado", 
                    })
                    .FirstOrDefault();


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
