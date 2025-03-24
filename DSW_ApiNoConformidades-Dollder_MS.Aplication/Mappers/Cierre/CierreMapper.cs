using DSW_ApiNoConformidades_Dollder_MS.Aplication.Requests.Cierre;
using DSW_ApiNoConformidades_Dollder_MS.Core.Entities;

namespace DSW_ApiNoConformidades_Dollder_MS.Aplication.Mappers.Cierre
{
    public class CierreMapper
    {
        public static CierreEntity MapRequestCierreEntity(CierreRequest request)
        {
            var entity = new CierreEntity()
            {
                conforme = request.conforme,
                observaciones = request.observaciones,
                responsable = request.responsable,
                fecha_verificacion = request.fecha_verificacion,
                noConformidad_Id = (Guid)request.noConformidad_Id,

            };
            return entity;
        }
    }
}
