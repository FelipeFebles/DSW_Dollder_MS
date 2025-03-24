using DSW_ApiNoConformidades_Dollder_MS.Application.Requests.RevisionReporte;
using DSW_ApiNoConformidades_Dollder_MS.Core.Entities;

namespace DSW_ApiNoConformidades_Dollder_MS.Application.Mappers.RevisionReporte
{
    public  class RevisionReporteMapper
    {
        public static RevisionReporteEntity MapRequestRevisionReporteEntity(RevisionReporteRequest request, ReporteEntity reporte, Guid id)
        {
            var entity = new RevisionReporteEntity()
            {
                nombre = request.nombre,
                reporte = reporte,
                usuario_Id = id,
            };
            return entity;
        }
    }
}
