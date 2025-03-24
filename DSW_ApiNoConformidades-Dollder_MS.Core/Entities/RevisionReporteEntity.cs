namespace DSW_ApiNoConformidades_Dollder_MS.Core.Entities
{
    public class RevisionReporteEntity : BaseEntity
    {
        public string? nombre { get; set; }     // Nombre de la revisión 

        //Relaciones FK 
            //1..1 con Reporte
            public Guid reporte_Id { get; set; }
            public ReporteEntity reporte { get; set; } = null!;

            //1..n con Usuario
            public Guid usuario_Id { get; set; }
            public UsuarioEntity usuario {get; set;} = null!;
    }

}
