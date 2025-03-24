using DSW_ApiNoConformidades_Dollder_MS.Aplication.Responses.Indicadores;
using MediatR;
namespace DSW_ApiNoConformidades_Dollder_MS.Aplication.Queries.Indicadores
{
    public class BuscarIndicadoresOrigenQuery : IRequest<List<IndicadorOrigenResponse>>
    {
        public BuscarIndicadoresOrigenQuery()
        {
        }
    }
  
}
