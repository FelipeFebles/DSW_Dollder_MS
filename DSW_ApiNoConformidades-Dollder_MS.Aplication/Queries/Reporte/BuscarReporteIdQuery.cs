using MediatR;

namespace DSW_ApiNoConformidades_Dollder_MS.Aplication.Queries.Reporte
{
    public class BuscarReporteIdQuery : IRequest<Guid>
    {
        public Guid _request { get; set; }
        public BuscarReporteIdQuery(Guid request)
        {
            _request = request;
        }
    }
}
