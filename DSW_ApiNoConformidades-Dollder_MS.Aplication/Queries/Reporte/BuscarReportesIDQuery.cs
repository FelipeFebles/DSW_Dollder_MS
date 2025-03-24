using DSW_ApiNoConformidades_Dollder_MS.Aplication.Responses.Reportes;
using DSW_ApiNoConformidades_Dollder_MS.Application.Requests;
using DSW_ApiNoConformidades_Dollder_MS.Application.Requests.Reportes;
using MediatR;

namespace DSW_ApiNoConformidades_Dollder_MS.Aplication.Queries.Reporte
{
    public class BuscarReportesIDQuery : IRequest<List<ReporteResponse>>
    {
        public BuscarUsuarioIDRequest _request { get; set; }
        public BuscarReportesIDQuery(BuscarUsuarioIDRequest request)
        {
            _request = request;
        }
    }
}
