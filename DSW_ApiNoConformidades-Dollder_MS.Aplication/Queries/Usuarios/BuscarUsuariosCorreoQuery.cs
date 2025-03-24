using MediatR;
using DSW_ApiNoConformidades_Dollder_MS.Application.Requests;
using DSW_ApiNoConformidades_Dollder_MS.Application.Responses.Usuarios;

namespace DSW_ApiNoConformidades_Dollder_MS.Application.Queries.Usuarios
{
    public class BuscarUsuariosCorreoQuery : IRequest<List<UsuarioResponse>>
    {
        public BuscarUsuarioRequest _request { get; set; }
        public BuscarUsuariosCorreoQuery(BuscarUsuarioRequest request)
        {
            _request = request;
        }
    }
}
