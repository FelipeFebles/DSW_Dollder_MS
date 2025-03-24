namespace DSW_ApiNoConformidades_Dollder_MS.Aplication.Responses.RevisionReporte
{
    public class RevisionReporteResponse : BaseResponse
    {
        public string? nombre { get; set; }     // Nombre de la revisión 



        public Guid? id_usuario { get; set; }
        public Guid? id_reporte { get; set; }   //Id del reporte
    }
}
