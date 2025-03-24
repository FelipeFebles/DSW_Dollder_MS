using DSW_ApiNoConformidades_Dollder_MS.Aplication.Requests.Reportes;
using DSW_ApiNoConformidades_Dollder_MS.Application.Requests.Reportes;
using DSW_ApiNoConformidades_Dollder_MS.Core.Entities;

namespace DSW_ApiNoConformidades_Dollder_MS.Aplication.Mappers.Reporte
{
    public class ImagenReporteMapper
    {
        public static ImagenReporteEntity MapRequestImagenReporteEntity(ImagenReporteRequest request, Guid id)
        {
            var entity = new ImagenReporteEntity()
            {
                reporte_Id = id,
                imagen = request.imagen,
                nombre_archivo=request.nombre_archivo,
            };
            return entity;
        }
    }
}
