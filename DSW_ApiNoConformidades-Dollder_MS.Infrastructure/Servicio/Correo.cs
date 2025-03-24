using System.Net.Mail;

namespace DSW_ApiNoConformidades_Dollder_MS.Infrastructure.Servicio
{
    public class Correo
    {
        private SmtpClient cliente = new SmtpClient("smtp.gmail.com", 587); //Creo la variable que envia correo atravez del protocolo SMTP
        private string correo = "";
        private string clave = ""; 

        /// <summary>
        /// Envía un correo electrónico al destinatario especificado con el asunto y cuerpo del mensaje especificados.
        /// </summary>
        /// <param name="correo_para_quien">La dirección de correo electrónico del destinatario.</param>
        /// <param name="asunto_del_correo">El asunto del correo electrónico.</param>
        /// <param name="cuerpo_del_mensaje">El cuerpo del mensaje del correo electrónico.</param>
        /// <exception cref="ArgumentNullException">Se lanza cuando la dirección de correo electrónico del destinatario, el asunto del correo electrónico o el cuerpo del mensaje son nulos o vacíos.</exception>
        public void EnviaCorreoUsuario(string correo_para_quien, string asunto_del_correo, string cuerpo_del_mensaje)
        {
            cliente.UseDefaultCredentials = false;
            cliente.Credentials = new System.Net.NetworkCredential(correo, clave);
            cliente.EnableSsl = true;

            MailMessage message = new MailMessage();
            message.From = new MailAddress(correo);

            message.To.Add(correo_para_quien);
            message.Subject = asunto_del_correo;
            message.Body = cuerpo_del_mensaje;
            message.IsBodyHtml = true;
            cliente.Send(message);
        }
    }
}
