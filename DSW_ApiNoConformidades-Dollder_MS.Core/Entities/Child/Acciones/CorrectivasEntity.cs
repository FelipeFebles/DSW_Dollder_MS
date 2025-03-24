namespace DSW_ApiNoConformidades_Dollder_MS.Core.Entities.Child.Acciones
{
    public class CorrectivasEntity : AccionesEntity
    {
        //Relaciones PK

        //1..* NoConformidad
        public ICollection<PreventivasEntity>? preventivas;
    }
}
