namespace DSW_ApiNoConformidades_Dollder_MS.Aplication.Responses.Seguimiento
{
    public class SeguimientoResponse: BaseResponse
    {
        public string? fecha_seguimiento { get; set; }  // Fecha de seguimiento
        public string? estatus { get; set; }            // Estatus
        public string? observaciones { get; set; }      // Observaciones
        public string? realizado_por { get; set; }      // Realizado por

        //1..1 NoConformidad
        public Guid? noConformidad_Id { get; set; }
    }
}
