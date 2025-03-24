using DSW_ApiNoConformidades_Dollder_MS.Aplication.Responses.Indicadores;
using DSW_ApiNoConformidades_Dollder_MS.Aplication.Responses.VerificacionEfectividad;

namespace DSW_ApiNoConformidades_Dollder_MS.Aplication.Responses.Cierre
{
    public class CierreResponse : BaseResponse
    {
        public bool? conforme { get; set; }              // Conforme
        public string? observaciones { get; set; }       // Observaciones
        public string? responsable { get; set; }         // Responsable
        public string? fecha_verificacion { get; set; }  // Fecha de verificación



        public Guid? noConformidad_Id { get; set; }
        public IndicadoresResponse? indicadores { get; set; } = new IndicadoresResponse(); // Indicadores
        public VerificacionEfectividadResponse? verificacionEfectividad { get; set; } = new VerificacionEfectividadResponse(); // Verificación de efectividad

    }
}
