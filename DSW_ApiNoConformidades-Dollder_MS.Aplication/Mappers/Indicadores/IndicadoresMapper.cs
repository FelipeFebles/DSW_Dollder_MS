using DSW_ApiNoConformidades_Dollder_MS.Aplication.Requests.Indicadores;
using DSW_ApiNoConformidades_Dollder_MS.Core.Entities;
using DSW_ApiNoConformidades_Dollder_MS.Core.Entities.Child.Indicadores;
using DSW_ApiNoConformidades_Dollder_MS.Core.Entities.Relaciones;

namespace DSW_ApiNoConformidades_Dollder_MS.Aplication.Mappers.Indicadores
{
    public class IndicadoresMapper
    {
        public static IndicadoresEntity MapRequestIndicadoresEntity(IndicadoresRequest request)
        {
            var entity = new IndicadoresEntity()
            {
                cierre_Id = (Guid)request.cierre_Id,
                origen_Id = (Guid)request.origen_Id,
            };
            return entity;
        }


        public static IndicadorOrigenEntity MapRequestIndicadorOrigenEntity(IndicadorOrigenRequest request)
        {
            var entity = new IndicadorOrigenEntity()
            {
                origen = request.origen,
            };
            return entity;
        }



        public static IndicadorCausaEntity MapRequestIndicadorCausaEntity(IndicadorCausaRequest request)
        {
            var entity = new IndicadorCausaEntity()
            {
                causa = request.causa,
            };
            return entity;
        }

        public static R_Indicadores_CausasEntity MapRequestR_Indicadores_CausasEntity(Guid causa, Guid indicador)
        {
            var entity = new R_Indicadores_CausasEntity()
            {
                causa_Id = causa,
                indicador_Id = indicador,
            };
            return entity;
        }
    }
}
