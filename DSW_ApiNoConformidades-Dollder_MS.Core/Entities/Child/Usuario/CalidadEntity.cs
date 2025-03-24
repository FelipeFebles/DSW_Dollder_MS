using DSW_ApiNoConformidades_Dollder_MS.Core.Entities.Relaciones;

namespace DSW_ApiNoConformidades_Dollder_MS.Core.Entities.Child.Usuario;

public class CalidadEntity : UsuarioEntity
{
    //Relaciones PK

    //1..* NoConformidad
    public ICollection<R_Calidad_NoConformidadEntity>? r_Calidad_NoConformidadEntity;


}
