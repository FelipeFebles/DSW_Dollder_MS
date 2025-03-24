using DSW_ApiNoConformidades_Dollder_MS.Aplication.Requests;

namespace DSW_ApiNoConformidades_Dollder_MS.Application.Requests.RevisionReporte
{
    public class RevisionReporteRequest : BaseRequest
    {
        public string? nombre { get; set; }     // Nombre de la revisión 



        public Guid? id_usuario { get; set; }
        public Guid? id_reporte { get; set; }   //Id del reporte
    }
}
