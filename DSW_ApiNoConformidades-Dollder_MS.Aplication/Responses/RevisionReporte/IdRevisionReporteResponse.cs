namespace DSW_ApiNoConformidades_Dollder_MS.Application.Responses.RevisionReporte
{
    public class IdRevisionReporteResponse
    {
        public IdRevisionReporteResponse(Guid id, Guid? id_reporte)
        {
            this.id = id;
            this.id_reporte = id_reporte;
        }
        public IdRevisionReporteResponse(Guid id)
        {
            this.id = id;
            this.id_reporte = id_reporte;
        }

        public Guid? id { get; set; }
        public Guid? id_reporte { get; set; }
    }
}
