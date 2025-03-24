using DSW_ApiNoConformidades_Dollder_MS.Application.Requests;
using DSW_ApiNoConformidades_Dollder_MS.Application.Responses.Usuarios;
using MediatR;


namespace DSW_ApiNoConformidades_Dollder_MS.Application.Queries.Usuarios
{
    public class BuscarUsuariosDepartamentoQuery : IRequest<List<UsuarioResponse>>
    {
        public BuscarUsuarioRequest _request { get; set; }
        public BuscarUsuariosDepartamentoQuery(BuscarUsuarioRequest request)
        {
            _request = request;
        }
    }
}

