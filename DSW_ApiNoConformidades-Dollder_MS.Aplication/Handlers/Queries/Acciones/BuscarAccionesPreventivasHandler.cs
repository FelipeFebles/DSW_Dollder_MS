using DSW_ApiNoConformidades_Dollder_MS.Aplication.Queries.Acciones;
using DSW_ApiNoConformidades_Dollder_MS.Aplication.Responses.Acciones;
using DSW_ApiNoConformidades_Dollder_MS.Infrastructure.Database;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DSW_ApiNoConformidades_Dollder_MS.Aplication.Handlers.Queries.Acciones
{
    public class BuscarAccionesPreventivasHandler : IRequestHandler<BuscarIdAccionesPreventivasQuery, List<AccionesResponse>>
    {
        private readonly ApiDbContext _dbContext;
        private readonly ILogger<BuscarAccionesPreventivasHandler> _logger;
        public BuscarAccionesPreventivasHandler(ApiDbContext dbContext, ILogger<BuscarAccionesPreventivasHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public Task<List<AccionesResponse>> Handle(BuscarIdAccionesPreventivasQuery request, CancellationToken cancellationToken)
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

        private async Task<List<AccionesResponse>> HandleAsync(BuscarIdAccionesPreventivasQuery request)
        {
            try
            {
                _logger.LogInformation("ConsultarUsuarioIdQueryHandler.HandleAsync");
                var list = new List<AccionesResponse>();
                // Crear una lista para almacenar los resultados
                var relacion = _dbContext.R_Acciones_Usuario.Where(c => c.usuario_Id == request._request.data).ToList();
                foreach (var acciones in relacion)
                {
                    var accion = _dbContext.Preventivas.Where(c => c.Id == acciones.acciones_Id).FirstOrDefault();
                    if(accion != null)
                        list.Add(new AccionesResponse
                        {
                            Id = accion.Id,
                            correctivas_Id = accion.correctiva_Id,
                            CreatedAt = accion.CreatedAt,
                            fecha_compromiso = accion.fecha_compromiso,
                            responsable_Id = accion.responsable_Id,
                            investigacion = accion.investigacion,
                            area = accion.area,
                            estado = accion.estado,
                            visto_bueno = accion.visto_bueno,
                            cargo_usuario = accion.cargo_usuario
                        });
                }

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