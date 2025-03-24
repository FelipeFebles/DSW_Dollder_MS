using DSW_ApiNoConformidades_Dollder_MS.Application.Queries.Usuarios;
using DSW_ApiNoConformidades_Dollder_MS.Application.Responses.Departamento;
using DSW_ApiNoConformidades_Dollder_MS.Application.Responses.Usuarios;
using DSW_ApiNoConformidades_Dollder_MS.Infrastructure.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;


namespace DSW_ApiNoConformidades_Dollder_MS.Application.Handlers.Queries.Usuarios
{
    public class BuscarUsuariosDepartamentoHandler : IRequestHandler<BuscarUsuariosDepartamentoQuery, List<UsuarioResponse>>
    {
        private readonly ApiDbContext _dbContext;
        private readonly ILogger<BuscarUsuariosDepartamentoHandler> _logger;
        public BuscarUsuariosDepartamentoHandler(ApiDbContext dbContext, ILogger<BuscarUsuariosDepartamentoHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public Task<List<UsuarioResponse>> Handle(BuscarUsuariosDepartamentoQuery request, CancellationToken cancellationToken)
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

        private async Task<List<UsuarioResponse>> HandleAsync(BuscarUsuariosDepartamentoQuery request)
        {

            try
            {
                // Obtener el departamento y su usuario asociado
                var departamentoConUsuario = _dbContext.Departamento
                                        .Where(d => d.nombre.ToLower().Contains(request._request.data.ToLower())) // Filtra por el nombre del departamento en minúsculas
                                        .Join(_dbContext.Usuario, // Une con la tabla Usuario
                                                departamento => departamento.Id, // Clave primaria en Departamento
                                                usuario => usuario.departamento_Id, // Clave foránea en Usuario
                                                (departamento, usuario) => new UsuarioResponse // Rellena el response
                                                {
                                                    Id = usuario.Id,
                                                    CreatedAt = usuario.CreatedAt,
                                                    CreatedBy = usuario.CreatedBy,
                                                    UpdatedAt = usuario.UpdatedAt,
                                                    UpdatedBy = usuario.UpdatedBy,

                                                    usuario = usuario.usuario,
                                                    nombre = usuario.nombre,
                                                    apellido = usuario.apellido,
                                                    password = usuario.password,
                                                    correo = usuario.correo,
                                                    discriminator = EF.Property<string>(usuario, "Discriminator"),
                                                    respuesta_de_seguridad = usuario.respuesta_de_seguridad,
                                                    respuesta_de_seguridad2 = usuario.respuesta_de_seguridad2,
                                                    preguntas_de_seguridad = usuario.preguntas_de_seguridad,
                                                    preguntas_de_seguridad2 = usuario.preguntas_de_seguridad2,
                                                    estado = usuario.estado,
                                                    departamento = new DepartamentoResponse // Asigna el departamento correspondiente
                                                    {
                                                        id = departamento.Id,
                                                        CreatedAt = departamento.CreatedAt,
                                                        CreatedBy = departamento.CreatedBy,
                                                        UpdatedAt = departamento.UpdatedAt,
                                                        UpdatedBy = departamento.UpdatedBy,

                                                        nombreDepartamento = departamento.nombre,
                                                        cargo = departamento.cargo
                                                    }
                                                }
                                        ).ToList(); // Obtiene la lista de usuarios asociados al departamento



                if (departamentoConUsuario == null)
                {
                    // Manejar el caso en que no se encuentre el departamento
                    throw new ArgumentNullException(nameof(request));
                }


                return departamentoConUsuario;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error ConsultarUsuarioIdQueryHandler.HandleAsync. {Mensaje}", ex.Message);
                throw;
            }
        }
    }
}
