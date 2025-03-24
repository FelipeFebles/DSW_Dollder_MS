using DSW_ApiNoConformidades_Dollder_MS.Application.Requests.Responsables;
using DSW_ApiNoConformidades_Dollder_MS.Core.Entities;

namespace DSW_ApiNoConformidades_Dollder_MS.Application.Mappers.Responsable
{
    public class ResponsableMapper
    {
        public static ResponsableEntity MapResponsableMapperEntity(ResponsableRequest request)
        {
            var entity = new ResponsableEntity()
            {
                investigacion = request.investigacion,
                analisis_causa = request.analisis_causa,
                correccion = request.correccion,
                analisis_riesgo = request.analisis_riesgo,
                usuario_Id = (Guid)request.usuario_Id,
                noConformidad_Id = (Guid)request.noConformidad_Id,
                cargo_responsable = request.cargo_usuario,
            }; 
            return entity;
        }
    }
}
