namespace DSW_ApiNoConformidades_Dollder_MS.Core.Entities.Child.Indicadores
{
    public class IndicadorOrigenEntity : BaseEntity
    {
        public string? origen {  get; set; }
        public ICollection<IndicadoresEntity>? indicadores;
    }
}
