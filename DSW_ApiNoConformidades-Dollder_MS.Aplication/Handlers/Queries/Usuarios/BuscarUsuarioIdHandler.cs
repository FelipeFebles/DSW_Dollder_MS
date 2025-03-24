﻿using DSW_ApiNoConformidades_Dollder_MS.Application.Queries.Usuarios;
using DSW_ApiNoConformidades_Dollder_MS.Application.Responses.Departamento;
using DSW_ApiNoConformidades_Dollder_MS.Application.Responses.Usuarios;
using DSW_ApiNoConformidades_Dollder_MS.Infrastructure.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DSW_ApiNoConformidades_Dollder_MS.Application.Handlers.Queries.Usuarios
{
    public class BuscarUsuarioIdHandler : IRequestHandler<BuscarUsuarioIdQuery, UsuarioResponse>
    {
        private readonly ApiDbContext _dbContext;
        private readonly ILogger<BuscarUsuarioIdHandler> _logger;
        public BuscarUsuarioIdHandler(ApiDbContext dbContext, ILogger<BuscarUsuarioIdHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public Task<UsuarioResponse> Handle(BuscarUsuarioIdQuery request, CancellationToken cancellationToken)
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

        private async Task<UsuarioResponse> HandleAsync(BuscarUsuarioIdQuery request)
        {

            try
            {
                _logger.LogInformation("ConsultarUsuarioIdQueryHandler.HandleAsync");
                var result = _dbContext.Usuario.Count(c => c.Id == request._request.data);

                if (result == 0) //Verifico que el Usuario exista
                {
                    throw new InvalidOperationException("No se encontro al usuario registrado");
                }

                // Realizar una consulta que una Usuario y Departamento


                var usuario = _dbContext.Usuario.Where(c => c.Id == request._request.data)// Filtra por el nombre del usuario
                    .Select(c => new UsuarioResponse // Rellena el response
                    {
                        Id = c.Id,
                        CreatedAt = c.CreatedAt,
                        CreatedBy = c.CreatedBy,
                        UpdatedAt = c.UpdatedAt,
                        UpdatedBy = c.UpdatedBy,

                        usuario = c.usuario,
                        nombre = c.nombre,
                        apellido = c.apellido,
                        password = c.password,
                        correo = c.correo,
                        discriminator = EF.Property<string>(c, "Discriminator"),
                        respuesta_de_seguridad = c.respuesta_de_seguridad,
                        respuesta_de_seguridad2 = c.respuesta_de_seguridad2,
                        preguntas_de_seguridad = c.preguntas_de_seguridad,
                        preguntas_de_seguridad2 = c.preguntas_de_seguridad2,
                        estado = c.estado,
                        departamento = new DepartamentoResponse // Asigna el departamento correspondiente
                        {
                            id = c.departamento.Id,
                            CreatedAt = c.departamento.CreatedAt,
                            CreatedBy = c.departamento.CreatedBy,
                            UpdatedAt = c.departamento.UpdatedAt,
                            UpdatedBy = c.departamento.UpdatedBy,

                            nombreDepartamento = c.departamento.nombre,
                            cargo = c.departamento.cargo
                        }
                    }).FirstOrDefault();
                    
                    return usuario; //Retorno la lista

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error ConsultarUsuarioIdQueryHandler.HandleAsync. {Mensaje}", ex.Message);
                throw;
            }



        }
    }
}

