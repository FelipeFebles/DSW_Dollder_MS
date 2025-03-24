namespace DSW_ApiNoConformidades_Dollder_MS.Core.Entities
{
    public class ImagenReporteEntity : BaseEntity
    {
        public byte[]? imagen { get; set; } // La imagen como arreglo de bytes
        public string? nombre_archivo { get; set; } // Opcional: nombre original del archivo


        public Guid reporte_Id { get; set; } // Llave foránea
        public ReporteEntity reporte { get; set; } // Relación inversa
    }
}

