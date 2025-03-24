using DSW_ApiNoConformidades_Dollder_MS.Core.Entities.Relaciones;


namespace DSW_ApiNoConformidades_Dollder_MS.Core.Entities
{
    public class NoConformidadEntity : BaseEntity
    {
        public string? numero_expedicion {  get; set; }          // Numero de expedicion de la no conformidad NC-25-001
        public string? revisado_por { get; set; }                // Revisado por
        public string? consecuencias { get; set; }               // Consecuencias del reporte
        public List<string>? responsables_cargo { get; set; }          // Responsables
        public List<string>? areas_involucradas { get; set; }    // Áreas involucradas
        public int? prioridad { get; set; }                      // Prioridad
        public Estado estado { get; set; }                       // Estado es un enumerado de la carpera Core>Enum

        //Relaciones PK
            // 1..* seguimiento
            public SeguimientoEntity? seguimiento;
            // 1..1 Cierre
            public CierreEntity cierre= new CierreEntity();
            // 1..* Reponsables
            public ICollection<ResponsableEntity> reponsables = new List<ResponsableEntity>();

            //1..1 Usuarios
            public ICollection<R_Calidad_NoConformidadEntity>? r_Calidad_NoConformidadEntity;

        //Relacion FK 

            // 1..1 Reporte
            public Guid reporte_Id { get; set; }
            public ReporteEntity reporte { get; set; } = null!;


    }

}
