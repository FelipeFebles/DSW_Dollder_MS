using DSW_ApiNoConformidades_Dollder_MS.Aplication.Mappers.Reporte;
using DSW_ApiNoConformidades_Dollder_MS.Application.Commands.Reportes;
using DSW_ApiNoConformidades_Dollder_MS.Application.Mappers.Notificacion;
using DSW_ApiNoConformidades_Dollder_MS.Application.Mappers.Reporte;
using DSW_ApiNoConformidades_Dollder_MS.Application.Mappers.RevisionReporte;
using DSW_ApiNoConformidades_Dollder_MS.Application.Requests.Notificacion;
using DSW_ApiNoConformidades_Dollder_MS.Application.Requests.RevisionReporte;
using DSW_ApiNoConformidades_Dollder_MS.Application.Responses.Reportes;
using DSW_ApiNoConformidades_Dollder_MS.Application.Responses.Usuarios;
using DSW_ApiNoConformidades_Dollder_MS.Core.Entities;
using DSW_ApiNoConformidades_Dollder_MS.Infrastructure.Database;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DSW_ApiNoConformidades_Dollder_MS.Application.Handlers.Commands.Reportes
{
    public class AgregarReporteHandler : IRequestHandler<AgregarReporteCommand, IdReporteResponse>
    {
        private readonly ApiDbContext _dbContext;
        private readonly ILogger<AgregarReporteHandler> _logger;
        public AgregarReporteHandler(ApiDbContext dbContext, ILogger<AgregarReporteHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }


        public Task<IdReporteResponse> Handle(AgregarReporteCommand request, CancellationToken cancellationToken)
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

        private async Task<IdReporteResponse> HandleAsync(AgregarReporteCommand request)
        {
            var transaccion = _dbContext.BeginTransaction();
            try
            {
                request._request.estado = false;
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                ///     Busco el nombre del departamento
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                var dep_usuario = _dbContext.Usuario
                                            .Where(u => u.Id == request._request.id_usuario) // Filtra por el Id del usuario
                                            .Select(u => new UsuarioEntity
                                            {
                                                nombre = u.nombre,
                                                apellido = u.apellido,
                                                departamento = u.departamento
                                            })
                                            .FirstOrDefault(); // Obtiene el primer resultado
                request._request.departamento_emisor = dep_usuario.departamento.nombre;
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                ///     Busco al usuario gerente/jefe/coordinador de ese departamento
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                var gerente = BuscarRevisor(dep_usuario);

                //Agrego el reporte

                var reporte = ReporteMapper.MapRequestReporteEntity(request._request, (Guid)request._request.id_usuario);
                _dbContext.Reporte.Add(reporte);
                await _dbContext.SaveEfContextChanges("APP");

                // Agregar el RevisionReporte al DbContext

                var revisionRequest = new RevisionReporteRequest();
                revisionRequest.nombre = gerente.nombre + " " + gerente.apellido;

                var revision = RevisionReporteMapper.MapRequestRevisionReporteEntity(revisionRequest, reporte, (Guid)gerente.Id);
                _dbContext.RevisionReporte.Add(revision);
                await _dbContext.SaveEfContextChanges("APP");

                //Agregar la imagen
                if(request._request.imagenes!=null && request._request.imagenes.Count > 0)
                {
                    foreach (var img in request._request.imagenes)
                    {
                        var imagenes = ImagenReporteMapper.MapRequestImagenReporteEntity(img, reporte.Id);
                        _dbContext.ImagenReporte.Add(imagenes);
                        await _dbContext.SaveEfContextChanges("APP");
                    }
                    
                }

                //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                /////     Genero la notificacion
                //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                var notificacio = NotificacionMapper.MapRequestNotificacionEntity(new NotificacionRequest(request._request.titulo, dep_usuario.nombre + " " + dep_usuario.apellido, gerente.correo, "Se ha generado una nuevo reporte en el area " + request._request.area, false, "Reportes"));
                _dbContext.Notificacion.Add(notificacio);
                await _dbContext.SaveEfContextChanges("APP");
                transaccion.Commit();


                return new IdReporteResponse(reporte.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error AgregarOperarioHandler.HandleAsync. {Mensaje}", ex.Message);
                throw;
            }

        }


        public UsuarioResponse BuscarRevisor(UsuarioEntity dep_usuario) 
        {

            var gerente = _dbContext.Usuario
                                        .Where(u => u.departamento.nombre == dep_usuario.departamento.nombre && u.departamento.cargo.Contains("Gerente"))
                                        .Select(u => new UsuarioResponse
                                        {
                                            Id = u.Id,
                                            correo = u.correo,
                                            nombre = u.nombre,
                                            apellido = u.apellido,
                                        }).FirstOrDefault();

            if (gerente == null)
            {
                var jefe = _dbContext.Usuario
                                        .Where(u => u.departamento.nombre == dep_usuario.departamento.nombre && u.departamento.cargo.Contains("Jefe"))
                                        .Select(u => new UsuarioResponse
                                        {
                                            Id = u.Id,
                                            correo = u.correo,
                                            nombre = u.nombre,
                                            apellido = u.apellido,
                                        }).FirstOrDefault();
                if (jefe == null)
                {
                    var coordinador = _dbContext.Usuario
                                            .Where(u => u.departamento.nombre == dep_usuario.departamento.nombre && u.departamento.cargo.Contains("Coordinador"))
                                            .Select(u => new UsuarioResponse
                                            {
                                                Id = u.Id,
                                                correo = u.correo,
                                                nombre = u.nombre,
                                                apellido = u.apellido,
                                            }).FirstOrDefault();

                    return coordinador;
                }

                return jefe;
            }

            return gerente;
        }

    }

}