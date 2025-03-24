using MediatR;
using DSW_ApiNoConformidades_Dollder_MS.Application.Responses.Usuarios;

namespace DSW_ApiNoConformidades_Dollder_MS.Application.Queries.Usuarios
{
    public class BuscarUsuariosQuery : IRequest<List<UsuarioResponse>>
    {
    }
}

