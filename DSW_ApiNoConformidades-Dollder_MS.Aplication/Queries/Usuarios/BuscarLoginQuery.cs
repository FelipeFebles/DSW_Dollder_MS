using DSW_ApiNoConformidades_Dollder_MS.Aplication.Requests.Usuarios;
using DSW_ApiNoConformidades_Dollder_MS.Application.Requests;
using DSW_ApiNoConformidades_Dollder_MS.Application.Requests.Usuarios;
using DSW_ApiNoConformidades_Dollder_MS.Application.Responses.Usuarios;
using MediatR;


namespace DSW_ApiNoConformidades_Dollder_MS.Aplication.Queries.Usuarios
{
    public class BuscarLoginQuery : IRequest<UsuarioResponse>
    {
        public UsuarioLoginRequest _request { get; set; }
        public BuscarLoginQuery(UsuarioLoginRequest request)
        {
            _request = request;
        }
    }
}
