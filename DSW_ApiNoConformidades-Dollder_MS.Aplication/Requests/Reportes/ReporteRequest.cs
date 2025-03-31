using DSW_ApiNoConformidades_Dollder_MS.Aplication.Requests;
using DSW_ApiNoConformidades_Dollder_MS.Aplication.Requests.Reportes;
using DSW_ApiNoConformidades_Dollder_MS.Application.Requests.NoConformidad;
using DSW_ApiNoConformidades_Dollder_MS.Application.Requests.RevisionReporte;

namespace DSW_ApiNoConformidades_Dollder_MS.Application.Requests.Reportes
{
    public class ReporteRequest : BaseRequest
    {
        public string? detectado_por { get; set; }       // Quién detectó el reporte
        public string? area { get; set; }                // Área relacionada con el reporte
        public string? titulo { get; set; }              // Título del reporte
        public string? descripcion { get; set; }         // Descripción detallada del reporte
        public string? departamento_emisor { get; set; } // Departamento emisor del reporte

        public List<ImagenReporteRequest>? imagenes { get; set; }
        public Guid? id_usuario { get; set; }            // Id Usuario
        public NoConformidadRequest? noConformidad { get; set; } = new NoConformidadRequest(); // No conformidad asociada al reporte
        public RevisionReporteRequest? revisionReporte { get; set; } = new RevisionReporteRequest(); // Revisión del reporte
    }
}
