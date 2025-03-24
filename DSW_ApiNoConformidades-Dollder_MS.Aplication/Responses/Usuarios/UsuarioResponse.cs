using DSW_ApiNoConformidades_Dollder_MS.Aplication.Responses;
using DSW_ApiNoConformidades_Dollder_MS.Aplication.Responses.Reportes;
using DSW_ApiNoConformidades_Dollder_MS.Aplication.Responses.RevisionReporte;
using DSW_ApiNoConformidades_Dollder_MS.Application.Responses.Departamento;

namespace DSW_ApiNoConformidades_Dollder_MS.Application.Responses.Usuarios
{
    public class UsuarioResponse : BaseResponse
    {
        public string? usuario { get; set; }
        public string? nombre { get; set; }
        public string? apellido { get; set; }
        public string? password { get; set; }
        public string? correo { get; set; }
        public string? preguntas_de_seguridad { set; get; }
        public string? preguntas_de_seguridad2 { set; get; }
        public string? respuesta_de_seguridad { set; get; }
        public string? respuesta_de_seguridad2 { set; get; }
        public bool? estado { set; get; }
        public string? discriminator { set; get; }
        // Nueva propiedad para almacenar información del departamento
        public DepartamentoResponse? departamento { get; set; }

        public Guid? id_departamento { get; set; }
        public List<ReporteResponse>? reportes { get; set; }
        public List<RevisionReporteResponse>? revisionReportes { get; set; }
    }

}
