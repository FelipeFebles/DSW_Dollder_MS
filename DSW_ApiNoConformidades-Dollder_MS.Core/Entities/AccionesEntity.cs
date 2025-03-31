using DSW_ApiNoConformidades_Dollder_MS.Core.Entities.Relaciones;

namespace DSW_ApiNoConformidades_Dollder_MS.Core.Entities
{
    public class AccionesEntity : BaseEntity
    {
        public string? investigacion { get; set; }     // Acciones correctivas
        public string? area { get; set; }             // Area donde se detecto
        public bool? visto_bueno { get; set; }        // Revisado por el responsable
        public string? cargo_usuario { get; set; }      // Cargo del usuario responsable de la accion
        public string? fecha_compromiso { get; set; }      // fecha_compromiso de la actividad

        //Relaciones PK
        //1..n RevisionAccionesCorrectivas
        public ICollection<R_Acciones_UsuarioEntity>? R_Acciones_Usuario;
        //Relaciones FK
            // 1..* Responsable
            public Guid responsable_Id { get; set; }
            public ResponsableEntity responsable { get; set; } = null!;
    }
}
