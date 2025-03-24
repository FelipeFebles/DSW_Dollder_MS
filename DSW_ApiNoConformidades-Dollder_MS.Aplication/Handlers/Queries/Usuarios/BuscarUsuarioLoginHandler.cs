using DSW_ApiNoConformidades_Dollder_MS.Aplication.Queries.Usuarios;
using DSW_ApiNoConformidades_Dollder_MS.Application.Responses.Departamento;
using DSW_ApiNoConformidades_Dollder_MS.Application.Responses.Usuarios;
using DSW_ApiNoConformidades_Dollder_MS.Infrastructure.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DSW_ApiNoConformidades_Dollder_MS.Aplication.Handlers.Queries.Usuarios
{
    public class BuscarUsuarioLoginHandler : IRequestHandler<BuscarLoginQuery, UsuarioResponse>
    {
        private readonly ApiDbContext _dbContext;
        private readonly ILogger<BuscarUsuarioLoginHandler> _logger;
        public BuscarUsuarioLoginHandler(ApiDbContext dbContext, ILogger<BuscarUsuarioLoginHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public Task<UsuarioResponse> Handle(BuscarLoginQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (request is null) //Pregunto si el request es nulo
                {
                    _logger.LogWarning("BuscarLoginHandler.Handle: Request nulo.");
                    throw new ArgumentNullException(nameof(request));

                }
                else
                {
                    return HandleAsync(request);
                }
            }
            catch (Exception)
            {
                _logger.LogWarning("BuscarLoginHandler.Handle: ArgumentNullException");
                throw;
            }
        }

        private async Task<UsuarioResponse> HandleAsync(BuscarLoginQuery request)
        {

            try
            {
                _logger.LogInformation("BuscarLoginHandler.HandleAsync");
                var result = _dbContext.Usuario.Where(c => c.usuario == request._request.data || c.correo == request._request.data).FirstOrDefault();

                if (result == null) //Verifico que el Usuario exista
                {
                    throw new InvalidOperationException("No se encontro al usuario o el correo registrado");
                }
                if (result.estado == false) //Verifico no este registrado
                {
                    throw new InvalidOperationException("El usuario no esta activo en el sistema");
                }
                var pass = _dbContext.Usuario.Count(c => c.password == request._request.pass);

                if (pass == 0) //Verifico que la contraseña exista
                {
                    throw new InvalidOperationException("La contraseña es erronea");
                } 

                // Realizar una consulta que una Usuario y Departamento
                var usuario = _dbContext.Usuario.Where(c => c.usuario == request._request.data || c.correo == request._request.data)// Filtra por el nombre del usuario
                    .Select(c => new UsuarioResponse // Rellena el response
                    {
                        Id = c.Id,
                        usuario = c.usuario,
                        discriminator = EF.Property<string>(c, "Discriminator"),
                        estado = c.estado,
                        departamento = new DepartamentoResponse // Asigna el departamento correspondiente
                        {
                            id = c.departamento.Id,
                        }
                    }).FirstOrDefault();

                return usuario; //Retorno la lista

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error BuscarLoginHandler.HandleAsync. {Mensaje}", ex.Message);
                throw;
            }



        }
    }
}
