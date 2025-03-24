namespace DSW_ApiNoConformidades_Dollder_MS.Aplication.Responses.Notificaciones
{
    public class NotificacionesResponse : BaseResponse
    {
        public string? titulo { get; set; }         // Titulo del correo
        public string? envia { get; set; }          // Envidado por 
        public string? dirigido { get; set; }       // Dirigido a
        public string? mensaje { get; set; }        // Nota del mensaje
        public bool? revisado { get; set; }       // Si el mensaje fue revisado o no
        public string? tipo { get; set; }           // Tipo de mensaje

    }
}
