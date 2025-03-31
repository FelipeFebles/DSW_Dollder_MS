namespace DSW_ApiNoConformidades_Dollder_MS.Core.Entities
{
    public class ReporteEntity : BaseEntity
    {
        public string? detectado_por { get; set; }       // Quién detectó el reporte
        public string? area { get; set; }                // Área relacionada con el reporte
        public string? titulo { get; set; }              // Título del reporte
        public string? descripcion { get; set; }         // Descripción detallada del reporte
        public string? departamento_emisor { get; set; } // Departamento emisor del reporte

        //Relacion PK 1..1 con Revision 

        public RevisionReporteEntity? revision =new RevisionReporteEntity();

        //Relacion PK 1..1 con No Conformidad 
        public NoConformidadEntity? noConformidad = new NoConformidadEntity();

        // Relación 1 a N con las imágenes
        public ICollection<ImagenReporteEntity>? imagenes = new List<ImagenReporteEntity>();

        //Relaciones FK
            // 1..n con Usuarios 
            public Guid usuario_Id { get; set; }
            public UsuarioEntity usuario { get; set; } = null!;

    }

}
