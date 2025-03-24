using DSW_ApiNoConformidades_Dollder_MS.Application.Requests.NoConformidad;
using DSW_ApiNoConformidades_Dollder_MS.Application.Responses.NoConformidad;
using MediatR;


namespace DSW_ApiNoConformidades_Dollder_MS.Aplication.Commands.NoConformidad
{
    public class ActualizarEstadoNoConformidadCommand : IRequest<IdNoConformidadResponse>
    {
        public NoConformidadRequest _request { get; set; }
        public ActualizarEstadoNoConformidadCommand(NoConformidadRequest request)
        {
            _request = request;
        }
    }
}
