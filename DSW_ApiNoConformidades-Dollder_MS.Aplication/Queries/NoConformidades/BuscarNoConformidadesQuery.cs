using DSW_ApiNoConformidades_Dollder_MS.Aplication.Responses.NoConformidad;
using MediatR;


namespace DSW_ApiNoConformidades_Dollder_MS.Aplication.Queries.NoConformidades
{
    public class BuscarNoConformidadesQuery: IRequest<List<NoConformidadesResponse>>
    {
    }
}
