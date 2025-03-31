using DSW_ApiNoConformidades_Dollder_MS.Aplication.Requests.Calendario;
using DSW_ApiNoConformidades_Dollder_MS.Aplication.Responses.Calendario;
using MediatR;

namespace DSW_ApiNoConformidades_Dollder_MS.Aplication.Commands.Calendario
{
    public class AgregarCalendarioCommand : IRequest<IdCalendarioResponse>
    {
        public CalendarioRequest _request { get; set; }
        public AgregarCalendarioCommand(CalendarioRequest request)
        {
            _request = request;
        }
    }
}

