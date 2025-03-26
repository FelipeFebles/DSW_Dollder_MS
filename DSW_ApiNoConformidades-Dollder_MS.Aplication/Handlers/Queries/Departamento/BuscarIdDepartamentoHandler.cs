using DSW_ApiNoConformidades_Dollder_MS.Aplication.Queries.Departamento;
using DSW_ApiNoConformidades_Dollder_MS.Application.Responses.Departamento;
using DSW_ApiNoConformidades_Dollder_MS.Infrastructure.Database;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DSW_ApiNoConformidades_Dollder_MS.Aplication.Handlers.Queries.Departamento
{
    public class BuscarIdDepartamentoHandler : IRequestHandler<BuscarIdDepartamentoQuery, DepartamentoResponse>
    {
        private readonly ApiDbContext _dbContext;
        private readonly ILogger<BuscarIdDepartamentoHandler> _logger;
        public BuscarIdDepartamentoHandler(ApiDbContext dbContext, ILogger<BuscarIdDepartamentoHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public Task<DepartamentoResponse> Handle(BuscarIdDepartamentoQuery request, CancellationToken cancellationToken)
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

        private async Task<DepartamentoResponse> HandleAsync(BuscarIdDepartamentoQuery request)
        {

            try
            {
                var departamento = _dbContext.Departamento.Where(c => c.Id == request._request.Data)
                    .Select(c => new DepartamentoResponse // Asigna el departamento correspondiente
                    {
                        id = c.Id,
                        CreatedAt = c.CreatedAt,
                        CreatedBy = c.CreatedBy,
                        UpdatedAt = c.UpdatedAt,
                        UpdatedBy = c.UpdatedBy,
                        estado = c.estado,

                        nombreDepartamento = c.nombre,
                        cargo = c.cargo
                    }).OrderBy(c => c.nombreDepartamento)
                    .FirstOrDefault();


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
