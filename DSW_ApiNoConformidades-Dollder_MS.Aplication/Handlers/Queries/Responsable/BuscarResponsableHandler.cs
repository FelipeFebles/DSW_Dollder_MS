using DSW_ApiNoConformidades_Dollder_MS.Aplication.Queries.Responsable;
using DSW_ApiNoConformidades_Dollder_MS.Aplication.Responses.Responsable;
using DSW_ApiNoConformidades_Dollder_MS.Infrastructure.Database;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DSW_ApiNoConformidades_Dollder_MS.Aplication.Handlers.Queries.Responsable
{
    public class BuscarResponsableHandler : IRequestHandler<BuscarResponsableQuery, List<ResponsableResponse>>
    {
        private readonly ApiDbContext _dbContext;
        private readonly ILogger<BuscarResponsableHandler> _logger;
        public BuscarResponsableHandler(ApiDbContext dbContext, ILogger<BuscarResponsableHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public Task<List<ResponsableResponse>> Handle(BuscarResponsableQuery request, CancellationToken cancellationToken)
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

        private async Task<List<ResponsableResponse>> HandleAsync(BuscarResponsableQuery request)
        {
            try
            {
                _logger.LogInformation("ConsultarUsuarioIdQueryHandler.HandleAsync");

                // Crear una lista para almacenar los resultados
                var list = _dbContext.Responsable.Where(c => c.usuario_Id == request._request.data).Select(c => new ResponsableResponse
                {
                    Id = c.Id,
                    CreatedAt= c.CreatedAt,
                    estado = c.estado,
                    investigacion = c.investigacion,
                    analisis_causa = c.analisis_causa,
                    correccion = c.correccion,
                    analisis_riesgo = c.analisis_riesgo,

                    fecha_compromiso = c.fecha_compromiso,
                    nombre_usuario = c.usuario.nombre,
                    apellido_usuario = c.usuario.apellido,
                    cargo_usuario = c.cargo_responsable,
                    usuario_Id = c.usuario_Id,
                    noConformidad_Id = c.noConformidad_Id
                }).ToList();


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
