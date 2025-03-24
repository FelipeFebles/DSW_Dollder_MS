using DSW_ApiNoConformidades_Dollder_MS.Aplication.Responses.Departamento;
using DSW_ApiNoConformidades_Dollder_MS.Application.Requests.Departamento;
using MediatR;

namespace DSW_ApiNoConformidades_Dollder_MS.Aplication.Commands.Departamento
{
    public class AgregarDepartamentoCommand : IRequest<IdDepartamentoResponse>
    {
        public DepartamentoRequest _request { get; set; }
        public AgregarDepartamentoCommand(DepartamentoRequest request)
        {
            _request = request;
        }
    }
}
