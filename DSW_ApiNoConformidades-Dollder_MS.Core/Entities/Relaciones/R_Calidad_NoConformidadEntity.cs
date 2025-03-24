using DSW_ApiNoConformidades_Dollder_MS.Core.Entities.Child.Usuario;

namespace DSW_ApiNoConformidades_Dollder_MS.Core.Entities.Relaciones
{
    public class R_Calidad_NoConformidadEntity :BaseEntity
    {
        // Relacion FK
            // AccionesCorrectivas 1..n 

            public Guid calidad_Id { get; set; }
            public CalidadEntity calidad { set; get; } = null!;

            // Usuario 1..*
            public Guid noConformidad_Id { get; set; }
            public NoConformidadEntity noConformidad { get; set; } = null!;
    }
}
