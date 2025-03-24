using DSW_ApiNoConformidades_Dollder_MS.Aplication.Queries.Reporte;
using DSW_ApiNoConformidades_Dollder_MS.Application.Responses.Reportes;
using DSW_ApiNoConformidades_Dollder_MS.Infrastructure.Database;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DSW_ApiNoConformidades_Dollder_MS.Aplication.Handlers.Queries.Reporte
{
    public class BuscarReporteIdHandler : IRequestHandler<BuscarReporteIdQuery, Guid>
    {
        private readonly ApiDbContext _dbContext;
        private readonly ILogger<BuscarReporteIdHandler> _logger;
        public BuscarReporteIdHandler(ApiDbContext dbContext, ILogger<BuscarReporteIdHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public Task<Guid> Handle(BuscarReporteIdQuery request, CancellationToken cancellationToken)
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

        private async Task<Guid> HandleAsync(BuscarReporteIdQuery request)
        {
            try
            {
                var isAcion = _dbContext.Acciones.Where(c => c.Id == request._request).FirstOrDefault();
                if (isAcion == null)
                {
                    var isResponsable = _dbContext.Responsable.Where(c => c.Id == request._request).FirstOrDefault();
                    if (isResponsable == null)
                    {
                        var isNoConformidad = _dbContext.NoConformidad.Where(c => c.Id == request._request).FirstOrDefault();
                        if (isNoConformidad == null)
                        {
                            var isReporte = _dbContext.Reporte.Where(c => c.Id == request._request).FirstOrDefault();
                            if (isReporte == null)
                            {
                                //Valido si no es in seguimiento
                                var isSeguimiento = _dbContext.Seguimiento.Where(c => c.Id == request._request).FirstOrDefault();
                                if (isSeguimiento != null)
                                {
                                    var seg = _dbContext.NoConformidad.Where(c => c.Id == isSeguimiento.noConformidad_Id).FirstOrDefault();
                                    var res = _dbContext.Reporte.Where(c => c.Id == seg.reporte_Id)
                                        .Select(c => new IdReporteResponse(c.Id)).FirstOrDefault();
                                    return res.id;
                                }

                                //Valido si no es in cierre
                                var isCierre = _dbContext.Cierre.Where(c => c.Id == request._request).FirstOrDefault();
                                if (isCierre != null)
                                {
                                    var cie = _dbContext.NoConformidad.Where(c => c.Id == isCierre.noConformidad_Id).FirstOrDefault();
                                    var res = _dbContext.Reporte.Where(c => c.Id == cie.reporte_Id)
                                        .Select(c => new IdReporteResponse(c.Id)).FirstOrDefault();
                                    return res.id;
                                }

                                //Valido si no es un inficador
                                var isIndicador = _dbContext.Indicadores.Where(c => c.Id == request._request).FirstOrDefault();
                                if (isIndicador != null)
                                {
                                    var cie = _dbContext.Cierre.Where(c => c.Id == isIndicador.cierre_Id).FirstOrDefault();

                                    var ind = _dbContext.NoConformidad.Where(c => c.Id == cie.noConformidad_Id).FirstOrDefault();
                                    var res = _dbContext.Reporte.Where(c => c.Id == ind.reporte_Id)
                                        .Select(c => new IdReporteResponse(c.Id)).FirstOrDefault();
                                    return res.id;
                                }

                                //Valido si no es una Verificacion
                                var isVeri = _dbContext.VerificacionEfectividad.Where(c => c.Id == request._request).FirstOrDefault();
                                if (isVeri != null)
                                {
                                    var cie = _dbContext.Cierre.Where(c => c.Id == isVeri.cierre_Id).FirstOrDefault();

                                    var veri = _dbContext.NoConformidad.Where(c => c.Id == cie.noConformidad_Id).FirstOrDefault();
                                    var res = _dbContext.Reporte.Where(c => c.Id == veri.reporte_Id)
                                        .Select(c => new IdReporteResponse(c.Id)).FirstOrDefault();
                                    return res.id;
                                }

                                //Si no arroja el error
                                throw new Exception("No se encontro el reporte");
                            }
                            //Si es un reporte devuelvo el mismo id
                            var response = _dbContext.Reporte.Where(c => c.Id == request._request)
                            .Select(c => new IdReporteResponse(request._request)).FirstOrDefault();
                            return response.id;
                        }

                        //Si es una NoConformidad devuelvo el id del reporte
                        var response2 = _dbContext.NoConformidad.Where(c => c.Id == request._request)
                            .Select(c => new IdReporteResponse(c.reporte_Id)).FirstOrDefault();
                        return response2.id;
                    }

                    //Si es un responsable devuelvo el id de la NoConformidad primero
                    var aux = _dbContext.NoConformidad.Where(c => c.Id == isResponsable.noConformidad_Id).FirstOrDefault();

                    //Ahora con la no conformidad devuelvo el id del reporte
                    var response3 = _dbContext.Reporte.Where(c => c.Id == aux.reporte_Id)
                        .Select(c => new IdReporteResponse(c.Id)).FirstOrDefault();
                    return response3.id;
                }
                //Si es una accion devuelvo el id del Responsable primero
                var aux2 = _dbContext.Responsable.Where(c => c.Id == isAcion.responsable_Id).FirstOrDefault();
                //Con el responsable ahora devuelvo el id de la NoConformidad
                var aux3 = _dbContext.NoConformidad.Where(c => c.Id == aux2.noConformidad_Id).FirstOrDefault();
                //Con la no conformidad devuelvo el id del reporte
                var response4 = _dbContext.Reporte.Where(c => c.Id == aux3.reporte_Id)
                    .Select(c => new IdReporteResponse(c.Id)).FirstOrDefault();

                return response4.id;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en ConsultarUsuarioIdQueryHandler.HandleAsync");
                throw; // Relanzar la excepción para que sea manejada en un nivel superior
            }
        }
    }
}
