using DSW_ApiNoConformidades_Dollder_MS.Application.Requests.Usuarios;
using DSW_ApiNoConformidades_Dollder_MS.Application.Responses.Usuarios;
using MediatR;

namespace DSW_ApiNoConformidades_Dollder_MS.Application.Commands.Usuario
{
    public class ActualizarUsuarioCommand : IRequest<IdUsuarioResponse>
    {
        public UsuarioRequest _request { get; set; }
        public ActualizarUsuarioCommand(UsuarioRequest request)
        {
            _request = request;
        }
    }
}
