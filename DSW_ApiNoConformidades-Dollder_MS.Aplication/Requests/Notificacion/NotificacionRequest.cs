using DSW_ApiNoConformidades_Dollder_MS.Aplication.Requests;

namespace DSW_ApiNoConformidades_Dollder_MS.Application.Requests.Notificacion
{
    public class NotificacionRequest : BaseRequest
    {
        public string? titulo { get; set; }         // Titulo del correo
        public string? envia { get; set; }          // Envidado por 
        public string? dirigido { get; set; }       // Dirigido a
        public string? mensaje { get; set; }        // Nota del mensaje
        public bool? revisado { get; set; }       // Si el mensaje fue revisado o no
        public string? tipo { get; set; }           // Tipo de mensaje


        public NotificacionRequest(string titulo, string envia, string dirigido, string mensaje, bool revisado, string? tipo)
        {
            this.titulo = titulo;
            this.envia = envia;
            this.dirigido = dirigido;
            this.mensaje = mensaje;
            this.revisado = revisado;
            this.tipo = tipo;
        }
    }
}
