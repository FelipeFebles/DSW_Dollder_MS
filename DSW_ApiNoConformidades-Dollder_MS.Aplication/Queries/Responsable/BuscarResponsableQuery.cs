using DSW_ApiNoConformidades_Dollder_MS.Aplication.Responses.Responsable;
using DSW_ApiNoConformidades_Dollder_MS.Application.Requests;
using MediatR;

namespace DSW_ApiNoConformidades_Dollder_MS.Aplication.Queries.Responsable
{
    public class BuscarResponsableQuery : IRequest<List<ResponsableResponse>>
    {
        public BuscarUsuarioIDRequest _request { get; set; }
        public BuscarResponsableQuery(BuscarUsuarioIDRequest request)
        {
            _request = request;
        }
    }
}
