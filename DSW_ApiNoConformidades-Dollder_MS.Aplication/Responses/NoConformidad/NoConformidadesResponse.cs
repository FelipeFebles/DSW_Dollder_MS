using DSW_ApiNoConformidades_Dollder_MS.Aplication.Responses.Cierre;
using DSW_ApiNoConformidades_Dollder_MS.Aplication.Responses.Responsable;
using DSW_ApiNoConformidades_Dollder_MS.Aplication.Responses.Seguimiento;

namespace DSW_ApiNoConformidades_Dollder_MS.Aplication.Responses.NoConformidad
{
    public class NoConformidadesResponse
    {
        public Guid? Id { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }

        public string? numero_expedicion { get; set; }          // Numero de expedicion de la no conformidad NC-25-001
        public string? revisado_por { get; set; }                // Revisado por
        public string? consecuencias { get; set; }               // Consecuencias del reporte
        public List<string>? responsables_cargo { get; set; }          // Responsables
        public List<string>? areas_involucradas { get; set; }    // Áreas involucradas
        public int? prioridad { get; set; }                      // Prioridad
        public string? estado { get; set; }                       // Estado es un enumerado de la carpera Core>Enum

        public string? titulo { get; set; }                      // Titulo
        public string? area { get; set; }                        // Area

        public Guid? reporte_Id { get; set; }                    // Id Reporte
        public List<ResponsableResponse>? responsables { get; set; } = new List<ResponsableResponse>(); // Responsables
        public CierreResponse? cierre { get; set; } = new CierreResponse();              // Cierre
        public List<SeguimientoResponse>? seguimientos { get; set; } = new List<SeguimientoResponse>();// Seguimientos
    }
}
