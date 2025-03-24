using DSW_ApiNoConformidades_Dollder_MS.Core.Entities.Relaciones;

namespace DSW_ApiNoConformidades_Dollder_MS.Application.Mappers.R_Acciones_Usuario
{
    public class R_Acciones_UsuarioMapper
    {
        public static R_Acciones_UsuarioEntity MapR_Acciones_UsuarioEntity(Guid acciones, Guid usuario)
        {
            var entity = new R_Acciones_UsuarioEntity()
            {
                acciones_Id = acciones,
                usuario_Id = usuario
            };
            return entity;
        }
    }
}
