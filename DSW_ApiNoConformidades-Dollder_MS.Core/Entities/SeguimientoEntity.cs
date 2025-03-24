namespace DSW_ApiNoConformidades_Dollder_MS.Core.Entities
{
    public class SeguimientoEntity : BaseEntity
    {
        public string? fecha_seguimiento { get; set; }  // Fecha de seguimiento
        public string? estatus { get; set; }            // Estatus
        public string? observaciones { get; set; }      // Observaciones
        public string? realizado_por { get; set; }      // Realizado por

        //Relacion FK 
            //1..1 NoConformidad
            public Guid noConformidad_Id { get; set; }
            public NoConformidadEntity noConformidad { get; set; } = null!;
    }

}
