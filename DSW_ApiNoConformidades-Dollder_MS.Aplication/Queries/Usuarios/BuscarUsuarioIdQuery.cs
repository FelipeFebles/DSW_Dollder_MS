using MediatR;
using DSW_ApiNoConformidades_Dollder_MS.Application.Requests;
using DSW_ApiNoConformidades_Dollder_MS.Application.Responses.Usuarios;

namespace DSW_ApiNoConformidades_Dollder_MS.Application.Queries.Usuarios
{
    public class BuscarUsuarioIdQuery : IRequest<UsuarioResponse>
    {
        public BuscarUsuarioIDRequest _request { get; set; }
        public BuscarUsuarioIdQuery(BuscarUsuarioIDRequest request)
        {
            _request = request;
        }
    }
}
