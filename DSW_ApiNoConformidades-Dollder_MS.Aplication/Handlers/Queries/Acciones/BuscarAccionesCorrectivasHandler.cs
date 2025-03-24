using DSW_ApiNoConformidades_Dollder_MS.Aplication.Queries.Acciones;
using DSW_ApiNoConformidades_Dollder_MS.Aplication.Responses.Acciones;
using DSW_ApiNoConformidades_Dollder_MS.Infrastructure.Database;
using MediatR;
using Microsoft.Extensions.Logging;


namespace DSW_ApiNoConformidades_Dollder_MS.Aplication.Handlers.Queries.Acciones
{

    public class BuscarAccionesCorrectivasHandler: IRequestHandler<BuscarAccionesCorrectivasQuery, List<AccionesResponse>>
    {
        private readonly ApiDbContext _dbContext;
        private readonly ILogger<BuscarAccionesCorrectivasHandler> _logger;
        public BuscarAccionesCorrectivasHandler(ApiDbContext dbContext, ILogger<BuscarAccionesCorrectivasHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public Task<List<AccionesResponse>> Handle(BuscarAccionesCorrectivasQuery request, CancellationToken cancellationToken)
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

        private async Task<List<AccionesResponse>> HandleAsync(BuscarAccionesCorrectivasQuery request)
        {
            try
            {
                _logger.LogInformation("ConsultarUsuarioIdQueryHandler.HandleAsync");
                var list = new List<AccionesResponse>();
                // Crear una lista para almacenar los resultados
                var relacion = _dbContext.R_Acciones_Usuario.Where(c => c.usuario_Id == request._request.data).ToList();
                foreach (var acciones in relacion) 
                {
                    var accion = _dbContext.Correctivas.Where(c => c.Id == acciones.acciones_Id).FirstOrDefault();
                    list.Add(new AccionesResponse
                    {
                        Id = accion.Id,
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
