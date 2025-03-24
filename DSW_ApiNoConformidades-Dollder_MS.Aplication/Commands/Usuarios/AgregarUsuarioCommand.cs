using MediatR;
using DSW_ApiNoConformidades_Dollder_MS.Application.Responses.Usuarios;
using DSW_ApiNoConformidades_Dollder_MS.Application.Requests.Usuarios;


namespace DSW_ApiNoConformidades_Dollder_MS.Application.Commands.Usuario
{
    public class AgregarUsuarioCommand : IRequest<IdUsuarioResponse>
    {
        public UsuarioRequest _request { get; set; }
        public AgregarUsuarioCommand(UsuarioRequest request)
        {
            _request = request;
        }
    }
}