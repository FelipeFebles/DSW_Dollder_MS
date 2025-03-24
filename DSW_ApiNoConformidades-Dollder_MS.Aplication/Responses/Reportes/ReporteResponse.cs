using DSW_ApiNoConformidades_Dollder_MS.Aplication.Responses.NoConformidad;
using DSW_ApiNoConformidades_Dollder_MS.Aplication.Responses.RevisionReporte;
using DSW_ApiNoConformidades_Dollder_MS.Core.Entities;

namespace DSW_ApiNoConformidades_Dollder_MS.Aplication.Responses.Reportes
{
    public class ReporteResponse: BaseResponse
    {

        public string? detectado_por { get; set; }       // Quién detectó el reporte
        public string? area { get; set; }                // Área relacionada con el reporte
        public string? titulo { get; set; }              // Título del reporte
        public string? descripcion { get; set; }         // Descripción detallada del reporte
        public List<ImagenReporteResponse>? imagenes { get; set; } = new List<ImagenReporteResponse>();

        public bool? estado { get; set; }                // Estado del reporte
        public string? departamento_emisor { get; set; } // Departamento emisor del reporte

        public string? cambiar { get; set; }


        public Guid? id_usuario { get; set; }            // Id Usuario
        public NoConformidadesResponse? noConformidad { get; set; } = new NoConformidadesResponse(); // No conformidad asociada al reporte
        public RevisionReporteResponse? revisionReporte { get; set; } = new RevisionReporteResponse(); // Revisión del reporte

    }
}
