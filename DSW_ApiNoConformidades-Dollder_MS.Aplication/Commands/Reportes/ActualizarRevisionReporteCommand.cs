using DSW_ApiNoConformidades_Dollder_MS.Application.Requests.RevisionReporte;
using DSW_ApiNoConformidades_Dollder_MS.Application.Responses.RevisionReporte;
using MediatR;

namespace DSW_ApiNoConformidades_Dollder_MS.Application.Commands.Reportes
{
    public class ActualizarRevisionReporteCommand : IRequest<IdRevisionReporteResponse>
    {
        public RevisionReporteRequest _request { get; set; }
        public ActualizarRevisionReporteCommand(RevisionReporteRequest request)
        {
            _request = request;
        }
    }
}
