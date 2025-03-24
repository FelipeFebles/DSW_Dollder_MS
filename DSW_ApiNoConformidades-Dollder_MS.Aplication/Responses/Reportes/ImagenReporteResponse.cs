namespace DSW_ApiNoConformidades_Dollder_MS.Aplication.Responses.Reportes
{
    public class ImagenReporteResponse : BaseResponse
    {
        public byte[]? imagen { get; set; } // La imagen como arreglo de bytes
        public string? nombre_archivo { get; set; } // Opcional: nombre original del archivo
        public Guid? reporte_Id { get; set; } // Llave foránea
    }
}
