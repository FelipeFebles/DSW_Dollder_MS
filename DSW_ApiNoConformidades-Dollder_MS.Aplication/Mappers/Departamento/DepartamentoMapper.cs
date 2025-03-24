using DSW_ApiNoConformidades_Dollder_MS.Application.Requests.Departamento;
using DSW_ApiNoConformidades_Dollder_MS.Core.Entities;


namespace DSW_ApiNoConformidades_Dollder_MS.Application.Mappers.Departamento
{
    public class DepartamentoMapper
    {
        public static DepartamentoEntity MapRequestDepartamentoEntity(DepartamentoRequest request)
        {
            var entity = new DepartamentoEntity()
            {
                nombre = request.nombre,
                cargo = request.cargo,
            };
            return entity;
        }

    }
}
