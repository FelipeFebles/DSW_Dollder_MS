using DSW_ApiNoConformidades_Dollder_MS.Aplication.Requests.Seguimiento;
using DSW_ApiNoConformidades_Dollder_MS.Core.Entities;

namespace DSW_ApiNoConformidades_Dollder_MS.Aplication.Mappers.Seguimiento
{
    public class SeguimientoMapper
    {
        public static SeguimientoEntity MapRequestSeguimientoEntity(SeguimientoRequest request)
        {
            var entity = new SeguimientoEntity()
            {
                fecha_seguimiento = request.fecha_seguimiento,
                estatus = request.estatus,
                observaciones = request.observaciones,
                realizado_por = request.realizado_por,
                noConformidad_Id = (Guid)request.noConformidad_Id,
            };
            return entity;
        }
    }
}
