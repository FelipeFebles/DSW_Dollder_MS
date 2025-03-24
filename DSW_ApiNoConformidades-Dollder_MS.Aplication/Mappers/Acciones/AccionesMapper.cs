using DSW_ApiNoConformidades_Dollder_MS.Application.Requests.AccionesCorrectivas;
using DSW_ApiNoConformidades_Dollder_MS.Core.Entities.Child.Acciones;

namespace DSW_ApiNoConformidades_Dollder_MS.Application.Mappers.Acciones
{
    public class AccionesMapper
    {
        public static CorrectivasEntity MapCorrectivasEntity(AccionesRequest request)
        {
            var entity = new CorrectivasEntity()
            {
                investigacion = request.investigacion,
                area = request.area,
                estado = request.estado,
                visto_bueno = request.visto_bueno,
                cargo_usuario = request.cargo_usuario,
                responsable_Id = (Guid)request.responsable_Id,
            };
            return entity;
        }

        public static PreventivasEntity MapPreventivasEntity(AccionesRequest request)
        {
            var entity = new PreventivasEntity()
            {
                correctiva_Id = (Guid)request.correctivas_Id,
                investigacion = request.investigacion,
                area = request.area,
                estado = request.estado,
                visto_bueno = request.visto_bueno,
                cargo_usuario = request.cargo_usuario,
                responsable_Id = (Guid)request.responsable_Id,
            };
            return entity;
        }
    }
}
