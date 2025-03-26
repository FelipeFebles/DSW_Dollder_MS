using DSW_ApiNoConformidades_Dollder_MS.Aplication.Requests.Departamento;
using DSW_ApiNoConformidades_Dollder_MS.Application.Responses.Departamento;
using MediatR;


namespace DSW_ApiNoConformidades_Dollder_MS.Aplication.Queries.Departamento
{
    public class BuscarIdDepartamentoQuery : IRequest<DepartamentoResponse>
    {
        public IdDepartamentoRequest _request { get; set; }
        public BuscarIdDepartamentoQuery(IdDepartamentoRequest request)
        {
            _request = request;
        }
    }
}
