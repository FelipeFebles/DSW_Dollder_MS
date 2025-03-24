using DSW_ApiNoConformidades_Dollder_MS.Aplication.Responses.RevisionReporte;
using DSW_ApiNoConformidades_Dollder_MS.Application.Requests.RevisionReporte;
using MediatR;

namespace DSW_ApiNoConformidades_Dollder_MS.Aplication.Queries.Reporte
{
    public class BuscarRevisionReporteQuery : IRequest<RevisionReporteResponse>
    {
        public RevisionReporteRequest _request { get; set; }
        public BuscarRevisionReporteQuery(RevisionReporteRequest request)
        {
            _request = request;
        }
    }
}
