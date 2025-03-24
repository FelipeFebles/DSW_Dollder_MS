using DSW_ApiNoConformidades_Dollder_MS.Aplication.Requests.Indicadores;
using DSW_ApiNoConformidades_Dollder_MS.Aplication.Responses.Indicadores;
using MediatR;

namespace DSW_ApiNoConformidades_Dollder_MS.Aplication.Commands.Indicadores
{
    public class AgregarIndicadorOrigenCommand : IRequest<IdIndicadoresResponse>
    {
        public IndicadorOrigenRequest _request { get; set; }
        public AgregarIndicadorOrigenCommand(IndicadorOrigenRequest request)
        {
            _request = request;
        }
    }
}
