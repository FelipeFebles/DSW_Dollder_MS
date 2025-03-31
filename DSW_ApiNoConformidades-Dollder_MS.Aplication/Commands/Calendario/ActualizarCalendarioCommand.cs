using DSW_ApiNoConformidades_Dollder_MS.Aplication.Requests.Calendario;
using DSW_ApiNoConformidades_Dollder_MS.Aplication.Responses.Calendario;
using MediatR;

namespace DSW_ApiNoConformidades_Dollder_MS.Aplication.Commands.Calendario
{
    public class ActualizarCalendarioCommand : IRequest<IdCalendarioResponse>
    {
        public CalendarioRequest _request { get; set; }
        public ActualizarCalendarioCommand(CalendarioRequest request)
        {
            _request = request;
        }
    }
}
