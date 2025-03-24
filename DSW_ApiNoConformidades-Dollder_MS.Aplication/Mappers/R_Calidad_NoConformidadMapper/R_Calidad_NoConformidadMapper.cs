using DSW_ApiNoConformidades_Dollder_MS.Core.Entities.Relaciones;


namespace DSW_ApiNoConformidades_Dollder_MS.Application.Mappers.R_Calidad_NoConformidadMapper
{
    public class R_Calidad_NoConformidadMapper
    {
        public static R_Calidad_NoConformidadEntity MapR_Calidad_NoConformidadEntity(Guid calidad, Guid noConformidad)
        {
            var entity = new R_Calidad_NoConformidadEntity()
            {
                calidad_Id = calidad,
                noConformidad_Id = noConformidad
            };
            return entity;
        }
    }
}
