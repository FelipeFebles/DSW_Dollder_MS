using DSW_ApiNoConformidades_Dollder_MS.Aplication.Requests.VerificacionEfectividad;
using DSW_ApiNoConformidades_Dollder_MS.Core.Entities;


namespace DSW_ApiNoConformidades_Dollder_MS.Aplication.Mappers.VerificacionEfectividad
{
    public class VerificacionEfectividadMapper
    {
        public static VerificacionEfectividadEntity MapRequestSeguimientoEntity(VerificacionEfectividadRequest request)
        {
            var entity = new VerificacionEfectividadEntity()
            {
                efectiva = request.efectiva,
                observaciones = request.observaciones,
                realizado_por = request.realizado_por,
                cierre_Id = (Guid)request.cierre_Id,
            };
            return entity;
        }
    }
}
