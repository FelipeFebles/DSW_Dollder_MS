using DSW_ApiNoConformidades_Dollder_MS.Aplication.Commands.Departamento;
using DSW_ApiNoConformidades_Dollder_MS.Aplication.Responses.Departamento;
using DSW_ApiNoConformidades_Dollder_MS.Application.Requests.Departamento;
using DSW_ApiNoConformidades_Dollder_MS.Core.Entities;
using DSW_ApiNoConformidades_Dollder_MS.Infrastructure.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DSW_ApiNoConformidades_Dollder_MS.Aplication.Handlers.Commands.Departamento
{
    public class ActualizarDepartamentoHandler : IRequestHandler<ActualizarDepartamentoCommand, IdDepartamentoResponse>
    {
        private readonly ApiDbContext _dbContext;
        private readonly ILogger<ActualizarDepartamentoHandler> _logger;
        public ActualizarDepartamentoHandler(ApiDbContext dbContext, ILogger<ActualizarDepartamentoHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public Task<IdDepartamentoResponse> Handle(ActualizarDepartamentoCommand request, CancellationToken cancellationToken)
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

        private async Task<IdDepartamentoResponse> HandleAsync(ActualizarDepartamentoCommand request)
        {
            var transaccion = _dbContext.BeginTransaction();
            try
            {
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                ///     Pregunto si el Departamento existe
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                var Departamento = _dbContext.Departamento.FirstOrDefault(c => c.Id == request._request.Id);
                if (Departamento == null)
                {
                    throw new InvalidOperationException("Registro fallido: el Departamento NO existe");
                }
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                ///     Actualizo la Departamento
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                // Obtener las propiedades de DepartamentoEntity y DepartamentoRequest
                var propiedadesDepartamento = typeof(DepartamentoEntity).GetProperties();
                var propiedadesRequest = typeof(DepartamentoRequest).GetProperties();

                // Actualizar propiedades solo si no están vacías o nulas
                foreach (var propRequest in propiedadesRequest)
                {
                    var valor = propRequest.GetValue(request._request);
                    if (valor != null)
                    {
                        // Buscar la propiedad correspondiente en DepartamentoEntity
                        var propDepartamento = propiedadesDepartamento.FirstOrDefault(p => p.Name.Equals(propRequest.Name, StringComparison.OrdinalIgnoreCase));

                        if (propDepartamento != null && propDepartamento.CanWrite)
                        {
                            // Actualizar el valor de la propiedad en el Departamento
                            propDepartamento.SetValue(Departamento, valor);
                        }
                    }
                }

                // Guardar cambios
                _dbContext.ChangeEntityState(Departamento, EntityState.Modified);
                await _dbContext.SaveEfContextChanges("APP");
                transaccion.Commit();

                _logger.LogInformation("Departamento actualizado correctamente. ID: {DepartamentoId}", Departamento.Id);
                return new IdDepartamentoResponse(Departamento.Id);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error AgregarOperarioHandler.HandleAsync. {Mensaje}", ex.Message);
                throw;
            }

        }
    }
}