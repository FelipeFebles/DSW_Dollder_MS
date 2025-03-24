using DSW_ApiNoConformidades_Dollder_MS.Application.Requests.NoConformidad;
using DSW_ApiNoConformidades_Dollder_MS.Core.Entities;

namespace DSW_ApiNoConformidades_Dollder_MS.Application.Mappers.NoConformidad
{
    public class NoConformidadMapper
    {
        public static NoConformidadEntity MapRequestNoConformidadEntity(NoConformidadRequest request, string expedicion)
        {
            // Mapear la entidad con el estado convertido
            return new NoConformidadEntity
            {
                numero_expedicion = expedicion,
                revisado_por = request.revisado_por,
                consecuencias = request.consecuencias,
                responsables_cargo = request.responsables_cargo,
                areas_involucradas = request.areas_involucradas,
                estado = Enum.TryParse<Estado>(request.estado, out var estado) ? estado : Estado.En_Proceso, // Asignar el valor del enum
                reporte_Id = (Guid)request.reporte_Id,
            };
        }
    }
}