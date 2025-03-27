using DSW_ApiNoConformidades_Dollder_MS.Application.Requests.Usuarios;
using DSW_ApiNoConformidades_Dollder_MS.Core.Entities.Child.Usuario;


namespace DSW_ApiNoConformidades_Dollder_MS.Application.Mappers.Usuario
{
    public class UsuarioMapper
    {

        public static OperarioEntity MapRequestOperarioEntity(UsuarioRequest request)
        {
            var entity = new OperarioEntity()
            {
                usuario = request.usuario,
                nombre = request.nombre,
                apellido = request.apellido,
                password = request.password, // Asegúrate de hashear la contraseña antes de guardarla
                correo = request.correo,
                preguntas_de_seguridad = request.preguntas_de_seguridad,
                preguntas_de_seguridad2 = request.preguntas_de_seguridad2,
                respuesta_de_seguridad = request.respuesta_de_seguridad,
                respuesta_de_seguridad2 = request.respuesta_de_seguridad2,
                estado = request.estado,
                departamento_Id = (Guid)request.id_departamento,
            };
            return entity;
        }

        public static RegenciaEntity MapRequestRegenciaEntity(UsuarioRequest request)
        {
            var entity = new RegenciaEntity()
            {
                usuario = request.usuario,
                nombre = request.nombre,
                apellido = request.apellido,
                password = request.password, // Asegúrate de hashear la contraseña antes de guardarla
                correo = request.correo,
                preguntas_de_seguridad = request.preguntas_de_seguridad,
                preguntas_de_seguridad2 = request.preguntas_de_seguridad2,
                respuesta_de_seguridad = request.respuesta_de_seguridad,
                respuesta_de_seguridad2 = request.respuesta_de_seguridad2,
                estado = request.estado,
                departamento_Id = (Guid)request.id_departamento,
            };
            return entity;
        }
        public static CalidadEntity MapRequestCalidadEntity(UsuarioRequest request)
        {
            var entity = new CalidadEntity()
            {
                usuario = request.usuario,
                nombre = request.nombre,
                apellido = request.apellido,
                password = request.password, // Asegúrate de hashear la contraseña antes de guardarla
                correo = request.correo,
                preguntas_de_seguridad = request.preguntas_de_seguridad,
                preguntas_de_seguridad2 = request.preguntas_de_seguridad2,
                respuesta_de_seguridad = request.respuesta_de_seguridad,
                respuesta_de_seguridad2 = request.respuesta_de_seguridad2,
                estado = request.estado,
                departamento_Id = (Guid)request.id_departamento,
            };
            return entity;
        }
    }
}
