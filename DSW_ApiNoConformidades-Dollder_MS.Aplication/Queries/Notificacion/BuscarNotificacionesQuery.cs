using DSW_ApiNoConformidades_Dollder_MS.Aplication.Responses.Notificaciones;
using DSW_ApiNoConformidades_Dollder_MS.Application.Requests;
using MediatR;

namespace DSW_ApiNoConformidades_Dollder_MS.Aplication.Queries.Notificaciones
{
    public class BuscarNotificacionesQuery : IRequest<List<NotificacionesResponse>>
    {
        public BuscarUsuarioIDRequest _request { get; set; }
        public BuscarNotificacionesQuery(BuscarUsuarioIDRequest request)
        {
            _request = request;
        }
    }
}
