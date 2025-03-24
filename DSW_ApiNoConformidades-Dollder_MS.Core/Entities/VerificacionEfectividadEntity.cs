namespace DSW_ApiNoConformidades_Dollder_MS.Core.Entities
{
    public class VerificacionEfectividadEntity : BaseEntity
    {
        public bool? efectiva { get; set; }          // Efectiva
        public string? observaciones { get; set; }   // Observaciones
        public string? realizado_por { get; set; }   // Realizado por

        //Relacion FK 
            //1..1 Cierre
            public Guid cierre_Id { get; set; }
            public CierreEntity cierre { get; set; } = null!;
    }

}
