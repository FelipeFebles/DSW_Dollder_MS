using DSW_ApiNoConformidades_Dollder_MS.Aplication.Queries.Notificaciones;
using DSW_ApiNoConformidades_Dollder_MS.Aplication.Responses.Notificaciones;
using DSW_ApiNoConformidades_Dollder_MS.Application.Responses.Usuarios;
using DSW_ApiNoConformidades_Dollder_MS.Infrastructure.Database;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DSW_ApiNoConformidades_Dollder_MS.Aplication.Handlers.Queries.Notificacion
{
    public class BuscarNotificacionesUsuarioHandler : IRequestHandler<BuscarNotificacionesQuery, List<NotificacionesResponse>>
    {
        private readonly ApiDbContext _dbContext;
        private readonly ILogger<BuscarNotificacionesUsuarioHandler> _logger;
        public BuscarNotificacionesUsuarioHandler(ApiDbContext dbContext, ILogger<BuscarNotificacionesUsuarioHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public Task<List<NotificacionesResponse>> Handle(BuscarNotificacionesQuery request, CancellationToken cancellationToken)
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

        private async Task<List<NotificacionesResponse>> HandleAsync(BuscarNotificacionesQuery request)
        {

            try
            {
                _logger.LogInformation("ConsultarUsuarioIdQueryHandler.HandleAsync");
                var result = _dbContext.Usuario.Count(c => c.Id == request._request.data);

                if (result == 0) //Verifico que el Usuario exista
                {
                    throw new InvalidOperationException("No se encontro al usuario registrado");
                }

                // Realizar una consulta que una Usuario y Departamento
                var usuario = _dbContext.Usuario.Where(c => c.Id == request._request.data)// Filtra por ID
                    .Select(c => new UsuarioResponse // Rellena el response
                    {
                        correo = c.correo,
                    }).FirstOrDefault();

                var noti = _dbContext.Notificacion.Where(n=> n.dirigido == usuario.correo)
                                                  .Select(c => new NotificacionesResponse
                                                         {
                                                              titulo = c.titulo,
                                                              envia = c.envia,
                                                              dirigido = c.dirigido,
                                                              mensaje = c.mensaje,
                                                              revisado = c.revisado,
                                                              tipo = c.tipo
                                                  } ).ToList();

                return noti; //Retorno la lista

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error ConsultarUsuarioIdQueryHandler.HandleAsync. {Mensaje}", ex.Message);
                throw;
            }



        }
    }
}
