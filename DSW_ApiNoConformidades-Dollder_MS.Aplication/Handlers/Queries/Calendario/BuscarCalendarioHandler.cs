using DSW_ApiNoConformidades_Dollder_MS.Aplication.Queries.Calendario;
using DSW_ApiNoConformidades_Dollder_MS.Aplication.Responses.Calendario;
using DSW_ApiNoConformidades_Dollder_MS.Infrastructure.Database;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DSW_ApiNoConformidades_Dollder_MS.Aplication.Handlers.Queries.Calendario
{
    internal class BuscarCalendarioHandler : IRequestHandler<BuscarCalendarioQuery, List<CalendarioResponse>>
    {
        private readonly ApiDbContext _dbContext;
        private readonly ILogger<BuscarCalendarioHandler> _logger;
        public BuscarCalendarioHandler(ApiDbContext dbContext, ILogger<BuscarCalendarioHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public Task<List<CalendarioResponse>> Handle(BuscarCalendarioQuery request, CancellationToken cancellationToken)
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

        private async Task<List<CalendarioResponse>> HandleAsync(BuscarCalendarioQuery request)
        {
            try
            {
                _logger.LogInformation("ConsultarUsuarioIdQueryHandler.HandleAsync");

                // Crear una lista para almacenar los resultados
                var list = _dbContext.Calendario.Where(x=> x.estado == true)
                    .Select(x => new CalendarioResponse
                    {
                        Id = x.Id,
                        CreatedAt = x.CreatedAt,
                        CreatedBy = x.CreatedBy,
                        UpdatedAt = x.UpdatedAt,
                        UpdatedBy = x.UpdatedBy,
                        color = x.color,
                        dia = x.dia,
                        mes = x.mes,
                        anio = x.anio,
                        titulo = x.titulo,
                        descripcion = x.descripcion,

                    })
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

