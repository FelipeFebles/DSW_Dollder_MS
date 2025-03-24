using DSW_ApiNoConformidades_Dollder_MS.Aplication.Requests;
using DSW_ApiNoConformidades_Dollder_MS.Application.Requests.AccionesCorrectivas;

namespace DSW_ApiNoConformidades_Dollder_MS.Application.Requests.Responsables
{
    public class ResponsableRequest : BaseRequest
    {
        public string? investigacion { get; set; }    // Investigación
        public List<string>? analisis_causa { get; set; }   // Análisis de causa
        public string? correccion { get; set; }       // Corrección
        public string? analisis_riesgo { get; set; }  // Análisis de riesgo
        public string? fecha_compromiso { get; set; }      // fecha_compromiso de la actividad
        public string? nombre_usuario { get; set; }           //Lo usare para ver el nombre del usuario
        public string? apellido_usuario { get; set; }         //Lo usare para ver el Apellido del usuario  
        public string? cargo_usuario { get; set; }    // Cargo del usuario



        public Guid? usuario_Id { get; set; }
        public Guid? noConformidad_Id { get; set; }
        public List<AccionesRequest>? acciones { get; set; } = new List<AccionesRequest>();
    }
}
