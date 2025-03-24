namespace DSW_ApiNoConformidades_Dollder_MS.Core.Entities
{
    public class ResponsableEntity : BaseEntity
    {
        public string? investigacion { get; set; }    // Investigación
        public List<string>? analisis_causa { get; set; }   // Análisis de causa
        public string? correccion { get; set; }       // Corrección
        public string? analisis_riesgo { get; set; }  // Análisis de riesgo
        public string? cargo_responsable { get; set; } // cargo
        public string? fecha_compromiso { get; set; } // Fecha de compromiso

        //Relaciones PK
        //Acciones Correctivas
        public ICollection<AccionesEntity> acciones = null!;

        //Relacion FK 
        //1..* No Conformidad
            public Guid usuario_Id { get; set; }
            public UsuarioEntity usuario { get; set; } = null!;

            //1..* No Conformidad
            public Guid noConformidad_Id { get; set; }
            public NoConformidadEntity noConformidad { get; set; } = null!;
    }

}
