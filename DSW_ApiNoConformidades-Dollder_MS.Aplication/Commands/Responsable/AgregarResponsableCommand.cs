using DSW_ApiNoConformidades_Dollder_MS.Application.Requests.Responsables;
using DSW_ApiNoConformidades_Dollder_MS.Application.Responses.Responsable;
using MediatR;


namespace DSW_ApiNoConformidades_Dollder_MS.Application.Commands.Responsable
{
    public class AgregarResponsableCommand : IRequest<IdResponsableResponse>
    {
        public ResponsableRequest _request { get; set; }
        public AgregarResponsableCommand(ResponsableRequest request)
        {
            _request = request;
        }
    }
}
