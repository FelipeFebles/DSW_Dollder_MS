using DSW_ApiNoConformidades_Dollder_MS.Aplication.Commands.Departamento;
using DSW_ApiNoConformidades_Dollder_MS.Aplication.Responses.Departamento;
using DSW_ApiNoConformidades_Dollder_MS.Application.Mappers.Departamento;
using DSW_ApiNoConformidades_Dollder_MS.Infrastructure.Database;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DSW_ApiNoConformidades_Dollder_MS.Aplication.Handlers.Commands.Departamentos
{
    internal class AgregarDepartamentoHandler : IRequestHandler<AgregarDepartamentoCommand, IdDepartamentoResponse>
    {
        private readonly ApiDbContext _dbContext;
        private readonly ILogger<AgregarDepartamentoHandler> _logger;
        public AgregarDepartamentoHandler(ApiDbContext dbContext, ILogger<AgregarDepartamentoHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public Task<IdDepartamentoResponse> Handle(AgregarDepartamentoCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request is null) //Pregunto si el request es nulo
                {
                    _logger.LogWarning("AgregarOperarioHandler.Handle: Request nulo.");
                    throw new ArgumentNullException(nameof(request));

                }
                else
                {
                    return HandleAsync(request);
                }
            }
            catch (Exception)
            {
                _logger.LogWarning("AgregarOperarioHandler.Handle: ArgumentNullException");
                throw;
            }
        }

        private async Task<IdDepartamentoResponse> HandleAsync(AgregarDepartamentoCommand request)
        {
            var transaccion = _dbContext.BeginTransaction();
            try
            {
                var departamento = _dbContext.Departamento.Count(x => x.nombre == request._request.nombre && x.cargo == request._request.cargo);
                if (departamento>0) 
                {
                    throw new InvalidOperationException("Registro fallido: el Departamento YA existe");
                }
                // Crear una instancia de Responsable con los datos del request
                var entity = DepartamentoMapper.MapRequestDepartamentoEntity(request._request);

                // Agregar el usuario al DbContext
                _dbContext.Departamento.Add(entity);
                await _dbContext.SaveEfContextChanges("APP");

                transaccion.Commit();
                //Retorno ID
                return new IdDepartamentoResponse(entity.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error AgregarOperarioHandler.HandleAsync. {Mensaje}", ex.Message);
                throw;
            }

        }
    }
}
