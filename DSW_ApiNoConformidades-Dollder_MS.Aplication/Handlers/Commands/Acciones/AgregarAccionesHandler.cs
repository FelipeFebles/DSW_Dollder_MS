﻿using DSW_ApiNoConformidades_Dollder_MS.Application.Commands.Acciones;
using DSW_ApiNoConformidades_Dollder_MS.Application.Mappers.Acciones;
using DSW_ApiNoConformidades_Dollder_MS.Application.Mappers.Notificacion;
using DSW_ApiNoConformidades_Dollder_MS.Application.Mappers.R_Acciones_Usuario;
using DSW_ApiNoConformidades_Dollder_MS.Application.Requests.Notificacion;
using DSW_ApiNoConformidades_Dollder_MS.Application.Responses.Acciones;
using DSW_ApiNoConformidades_Dollder_MS.Infrastructure.Database;
using DSW_ApiNoConformidades_Dollder_MS.Infrastructure.Servicio;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DSW_ApiNoConformidades_Dollder_MS.Application.Handlers.Commands.Acciones
{
    public class AgregarAccionesHandler : IRequestHandler<AgregarAccionesCommand, IdAccionesResponse>
    {
        private readonly ApiDbContext _dbContext;
        private readonly ILogger<AgregarAccionesHandler> _logger;
        private readonly Correo correo = new Correo();

        public AgregarAccionesHandler(ApiDbContext dbContext, ILogger<AgregarAccionesHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }
        public Task<IdAccionesResponse> Handle(AgregarAccionesCommand request, CancellationToken cancellationToken)
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

        private async Task<IdAccionesResponse> HandleAsync(AgregarAccionesCommand request)
        {
            var transaccion = _dbContext.BeginTransaction();
            try
            {
                request._request.estado = false;
                request._request.visto_bueno = false;

                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                ///     Genero la notificacion
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                var usuario = _dbContext.Usuario.Where(u => u.Id == request._request.usuario_Id).FirstOrDefault();
                var dep = _dbContext.Departamento.Where(d => d.Id == usuario.departamento_Id).FirstOrDefault();

                request._request.cargo_usuario = dep.cargo;
                request._request.area = usuario.departamento.cargo;
                var responsable = _dbContext.Responsable.Where(r => r.Id == request._request.responsable_Id).FirstOrDefault();
                var envia = _dbContext.Usuario.Where(u => u.Id == responsable.usuario_Id).FirstOrDefault();

                var nc = _dbContext.NoConformidad.Where(n=> n.Id == responsable.noConformidad_Id).FirstOrDefault();


                var notificacion = NotificacionMapper.MapRequestNotificacionEntity(new NotificacionRequest("Accion correctiva/preventiva generada", envia.nombre + " " + envia.apellido, usuario.correo, "Se ha asignado una Accion de la no conformidad: "+ nc.numero_expedicion, false, "Acciones" ));
                _dbContext.Notificacion.Add(notificacion);
                await _dbContext.SaveEfContextChanges("APP");


                correo.EnviaCorreoUsuario(usuario.correo, "Accion correctiva/preventiva generada", "Se ha asignado una Accion");


                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                ///     Crear una instancia de Responsable con los datos del request
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                if (request._request.correctivas_Id != null)
                {
                    var entity = AccionesMapper.MapPreventivasEntity(request._request);

                    // Agregar la accion preventiva al DbContext
                    _dbContext.Preventivas.Add(entity);
                    await _dbContext.SaveEfContextChanges("APP");

                    var relacion = R_Acciones_UsuarioMapper.MapR_Acciones_UsuarioEntity(entity.Id, (Guid)request._request.usuario_Id);
                    _dbContext.R_Acciones_Usuario.Add(relacion);
                    await _dbContext.SaveEfContextChanges("APP");
                    transaccion.Commit();
                    return new IdAccionesResponse(entity.Id);
                }
                else
                {
                    var entity = AccionesMapper.MapCorrectivasEntity(request._request);

                    // Agregar el usuario al DbContext
                    _dbContext.Correctivas.Add(entity);
                    await _dbContext.SaveEfContextChanges("APP");

                    var relacion = R_Acciones_UsuarioMapper.MapR_Acciones_UsuarioEntity(entity.Id, (Guid)request._request.usuario_Id);
                    _dbContext.R_Acciones_Usuario.Add(relacion);
                    await _dbContext.SaveEfContextChanges("APP");

                    transaccion.Commit();
                    return new IdAccionesResponse(entity.Id);
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

