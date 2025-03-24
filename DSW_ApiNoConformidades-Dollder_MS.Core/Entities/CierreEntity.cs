namespace DSW_ApiNoConformidades_Dollder_MS.Core.Entities
{
    public class CierreEntity : BaseEntity
    {
        public bool? conforme { get; set; }              // Conforme
        public string? observaciones { get; set; }       // Observaciones
        public string? responsable { get; set; }         // Responsable
        public string? fecha_verificacion { get; set; }  // Fecha de verificación

        //Relaciones PK
            // 1..1 Verificacion de efectividad
            public VerificacionEfectividadEntity verificacionEfectividad = new VerificacionEfectividadEntity();
            // 1..1 Indicadores
            public IndicadoresEntity indicadores = new IndicadoresEntity();

        //Relacion FK 
            //1..1 NoConfomidad
            public Guid noConformidad_Id { get; set; }
            public NoConformidadEntity noConformidad { get; set; } = null!;
    }

}
