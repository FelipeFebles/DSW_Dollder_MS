using DSW_ApiNoConformidades_Dollder_MS.Aplication.Queries.Departamento;
using DSW_ApiNoConformidades_Dollder_MS.Application.Responses.Departamento;
using DSW_ApiNoConformidades_Dollder_MS.Infrastructure.Database;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DSW_ApiNoConformidades_Dollder_MS.Aplication.Handlers.Queries.Departamento
{
    public class BuscarDepartamentoHandler : IRequestHandler<BuscarDepartamentoQuery, List<DepartamentoResponse>>
    {
        private readonly ApiDbContext _dbContext;
        private readonly ILogger<BuscarDepartamentoHandler> _logger;
        public BuscarDepartamentoHandler(ApiDbContext dbContext, ILogger<BuscarDepartamentoHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public Task<List<DepartamentoResponse>> Handle(BuscarDepartamentoQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (request is null) //Pregunto si el request es nulo
                {
                    _logger.LogWarning("BuscarUsuariosCorreoHandler.Handle: Request nulo.");
                    throw new ArgumentNullException(nameof(request));

                }
                else
                {
                    return HandleAsync(request);
                }
            }
            catch (Exception)
            {
                _logger.LogWarning("BuscarUsuariosCorreoHandler.Handle: ArgumentNullException");
                throw;
            }
        }

        private async Task<List<DepartamentoResponse>> HandleAsync(BuscarDepartamentoQuery request)
        {

            try
            {
                var departamento = _dbContext.Departamento
                    .Select(c => new DepartamentoResponse // Asigna el departamento correspondiente
                    {
                        id = c.Id,
                        CreatedAt = c.CreatedAt,
                        CreatedBy = c.CreatedBy,
                        UpdatedAt = c.UpdatedAt,
                        UpdatedBy = c.UpdatedBy,

                        nombreDepartamento = c.nombre,
                        cargo = c.cargo
                    }).OrderBy(c=> c.nombreDepartamento)
                    .ToList();


                return departamento; //Retorno la lista
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error BuscarUsuariosCorreoHandler.HandleAsync. {Mensaje}", ex.Message);
                throw;
            }

        }
    }
}
