using DSW_ApiNoConformidades_Dollder_MS.Aplication.Requests.Seguimiento;
using DSW_ApiNoConformidades_Dollder_MS.Aplication.Responses.Seguimiento;
using MediatR;

namespace DSW_ApiNoConformidades_Dollder_MS.Aplication.Commands.Seguimiento
{
    public class ActualizarSeguimientoCommand : IRequest<IdSeguimientoReponse>
    {
        public SeguimientoRequest _request { get; set; }
        public ActualizarSeguimientoCommand(SeguimientoRequest request)
        {
            _request = request;
        }
    }
}
