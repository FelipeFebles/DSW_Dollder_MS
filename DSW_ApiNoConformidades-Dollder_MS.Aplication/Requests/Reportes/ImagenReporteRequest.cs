using DSW_ApiNoConformidades_Dollder_MS.Application.Requests.Reportes;

namespace DSW_ApiNoConformidades_Dollder_MS.Aplication.Requests.Reportes
{
    public class ImagenReporteRequest : BaseRequest
    {
        public byte[]? imagen { get; set; } // La imagen como arreglo de bytes
        public string? nombre_archivo { get; set; } // Opcional: nombre original del archivo
        public Guid? reporte_Id { get; set; } // Llave foránea
    }
}
