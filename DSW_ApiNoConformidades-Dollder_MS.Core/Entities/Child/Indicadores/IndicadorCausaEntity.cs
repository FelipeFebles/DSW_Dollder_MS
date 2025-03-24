using DSW_ApiNoConformidades_Dollder_MS.Core.Entities.Relaciones;

namespace DSW_ApiNoConformidades_Dollder_MS.Core.Entities.Child.Indicadores
{
    public class IndicadorCausaEntity: BaseEntity
    {
        public string? causa {  get; set; }

        public ICollection<R_Indicadores_CausasEntity>? indicadores;
    }
}
