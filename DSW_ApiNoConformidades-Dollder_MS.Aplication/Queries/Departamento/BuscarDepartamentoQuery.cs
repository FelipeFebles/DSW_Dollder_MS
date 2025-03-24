using DSW_ApiNoConformidades_Dollder_MS.Application.Responses.Departamento;
using MediatR;

namespace DSW_ApiNoConformidades_Dollder_MS.Aplication.Queries.Departamento
{
    public class BuscarDepartamentoQuery: IRequest<List<DepartamentoResponse>>
    {
        public BuscarDepartamentoQuery()
        {
        }
    }
}
