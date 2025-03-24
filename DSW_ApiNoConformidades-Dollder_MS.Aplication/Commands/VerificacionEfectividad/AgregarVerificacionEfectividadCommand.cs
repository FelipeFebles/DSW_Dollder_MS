using DSW_ApiNoConformidades_Dollder_MS.Aplication.Requests.VerificacionEfectividad;
using DSW_ApiNoConformidades_Dollder_MS.Aplication.Responses.VerificacionEfectividad;
using MediatR;


namespace DSW_ApiNoConformidades_Dollder_MS.Aplication.Commands.VerificacionEfectividad
{
    public class AgregarVerificacionEfectividadCommand : IRequest<IdVerificacionEfectividadResponse>
    {
        public VerificacionEfectividadRequest _request { get; set; }
        public AgregarVerificacionEfectividadCommand(VerificacionEfectividadRequest request)
        {
            _request = request;
        }
    }
}
