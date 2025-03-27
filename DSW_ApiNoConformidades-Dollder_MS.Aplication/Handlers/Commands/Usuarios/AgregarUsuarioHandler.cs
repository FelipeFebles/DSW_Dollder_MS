using DSW_ApiNoConformidades_Dollder_MS.Application.Commands.Usuario;
using DSW_ApiNoConformidades_Dollder_MS.Application.Mappers.Usuario;
using DSW_ApiNoConformidades_Dollder_MS.Application.Responses.Usuarios;
using DSW_ApiNoConformidades_Dollder_MS.Infrastructure.Database;
using MediatR;
using Microsoft.Extensions.Logging;


namespace DSW_ApiNoConformidades_Dollder_MS.Application.Handlers.Commands.Usuarios
{
    public class AgregarUsuarioHandler : IRequestHandler<AgregarUsuarioCommand, IdUsuarioResponse>
    {
        private readonly ApiDbContext _dbContext;
        private readonly ILogger<AgregarUsuarioHandler> _logger;
        public AgregarUsuarioHandler(ApiDbContext dbContext, ILogger<AgregarUsuarioHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public Task<IdUsuarioResponse> Handle(AgregarUsuarioCommand request, CancellationToken cancellationToken)
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

        private async Task<IdUsuarioResponse> HandleAsync(AgregarUsuarioCommand request)
        {
            var transaccion = _dbContext.BeginTransaction();
            try
            {
                //Agrego el estado de que no esta aceptado
                request._request.estado = false;
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                ///     Pregunto si el usuario ya esta registrado y si el departamento existe
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                var Registrado = _dbContext.Usuario.Count(c => c.correo == request._request.correo);
                if (Registrado > 0)
                {
                    throw new InvalidOperationException("Registro fallido: el usuario ya existe");
                }

                var departamento = _dbContext.Departamento.Where(d => d.Id == request._request.id_departamento).FirstOrDefault();
                if (departamento == null)
                {
                    throw new InvalidOperationException("Registro fallido: el Departamento No existe");
                }
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                ///     Creo el Usuario
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                // Suponiendo que nombre = "Luis", nombre2 = "Felipe" y apellido = "Febles" y apellido2 = "Castro" 

                //Genero el primer Usuario
                request._request.usuario =
                    request._request.primer_nombre.Substring(0, 1) + // Primera letra del nombre
                    request._request.primer_apellido;                // Apellido completo

                //Pregunto si Mi usuario no esta registrado en el sistema = lfebles
                var result = _dbContext.Usuario.Count(c => c.usuario == request._request.usuario);

                if (result > 0) //Si ya estoy registrado agrego la primera letra del segundo nombre lffebles 
                {
                    request._request.usuario =
                        request._request.primer_nombre.Substring(0, 1) + // Primera letra del nombre
                        request._request.segundo_nombre.Substring(0, 1) + // Primera letra del nombre2
                        request._request.primer_apellido;                // Apellido completo
                }

                //Pregunto si Mi usuario no esta registrado en el sistema
                var result2 = _dbContext.Usuario.Count(c => c.usuario == request._request.usuario);

                if (result2 > 0) //Si ya estoy registrado agrego la primera letra del segundo apellido lffeblesC
                {
                    request._request.usuario =
                        request._request.primer_nombre.Substring(0, 1) +    // Primera letra del nombre
                        request._request.segundo_nombre.Substring(0, 1) +   // Primera letra del nombre2
                        request._request.primer_apellido +                  // Apellido completo
                        request._request.segundo_apellido.Substring(0, 1);  // Primera letra del Apellido
                }


                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                ///     Mappeo la entidad para la insercion y le paso los campos / Luego Inserto en la bd
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                if (departamento.nombre.Contains("Calidad")) 
                {
                   
                    // Crear una instancia de CalidadEntity con los datos del request
                    var entity = UsuarioMapper.MapRequestCalidadEntity(request._request);

                    // Agregar el usuario al DbContext
                    _dbContext.Calidad.Add(entity);
                    await _dbContext.SaveEfContextChanges("APP");

                    //Doy commit
                    transaccion.Commit();

                    //Retorno ID
                    return new IdUsuarioResponse(entity.Id);
                }
                if (departamento.nombre.Contains("Regencia") || departamento.nombre.Contains("Dirección"))
                {
                    // Crear una instancia de OperarioEntity con los datos del request
                    var entity = UsuarioMapper.MapRequestRegenciaEntity(request._request);

                    _dbContext.Regencia.Add(entity);
                    await _dbContext.SaveEfContextChanges("APP");

                    //Doy commit
                    transaccion.Commit();

                    //Retorno ID
                    return new IdUsuarioResponse(entity.Id);
                }
                else
                {

                    // Crear una instancia de OperarioEntity con los datos del request
                    var entity = UsuarioMapper.MapRequestOperarioEntity(request._request);

                    _dbContext.Operario.Add(entity);
                    await _dbContext.SaveEfContextChanges("APP");

                    //Doy commit
                    transaccion.Commit();

                    //Retorno ID
                    return new IdUsuarioResponse(entity.Id);

                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error AgregarOperarioHandler.HandleAsync. {Mensaje}", ex.Message);
                throw;
            }



        }
    }
}


