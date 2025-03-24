using DSW_ApiNoConformidades_Dollder_MS.Aplication.Requests.NoConformidad;
using DSW_ApiNoConformidades_Dollder_MS.Aplication.Responses.NoConformidad;
using MediatR;

namespace DSW_ApiNoConformidades_Dollder_MS.Aplication.Queries.NoConformidades
{
    public class BuscarNoConformidadQuery : IRequest<NoConformidadesResponse>
    {
        public IdNoConformidadRequest _request { get; set; }
        public BuscarNoConformidadQuery(IdNoConformidadRequest request)
        {
            _request = request;
        }
    }
}
