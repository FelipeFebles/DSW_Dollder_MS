using DSW_ApiNoConformidades_Dollder_MS.Aplication.Responses.Acciones;
using DSW_ApiNoConformidades_Dollder_MS.Application.Requests;
using MediatR;
namespace DSW_ApiNoConformidades_Dollder_MS.Aplication.Queries.Acciones
{
    public class BuscarAccionesCorrectivasQuery: IRequest<List<AccionesResponse>>
    {
        public BuscarUsuarioIDRequest _request { get; set; }
        public BuscarAccionesCorrectivasQuery(BuscarUsuarioIDRequest request)
        {
            _request = request;
        }
    }
}
