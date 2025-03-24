using DSW_ApiNoConformidades_Dollder_MS.Aplication.Responses.Departamento;
using DSW_ApiNoConformidades_Dollder_MS.Application.Requests.Departamento;
using MediatR;

namespace DSW_ApiNoConformidades_Dollder_MS.Aplication.Commands.Departamento
{
    public class ActualizarDepartamentoCommand : IRequest<IdDepartamentoResponse>
    {
        public DepartamentoRequest _request { get; set; }
        public ActualizarDepartamentoCommand(DepartamentoRequest request)
        {
            _request = request;
        }
    }
}
