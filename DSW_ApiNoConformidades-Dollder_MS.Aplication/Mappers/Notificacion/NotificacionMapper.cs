using DSW_ApiNoConformidades_Dollder_MS.Application.Requests.Notificacion;
using DSW_ApiNoConformidades_Dollder_MS.Core.Entities;

namespace DSW_ApiNoConformidades_Dollder_MS.Application.Mappers.Notificacion
{
    public class NotificacionMapper
    {
        public static NotificacionEntity MapRequestNotificacionEntity(NotificacionRequest request)
        {
            var entity = new NotificacionEntity()
            {
                 titulo = request.titulo,
                envia   = request.envia,
                dirigido = request.dirigido,
                mensaje = request.mensaje,
                revisado = request.revisado,
                tipo = request.tipo
            };
            return entity;
        }
    }
}
