namespace DSW_ApiNoConformidades_Dollder_MS.Aplication.Requests.VerificacionEfectividad
{
    public class VerificacionEfectividadRequest : BaseRequest
    {
        public bool? efectiva { get; set; }          // Efectiva
        public string? observaciones { get; set; }   // Observaciones
        public string? realizado_por { get; set; }   // Realizado por

        public Guid? cierre_Id { get; set; }
    }
}
