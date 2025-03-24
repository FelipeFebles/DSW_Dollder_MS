namespace DSW_ApiNoConformidades_Dollder_MS.Core.Entities
{
    public class DepartamentoEntity : BaseEntity
    {
        public string? nombre { get; set; }         //Nombre del departamento
        public string? cargo { get; set; }          //Cargo en el departamento

        public ICollection<UsuarioEntity>? usuarios;
    }
}
