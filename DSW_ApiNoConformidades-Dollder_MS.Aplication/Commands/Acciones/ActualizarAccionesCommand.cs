using DSW_ApiNoConformidades_Dollder_MS.Application.Requests.AccionesCorrectivas;
using DSW_ApiNoConformidades_Dollder_MS.Application.Responses.Acciones;
using MediatR;


namespace DSW_ApiNoConformidades_Dollder_MS.Aplication.Commands.Acciones
{
    public class ActualizarAccionesCommand : IRequest<IdAccionesResponse>
    {
        public AccionesRequest _request { get; set; }
        public ActualizarAccionesCommand(AccionesRequest request)
        {
            _request = request;
        }
    }
}
