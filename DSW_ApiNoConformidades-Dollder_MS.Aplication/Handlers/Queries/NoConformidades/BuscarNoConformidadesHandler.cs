using DSW_ApiNoConformidades_Dollder_MS.Aplication.Queries.NoConformidades;
using DSW_ApiNoConformidades_Dollder_MS.Aplication.Responses.NoConformidad;
using DSW_ApiNoConformidades_Dollder_MS.Infrastructure.Database;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DSW_ApiNoConformidades_Dollder_MS.Aplication.Handlers.Queries.NoConformidades
{
    public class BuscarNoConformidadesHandler : IRequestHandler<BuscarNoConformidadesQuery, List<NoConformidadesResponse>>
    {
        private readonly ApiDbContext _dbContext;
        private readonly ILogger<BuscarNoConformidadesHandler> _logger;
        public BuscarNoConformidadesHandler(ApiDbContext dbContext, ILogger<BuscarNoConformidadesHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public Task<List<NoConformidadesResponse>> Handle(BuscarNoConformidadesQuery request, CancellationToken cancellationToken)
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

        private async Task<List<NoConformidadesResponse>> HandleAsync(BuscarNoConformidadesQuery request)
        {
            try
            {
                _logger.LogInformation("ConsultarUsuarioIdQueryHandler.HandleAsync");

                // Crear una lista para almacenar los resultados
                var list = _dbContext.NoConformidad
                    .Select(x => new NoConformidadesResponse
                    {
                        Id = x.Id,
                        CreatedAt = x.CreatedAt,

                        reporte_Id = x.reporte_Id,
                        revisado_por = x.revisado_por,
                        numero_expedicion = x.numero_expedicion,
                        responsables_cargo = x.responsables_cargo,
                        areas_involucradas = x.areas_involucradas,
                        prioridad = x.prioridad,
                        titulo = x.reporte.titulo,
                        area = x.reporte.area,
                        estado = ((Estado)x.estado).ToString() ?? "Sin estado", // Conversión directa
                    }).OrderBy(x=> x.numero_expedicion)
                    .ToList();


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
