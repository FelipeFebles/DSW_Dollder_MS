namespace DSW_ApiNoConformidades_Dollder_MS.Core.Entities.Relaciones
{
    public class R_Acciones_UsuarioEntity : BaseEntity
    {
        // Relacion FK
        // AccionesCorrectivas 1..n 

        public Guid acciones_Id { get; set; }
        public AccionesEntity acciones { set; get; } = null!;

        // Usuario 1..*
        public Guid usuario_Id { get; set; }
        public UsuarioEntity usuario { get; set; } = null!;
    }
}
