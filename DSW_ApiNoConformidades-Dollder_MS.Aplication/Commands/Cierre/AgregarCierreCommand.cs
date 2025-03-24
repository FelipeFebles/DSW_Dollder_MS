using DSW_ApiNoConformidades_Dollder_MS.Aplication.Requests.Cierre;
using DSW_ApiNoConformidades_Dollder_MS.Aplication.Responses.Cierre;
using MediatR;


namespace DSW_ApiNoConformidades_Dollder_MS.Aplication.Commands.Cierre
{
    public class AgregarCierreCommand : IRequest<IdCierreResponse>
    {
        public CierreRequest _request { get; set; }
        public AgregarCierreCommand(CierreRequest request)
        {
            _request = request;
        }
    }
}
