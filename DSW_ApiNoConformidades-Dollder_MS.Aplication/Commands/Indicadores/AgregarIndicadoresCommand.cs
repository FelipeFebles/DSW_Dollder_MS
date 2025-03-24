using DSW_ApiNoConformidades_Dollder_MS.Aplication.Requests.Indicadores;
using DSW_ApiNoConformidades_Dollder_MS.Aplication.Responses.Indicadores;
using MediatR;

namespace DSW_ApiNoConformidades_Dollder_MS.Aplication.Commands.Indicadores
{
    public class AgregarIndicadoresCommand : IRequest<IdIndicadoresResponse>
    {
        public IndicadoresRequest _request { get; set; }
        public AgregarIndicadoresCommand(IndicadoresRequest request)
        {
            _request = request;
        }
    }
}

