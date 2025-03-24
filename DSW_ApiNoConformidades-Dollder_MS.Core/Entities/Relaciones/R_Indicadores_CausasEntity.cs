using DSW_ApiNoConformidades_Dollder_MS.Core.Entities.Child.Indicadores;

namespace DSW_ApiNoConformidades_Dollder_MS.Core.Entities.Relaciones
{
    public class R_Indicadores_CausasEntity: BaseEntity
    {
        // Relacion FK
        // Causa 1..n 

        public Guid causa_Id { get; set; }
        public IndicadorCausaEntity causa { set; get; } = null!;

        // Usuario 1..*
        public Guid indicador_Id { get; set; }
        public IndicadoresEntity indicador { get; set; } = null!;
    }
}
