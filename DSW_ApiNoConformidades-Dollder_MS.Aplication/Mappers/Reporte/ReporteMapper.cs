using DSW_ApiNoConformidades_Dollder_MS.Application.Requests.Reportes;
using DSW_ApiNoConformidades_Dollder_MS.Core.Entities;

namespace DSW_ApiNoConformidades_Dollder_MS.Application.Mappers.Reporte
{
    public class ReporteMapper
    {
        public static ReporteEntity MapRequestReporteEntity(ReporteRequest request, Guid id)
        {
            var entity = new ReporteEntity()
            {
                detectado_por = request.detectado_por,
                area = request.area,
                titulo = request.titulo,
                descripcion = request.descripcion,
                usuario_Id = id,
                departamento_emisor = request.departamento_emisor,
            };
            return entity;
        }
    }
}
