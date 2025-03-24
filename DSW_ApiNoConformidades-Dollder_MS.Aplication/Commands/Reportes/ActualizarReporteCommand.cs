using DSW_ApiNoConformidades_Dollder_MS.Application.Requests.Reportes;
using DSW_ApiNoConformidades_Dollder_MS.Application.Responses.Reportes;
using MediatR;

namespace DSW_ApiNoConformidades_Dollder_MS.Application.Commands.Reportes
{
    public class ActualizarReporteCommand : IRequest<IdReporteResponse>
    {
        public ReporteRequest _request { get; set; }
        public ActualizarReporteCommand(ReporteRequest request)
        {
            _request = request;
        }
    }
}
