using DSW_ApiNoConformidades_Dollder_MS.Aplication.Responses.Reportes;
using DSW_ApiNoConformidades_Dollder_MS.Application.Requests.Reportes;
using MediatR;

namespace DSW_ApiNoConformidades_Dollder_MS.Aplication.Queries.Reporte
{
    public class BuscarReporteQuery : IRequest<ReporteResponse>
    {
        public ReporteRequest _request { get; set; }
        public BuscarReporteQuery(ReporteRequest request)
        {
            _request = request;
        }
    }
}
