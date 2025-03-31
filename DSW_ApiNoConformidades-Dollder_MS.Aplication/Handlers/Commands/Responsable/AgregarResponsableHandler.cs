using DSW_ApiNoConformidades_Dollder_MS.Application.Commands.NoConformidad;
using DSW_ApiNoConformidades_Dollder_MS.Application.Commands.Responsable;
using DSW_ApiNoConformidades_Dollder_MS.Application.Mappers.Notificacion;
using DSW_ApiNoConformidades_Dollder_MS.Application.Mappers.Responsable;
using DSW_ApiNoConformidades_Dollder_MS.Application.Requests.NoConformidad;
using DSW_ApiNoConformidades_Dollder_MS.Application.Requests.Notificacion;
using DSW_ApiNoConformidades_Dollder_MS.Application.Responses.Responsable;
using DSW_ApiNoConformidades_Dollder_MS.Infrastructure.Database;
using DSW_ApiNoConformidades_Dollder_MS.Infrastructure.Servicio;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DSW_ApiNoConformidades_Dollder_MS.Application.Handlers.Commands.Responsable
{
    public class AgregarResponsableHandler : IRequestHandler<AgregarResponsableCommand, IdResponsableResponse>
    {
        private readonly ApiDbContext _dbContext;
        private readonly ILogger<AgregarResponsableHandler> _logger;
        private readonly MediatR.IMediator _mediator;
        private readonly Correo correo = new Correo();

        public AgregarResponsableHandler(ApiDbContext dbContext, ILogger<AgregarResponsableHandler> logger, MediatR.IMediator mediator)
        {
            _dbContext = dbContext;
            _logger = logger;
            _mediator = mediator;
        }

        public Task<IdResponsableResponse> Handle(AgregarResponsableCommand request, CancellationToken cancellationToken)
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

        private async Task<IdResponsableResponse> HandleAsync(AgregarResponsableCommand request)
        {
            var transaccion = _dbContext.BeginTransaction();
            try
            {
                // Crear una instancia de Responsable con los datos del request
                var usuario = _dbContext.Usuario.Where(c => c.Id == request._request.usuario_Id).FirstOrDefault();
                var departamento = _dbContext.Departamento.Where(c=> c.Id == usuario.departamento_Id).FirstOrDefault();
                request._request.cargo_usuario = departamento.cargo;

                var entity = ResponsableMapper.MapResponsableMapperEntity(request._request);

                // Agregar el usuario al DbContext
                _dbContext.Responsable.Add(entity);
                await _dbContext.SaveEfContextChanges("APP");

                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                ///     Genero la notificacion
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                var noConformidad = _dbContext.NoConformidad.Where(n => n.Id == request._request.noConformidad_Id).FirstOrDefault();

                var reporte = _dbContext.Reporte.Where(r => r.Id == noConformidad.reporte_Id).FirstOrDefault();

                var notificacion = NotificacionMapper.MapRequestNotificacionEntity(new NotificacionRequest(reporte.titulo, "Garantia de calidad", usuario.correo, "Se ha diferido una no conformidad a su departamento", false, "Responsables"));
                _dbContext.Notificacion.Add(notificacion);
                await _dbContext.SaveEfContextChanges("APP");
                transaccion.Commit();

                correo.EnviaCorreoUsuario(usuario.correo, "Garantia de calidad", "Se ha diferido una no conformidad a su departamento");




                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                ///     Actualizo el campo de responsables_cargo del usuario agregando al nuevo usuario
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                var NC = new NoConformidadRequest();
                NC.Id = request._request.noConformidad_Id;
                NC.responsables_cargo = noConformidad.responsables_cargo;
                NC.responsables_cargo.Add(usuario.nombre + " " + usuario.apellido + "-" + usuario.departamento.cargo);

                var command = new ActualizarNoConformidadCommand(NC);
                var response = await _mediator.Send(command);
                //Retorno ID
                return new IdResponsableResponse(entity.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error AgregarOperarioHandler.HandleAsync. {Mensaje}", ex.Message);
                throw;
            }

        }
    }
}
