using DSW_ApiNoConformidades_Dollder_MS.Aplication.Requests;
using DSW_ApiNoConformidades_Dollder_MS.Aplication.Requests.Cierre;
using DSW_ApiNoConformidades_Dollder_MS.Aplication.Requests.Seguimiento;
using DSW_ApiNoConformidades_Dollder_MS.Application.Requests.Responsables;

namespace DSW_ApiNoConformidades_Dollder_MS.Application.Requests.NoConformidad
{
    public class NoConformidadRequest 
    {
        public Guid? Id { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }


        public string? revisado_por { get; set; }                // Revisado por
        public string? consecuencias { get; set; }               // Consecuencias del reporte
        public List<string>? responsables_cargo { get; set; } = new List<string>();       // Responsables
        public List<string>? areas_involucradas { get; set; } = new List<string>();   // Áreas involucradas
        public int? prioridad { get; set; }                      // Prioridad
        public string? estado { get; set; }                       // Estado es un enumerado de la carpera Core>Enum
        public string? numero_expedicion { get; set; }          // Numero de expedicion de la no conformidad NC-25-001

        public Guid? reporte_Id { get; set; }                    // Id Reporte
        public List<ResponsableRequest>? responsables { get; set; } = new List<ResponsableRequest>(); // Responsables
        public CierreRequest? cierre { get; set; } = new CierreRequest();               // Cierre
        public List<SeguimientoRequest>? seguimientos { get; set; } = new List<SeguimientoRequest>(); // Seguimientos

    }
}
