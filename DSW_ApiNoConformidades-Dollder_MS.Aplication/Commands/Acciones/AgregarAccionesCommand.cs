using DSW_ApiNoConformidades_Dollder_MS.Application.Requests.AccionesCorrectivas;
using DSW_ApiNoConformidades_Dollder_MS.Application.Responses.Acciones;
using MediatR;

namespace DSW_ApiNoConformidades_Dollder_MS.Application.Commands.Acciones
{
    public class AgregarAccionesCommand : IRequest<IdAccionesResponse>
    {
        public AccionesRequest _request { get; set; }
        public AgregarAccionesCommand(AccionesRequest request)
        {
            _request = request;
        }
    }
}
