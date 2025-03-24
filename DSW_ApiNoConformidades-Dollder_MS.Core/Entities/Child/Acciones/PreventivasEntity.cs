namespace DSW_ApiNoConformidades_Dollder_MS.Core.Entities.Child.Acciones
{
    public class PreventivasEntity : AccionesEntity
    {
        public Guid correctiva_Id { get; set; }
        public CorrectivasEntity correctiva { get; set; } = null!;
    }
}
