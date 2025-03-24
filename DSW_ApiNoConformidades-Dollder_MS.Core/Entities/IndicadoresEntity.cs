using DSW_ApiNoConformidades_Dollder_MS.Core.Entities.Child.Indicadores;
using DSW_ApiNoConformidades_Dollder_MS.Core.Entities.Relaciones;

namespace DSW_ApiNoConformidades_Dollder_MS.Core.Entities
{
    public class IndicadoresEntity : BaseEntity
    {
        //Relacion FK 
        //1..1 Cierre
            public Guid cierre_Id { get; set; }
            public CierreEntity cierre { get; set; } = null!;

        //1..n Indicador origen
            public Guid origen_Id { get; set; }
            public IndicadorOrigenEntity origen { get; set; } = null!;


        //Relaciones PK
        //1..n Indicadores-Causas
        public ICollection<R_Indicadores_CausasEntity>? R_Indicadores_Causas;
    }

}
