using DSW_ApiNoConformidades_Dollder_MS.Aplication.Requests.Indicadores;
using DSW_ApiNoConformidades_Dollder_MS.Aplication.Requests.VerificacionEfectividad;

namespace DSW_ApiNoConformidades_Dollder_MS.Aplication.Requests.Cierre
{
    public class CierreRequest : BaseRequest
    {
        public bool? conforme { get; set; }              // Conforme
        public string? observaciones { get; set; }       // Observaciones
        public string? responsable { get; set; }         // Responsable
        public string? fecha_verificacion { get; set; }  // Fecha de verificación



        public Guid? noConformidad_Id { get; set; }
        public IndicadoresRequest? indicadores { get; set; } = new IndicadoresRequest(); // Indicadores
        public VerificacionEfectividadRequest? verificacionEfectividad { get; set; } = new VerificacionEfectividadRequest();// Verificación de efectividad

    }
}
