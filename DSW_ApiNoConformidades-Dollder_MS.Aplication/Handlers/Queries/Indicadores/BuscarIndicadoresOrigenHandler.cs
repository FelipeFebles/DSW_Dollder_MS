﻿using DSW_ApiNoConformidades_Dollder_MS.Aplication.Handlers.Queries.NoConformidades;
using DSW_ApiNoConformidades_Dollder_MS.Aplication.Queries.Indicadores;
using DSW_ApiNoConformidades_Dollder_MS.Aplication.Queries.NoConformidades;
using DSW_ApiNoConformidades_Dollder_MS.Aplication.Responses.Indicadores;
using DSW_ApiNoConformidades_Dollder_MS.Aplication.Responses.NoConformidad;
using DSW_ApiNoConformidades_Dollder_MS.Infrastructure.Database;
using MediatR;
using Microsoft.Extensions.Logging;


namespace DSW_ApiNoConformidades_Dollder_MS.Aplication.Handlers.Queries.Indicadores
{
    internal class BuscarIndicadoresOrigenHandler : IRequestHandler<BuscarIndicadoresOrigenQuery, List<IndicadorOrigenResponse>>
    {
        private readonly ApiDbContext _dbContext;
        private readonly ILogger<BuscarIndicadoresOrigenHandler> _logger;
        public BuscarIndicadoresOrigenHandler(ApiDbContext dbContext, ILogger<BuscarIndicadoresOrigenHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public Task<List<IndicadorOrigenResponse>> Handle(BuscarIndicadoresOrigenQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (request is null) //Pregunto si el request es nulo
                {
                    _logger.LogWarning("ConsultarUsuarioIdQueryHandler.Handle: Request nulo.");
                    throw new ArgumentNullException(nameof(request));

                }
                else
                {
                    return HandleAsync(request);
                }
            }
            catch (Exception)
            {
                _logger.LogWarning("ConsultarUsuariosQueryHandler.Handle: ArgumentNullException");
                throw;
            }
        }

        private async Task<List<IndicadorOrigenResponse>> HandleAsync(BuscarIndicadoresOrigenQuery request)
        {
            try
            {
                _logger.LogInformation("ConsultarUsuarioIdQueryHandler.HandleAsync");

                // Crear una lista para almacenar los resultados
                var list = _dbContext.IndicadorOrigen
                    .Select(x => new IndicadorOrigenResponse
                    {
                        Id = x.Id,
                        CreatedAt = x.CreatedAt,
                        CreatedBy = x.CreatedBy,
                        UpdatedAt = x.UpdatedAt,
                        UpdatedBy = x.UpdatedBy,
                        origen = x.origen,

                    })
                    .ToList();


                // Retornar la lista de no conformidades
                return list;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en ConsultarUsuarioIdQueryHandler.HandleAsync");
                throw; // Relanzar la excepción para que sea manejada en un nivel superior
            }
        }
    }
}


