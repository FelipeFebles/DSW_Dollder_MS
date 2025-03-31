using DSW_ApiNoConformidades_Dollder_MS.Aplication.Responses.Calendario;
using MediatR;

namespace DSW_ApiNoConformidades_Dollder_MS.Aplication.Queries.Calendario
{
    public class BuscarCalendarioQuery : IRequest<List<CalendarioResponse>>
    {
        public BuscarCalendarioQuery()
        {
        }
    }
}
