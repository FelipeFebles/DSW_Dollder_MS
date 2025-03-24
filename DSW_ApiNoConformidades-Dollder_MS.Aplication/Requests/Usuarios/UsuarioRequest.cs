using DSW_ApiNoConformidades_Dollder_MS.Aplication.Requests;
using DSW_ApiNoConformidades_Dollder_MS.Application.Requests.Departamento;
using DSW_ApiNoConformidades_Dollder_MS.Application.Requests.Reportes;
using DSW_ApiNoConformidades_Dollder_MS.Application.Requests.RevisionReporte;
using System.Globalization;

namespace DSW_ApiNoConformidades_Dollder_MS.Application.Requests.Usuarios
{
    public class UsuarioRequest : BaseRequest
    {
        // Constructor para limpiar automáticamente las cadenas
        public UsuarioRequest()
        {
            LimpiarCadenas();
        }
        public string? usuario { get; set; }
        public string? primer_nombre { get; set; }
        public string? segundo_nombre { get; set; }
        public string? primer_apellido { get; set; }
        public string? segundo_apellido { get; set; }
        public string? password { get; set; }
        public string? correo { get; set; }
        public string? preguntas_de_seguridad { set; get; }
        public string? preguntas_de_seguridad2 { set; get; }
        public string? respuesta_de_seguridad { set; get; }
        public string? respuesta_de_seguridad2 { set; get; }
        public bool? estado { set; get; }
        public string? nombreDepartamento { get; set; }
        public string? cargoDepartamento { get; set; }




        // Nueva propiedad para almacenar información del departamento
        public DepartamentoRequest? departamento { get; set; }

        public Guid? id_departamento { get; set; }
        public List<ReporteRequest>? reportes { get; set; }
        public List<RevisionReporteRequest>? revisionReportes { get; set; }




        // Propiedad calculada para el nombre completo
        public string? nombre
        {
            get
            {
                var nombres = new List<string?> { primer_nombre, segundo_nombre };
                return string.Join(" ", nombres.Where(n => !string.IsNullOrWhiteSpace(n)));
            }
        }

        // Propiedad calculada para el apellido completo
        public string? apellido
        {
            get
            {
                var apellidos = new List<string?> { primer_apellido, segundo_apellido };
                return string.Join(" ", apellidos.Where(a => !string.IsNullOrWhiteSpace(a)));
            }
        }

        // Método para limpiar todas las cadenas de texto
        private void LimpiarCadenas()
        {
            usuario = LimpiarCadena(usuario);
            primer_nombre = LimpiarCadena(primer_nombre);
            segundo_nombre = LimpiarCadena(segundo_nombre);
            primer_apellido = LimpiarCadena(primer_apellido);
            segundo_apellido = LimpiarCadena(segundo_apellido);
            correo = LimpiarCadena(correo);
            preguntas_de_seguridad = LimpiarCadena(preguntas_de_seguridad);
            preguntas_de_seguridad2 = LimpiarCadena(preguntas_de_seguridad2);
            respuesta_de_seguridad = LimpiarCadena(respuesta_de_seguridad);
            respuesta_de_seguridad2 = LimpiarCadena(respuesta_de_seguridad2);
            nombreDepartamento = LimpiarCadena(nombreDepartamento);
            cargoDepartamento = LimpiarCadena(cargoDepartamento);
        }

        // Método para limpiar una cadena y poner la primera letra en mayúscula
        private string? LimpiarCadena(string? valor)
        {
            if (string.IsNullOrWhiteSpace(valor))
                return valor;

            // Eliminar espacios innecesarios
            valor = valor.Trim();

            // Poner la primera letra en mayúscula
            TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
            return textInfo.ToTitleCase(valor.ToLower());
        }
    }
}