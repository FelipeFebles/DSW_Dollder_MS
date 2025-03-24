using DSW_ApiNoConformidades_Dollder_MS.Aplication.Requests.NoConformidad;
using DSW_ApiNoConformidades_Dollder_MS.Aplication.Responses.Reportes;
using MediatR;

namespace DSW_ApiNoConformidades_Dollder_MS.Aplication.Queries.NoConformidades
{
    public class BuscarNoConformidadCompletaQuery : IRequest<ReporteResponse>
    {
        public IdNoConformidadRequest _request { get; set; }
        public BuscarNoConformidadCompletaQuery(IdNoConformidadRequest request)
        {
            _request = request;
        }
    }
}
