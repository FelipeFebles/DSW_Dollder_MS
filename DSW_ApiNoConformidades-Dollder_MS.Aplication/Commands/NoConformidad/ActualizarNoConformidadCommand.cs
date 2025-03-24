using DSW_ApiNoConformidades_Dollder_MS.Application.Requests.NoConformidad;
using DSW_ApiNoConformidades_Dollder_MS.Application.Responses.NoConformidad;
using MediatR;

namespace DSW_ApiNoConformidades_Dollder_MS.Application.Commands.NoConformidad
{
    public class ActualizarNoConformidadCommand : IRequest<IdNoConformidadResponse>
    {
        public NoConformidadRequest _request { get; set; }
        public ActualizarNoConformidadCommand(NoConformidadRequest request)
        {
            _request = request;
        }
    }
}
