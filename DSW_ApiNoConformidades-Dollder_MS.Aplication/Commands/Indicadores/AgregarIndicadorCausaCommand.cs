using DSW_ApiNoConformidades_Dollder_MS.Aplication.Requests.Indicadores;
using DSW_ApiNoConformidades_Dollder_MS.Aplication.Responses.Indicadores;
using MediatR;

namespace DSW_ApiNoConformidades_Dollder_MS.Aplication.Commands.Indicadores
{
    public class AgregarIndicadorCausaCommand : IRequest<IdIndicadoresResponse>
    {
        public IndicadorCausaRequest _request { get; set; }
        public AgregarIndicadorCausaCommand(IndicadorCausaRequest request)
        {
            _request = request;
        }
    }
}
