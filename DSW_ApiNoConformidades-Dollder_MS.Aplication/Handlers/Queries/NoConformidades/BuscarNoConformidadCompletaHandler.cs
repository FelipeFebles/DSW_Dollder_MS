using DSW_ApiNoConformidades_Dollder_MS.Aplication.Queries.NoConformidades;
using DSW_ApiNoConformidades_Dollder_MS.Aplication.Queries.Reporte;
using DSW_ApiNoConformidades_Dollder_MS.Aplication.Responses.Acciones;
using DSW_ApiNoConformidades_Dollder_MS.Aplication.Responses.Cierre;
using DSW_ApiNoConformidades_Dollder_MS.Aplication.Responses.Indicadores;
using DSW_ApiNoConformidades_Dollder_MS.Aplication.Responses.NoConformidad;
using DSW_ApiNoConformidades_Dollder_MS.Aplication.Responses.Reportes;
using DSW_ApiNoConformidades_Dollder_MS.Aplication.Responses.Responsable;
using DSW_ApiNoConformidades_Dollder_MS.Aplication.Responses.RevisionReporte;
using DSW_ApiNoConformidades_Dollder_MS.Aplication.Responses.Seguimiento;
using DSW_ApiNoConformidades_Dollder_MS.Aplication.Responses.VerificacionEfectividad;
using DSW_ApiNoConformidades_Dollder_MS.Infrastructure.Database;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DSW_ApiNoConformidades_Dollder_MS.Aplication.Handlers.Queries.NoConformidades
{
    public class BuscarNoConformidadCompletaHandler : IRequestHandler<BuscarNoConformidadCompletaQuery, ReporteResponse>
    {
        private readonly ApiDbContext _dbContext;
        private readonly ILogger<BuscarNoConformidadCompletaHandler> _logger;
        private readonly MediatR.IMediator _mediator;
        public BuscarNoConformidadCompletaHandler(ApiDbContext dbContext, ILogger<BuscarNoConformidadCompletaHandler> logger, MediatR.IMediator mediator)
        {
            _dbContext = dbContext;
            _logger = logger;
            _mediator = mediator;
        }

        public Task<ReporteResponse> Handle(BuscarNoConformidadCompletaQuery request, CancellationToken cancellationToken)
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

        private async Task<ReporteResponse> HandleAsync(BuscarNoConformidadCompletaQuery request)
        {
            try
            {
                var query = new BuscarReporteIdQuery(request._request.Data);
                var idReporte = _mediator.Send(query);

                var response = new ReporteResponse();

                var reporte = _dbContext.Reporte.FirstOrDefault(x => x.Id == idReporte.Result);
                var noConformidad = _dbContext.NoConformidad.FirstOrDefault(x => x.reporte_Id == idReporte.Result);
                var revisadorPor = _dbContext.RevisionReporte.FirstOrDefault(x => x.reporte_Id == idReporte.Result);

                response = new ReporteResponse
                {
                    //Datos del reporte
                    Id = reporte.Id,
                    CreatedAt = reporte.CreatedAt,
                    CreatedBy = reporte.CreatedBy,
                    UpdatedAt = reporte.UpdatedAt,
                    UpdatedBy = reporte.UpdatedBy,

                    id_usuario = reporte.usuario_Id,
                    detectado_por = reporte.detectado_por,
                    area = reporte.area,
                    titulo = reporte.titulo,
                    descripcion = reporte.descripcion,
                    estado = reporte.estado,
                    departamento_emisor = reporte.departamento_emisor,

                    //Datos de la revisión
                    revisionReporte = new RevisionReporteResponse
                    {
                        Id = revisadorPor.Id,
                        CreatedAt = revisadorPor.CreatedAt,
                        CreatedBy = revisadorPor.CreatedBy,
                        UpdatedAt = revisadorPor.UpdatedAt,
                        UpdatedBy = revisadorPor.UpdatedBy,
                        estado = revisadorPor.estado,

                        nombre = revisadorPor.nombre,
                    },

                    //Datos de la no conformidad
                    noConformidad = new NoConformidadesResponse
                    {
                        Id = noConformidad.Id,
                        CreatedAt = noConformidad.CreatedAt,
                        CreatedBy = noConformidad.CreatedBy,
                        UpdatedAt = noConformidad.UpdatedAt,
                        UpdatedBy = noConformidad.UpdatedBy,

                        numero_expedicion = noConformidad.numero_expedicion,
                        revisado_por = noConformidad.revisado_por,
                        consecuencias = noConformidad.consecuencias,
                        responsables_cargo = noConformidad.responsables_cargo,
                        areas_involucradas = noConformidad.areas_involucradas,
                        prioridad = noConformidad.prioridad,
                        estado = Estado.GetName(typeof(Estado), noConformidad.estado) ?? "Sin estado",
                    }
                };

                var auxImag = _dbContext.ImagenReporte.Count(x => x.reporte_Id == idReporte.Result);
                //Si hay imagenes
                if (auxImag > 0)
                {
                    var imagenes = _dbContext.ImagenReporte.Where(x => x.reporte_Id == idReporte.Result).ToList();
                    foreach (var img in imagenes)
                    {
                        response.imagenes.Add(new ImagenReporteResponse
                        {
                            Id = img.Id,
                            estado= img.estado,
                            imagen = img.imagen,
                            nombre_archivo = img.nombre_archivo
                        });
                    }
                }

                var aux1 = _dbContext.Responsable.Count(x => x.noConformidad_Id == noConformidad.Id);

                //Si hay responsables
                if (aux1 > 0)
                {
                    var isresponsable = _dbContext.Responsable.Where(x => x.noConformidad_Id == noConformidad.Id).ToList();
                    foreach (var resp in isresponsable)
                    {
                        var usuarioResponsable = _dbContext.Usuario.FirstOrDefault(x => x.Id == resp.usuario_Id);

                        //Agregar responsables
                        response.noConformidad.responsables.Add(new ResponsableResponse
                        {
                            Id = resp.Id,
                            CreatedAt = resp.CreatedAt,
                            CreatedBy = resp.CreatedBy,
                            UpdatedAt = resp.UpdatedAt,
                            UpdatedBy = resp.UpdatedBy,
                            estado = resp.estado,

                            usuario_Id = resp.usuario_Id,

                            investigacion = resp.investigacion,
                            analisis_causa = resp.analisis_causa,
                            correccion = resp.correccion,
                            analisis_riesgo = resp.analisis_riesgo,
                            cargo_usuario = resp.cargo_responsable,
                            fecha_compromiso = resp.fecha_compromiso,

                            nombre_usuario = usuarioResponsable.nombre,
                            apellido_usuario = usuarioResponsable.apellido,


                        });

                        var aux2 = _dbContext.Correctivas.Count(x => x.responsable_Id == resp.Id);
                        if (aux2 > 0)
                        {
                            //Si hay acciones
                            var accionesCorrectivas = _dbContext.Correctivas.Where(x => x.responsable_Id == resp.Id).ToList();
                            foreach (var accion in accionesCorrectivas)
                            {
                                //Traigo el id del usuario presente en la accion para poder traer el nombre
                                var uid = _dbContext.R_Acciones_Usuario.FirstOrDefault(x => x.acciones_Id == accion.Id);
                                var usuario = _dbContext.Usuario.FirstOrDefault(x => x.Id == uid.usuario_Id);



                                var acc = new AccionesResponse
                                {
                                    Id = accion.Id,
                                    CreatedAt = accion.CreatedAt,
                                    CreatedBy = accion.CreatedBy,
                                    UpdatedAt = accion.UpdatedAt,
                                    UpdatedBy = accion.UpdatedBy,

                                    usuario_Id = usuario.Id,
                                    nombre_usuario = usuario.nombre,
                                    apellido_usuario = usuario.apellido,

                                    fecha_compromiso = accion.fecha_compromiso,
                                    cargo_usuario = accion.cargo_usuario,

                                    investigacion = accion.investigacion,
                                    area = accion.area,
                                    estado = accion.estado,
                                    visto_bueno = accion.visto_bueno,

                                };

                                response.noConformidad.responsables.Last().acciones.Add(acc);
                            }
                        }


                        var aux22 = _dbContext.Preventivas.Count(x => x.responsable_Id == resp.Id);
                        if (aux2 > 0)
                        {
                            //Si hay acciones
                            var accionesPreventivas = _dbContext.Preventivas.Where(x => x.responsable_Id == resp.Id).ToList();
                            foreach (var accion in accionesPreventivas)
                            {
                                //Traigo el id del usuario presente en la accion para poder traer el nombre
                                var uid = _dbContext.R_Acciones_Usuario.FirstOrDefault(x => x.acciones_Id == accion.Id);
                                var usuario = _dbContext.Usuario.FirstOrDefault(x => x.Id == uid.usuario_Id);

                                var acc = new AccionesResponse
                                {
                                    Id = accion.Id,
                                    CreatedAt = accion.CreatedAt,
                                    CreatedBy = accion.CreatedBy,
                                    UpdatedAt = accion.UpdatedAt,
                                    UpdatedBy = accion.UpdatedBy,
                                    correctivas_Id = accion.correctiva_Id,

                                    usuario_Id = usuario.Id,
                                    nombre_usuario = usuario.nombre,
                                    apellido_usuario = usuario.apellido,

                                    fecha_compromiso = accion.fecha_compromiso,
                                    cargo_usuario = accion.cargo_usuario,

                                    investigacion = accion.investigacion,
                                    area = accion.area,
                                    estado = accion.estado,
                                    visto_bueno = accion.visto_bueno,
                                };

                                response.noConformidad.responsables.Last().acciones.Add(acc);
                            }

                        }

                    }
                }

                var aux3 = _dbContext.Seguimiento.Count(x => x.noConformidad_Id == noConformidad.Id);
                if (aux3 > 0)
                {
                    var seguimiento = _dbContext.Seguimiento.Where(x => x.noConformidad_Id == noConformidad.Id).ToList();
                    foreach (var seg in seguimiento)
                    {
                        //Agregar seguimiento
                        response.noConformidad.seguimientos.Add(new SeguimientoResponse
                        {
                            Id = seg.Id,
                            CreatedAt = seg.CreatedAt,
                            CreatedBy = seg.CreatedBy,
                            UpdatedAt = seg.UpdatedAt,
                            UpdatedBy = seg.UpdatedBy,
                            estado = seg.estado,

                            fecha_seguimiento = seg.fecha_seguimiento,
                            estatus = seg.estatus,
                            observaciones = seg.observaciones,
                            realizado_por = seg.realizado_por,
                        });
                    }
                }

                var aux4 = _dbContext.Cierre.Count(x => x.noConformidad_Id == noConformidad.Id);
                if (aux4 > 0)
                {
                    var cierre = _dbContext.Cierre.FirstOrDefault(x => x.noConformidad_Id == noConformidad.Id);
                    response.noConformidad.cierre = new CierreResponse
                    {
                        Id = cierre.Id,
                        CreatedAt = cierre.CreatedAt,
                        CreatedBy = cierre.CreatedBy,
                        UpdatedAt = cierre.UpdatedAt,
                        UpdatedBy = cierre.UpdatedBy,
                        estado= cierre.estado,

                        conforme = cierre.conforme,
                        observaciones = cierre.observaciones,
                        responsable = cierre.responsable,
                        fecha_verificacion = cierre.fecha_verificacion,
                    };


                    var aux5 = _dbContext.Indicadores.Count(x => x.cierre_Id == cierre.Id);
                    if (aux5 != null)
                    {
                        var indicador = _dbContext.Indicadores.FirstOrDefault(x => x.cierre_Id == cierre.Id);
                        response.noConformidad.cierre.indicadores = new IndicadoresResponse
                        {
                            Id = indicador.Id,
                            CreatedAt = indicador.CreatedAt,
                            CreatedBy = indicador.CreatedBy,
                            UpdatedAt = indicador.UpdatedAt,
                            UpdatedBy = indicador.UpdatedBy,
                            estado = indicador.estado,

                            origen_Id = indicador.origen_Id,
                        };


                        var origen = _dbContext.IndicadorOrigen.FirstOrDefault(c => c.Id == response.noConformidad.cierre.indicadores.origen_Id);
                        response.noConformidad.cierre.indicadores.origen = new IndicadorOrigenResponse { origen = origen.origen };




                        var aux51 = _dbContext.R_Indicadores_Causas.Where(c => c.indicador_Id == indicador.Id).ToList();
                        foreach (var ind in aux51)
                        {
                            var causa = _dbContext.IndicadorCausa.FirstOrDefault(c => c.Id == ind.causa_Id);
                            response.noConformidad.cierre.indicadores.causa.Add(new IndicadorCausaResponse {
                                                                                                            Id = causa.Id,
                                                                                                            causa = causa.causa });
                        };
                    }


                    var aux6 = _dbContext.VerificacionEfectividad.Count(x => x.cierre_Id == cierre.Id);

                    if (aux6 > 0)
                    {
                        var verificacionEfectividad = _dbContext.VerificacionEfectividad.FirstOrDefault(x => x.cierre_Id == cierre.Id);
                        response.noConformidad.cierre.verificacionEfectividad = new VerificacionEfectividadResponse
                        {
                            Id = verificacionEfectividad.Id,
                            CreatedAt = verificacionEfectividad.CreatedAt,
                            CreatedBy = verificacionEfectividad.CreatedBy,
                            UpdatedAt = verificacionEfectividad.UpdatedAt,
                            UpdatedBy = verificacionEfectividad.UpdatedBy,
                            estado = verificacionEfectividad.estado,

                            efectiva = verificacionEfectividad.efectiva,
                            realizado_por = verificacionEfectividad.realizado_por,
                            observaciones = verificacionEfectividad.observaciones,
                        };
                    }
                }



                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en ConsultarUsuarioIdQueryHandler.HandleAsync");
                throw; // Relanzar la excepción para que sea manejada en un nivel superior
            }
        }
    }
}
