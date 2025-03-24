using DSW_ApiNoConformidades_Dollder_MS.Application.Commands.NoConformidad;
using DSW_ApiNoConformidades_Dollder_MS.Application.Mappers.NoConformidad;
using DSW_ApiNoConformidades_Dollder_MS.Application.Mappers.Notificacion;
using DSW_ApiNoConformidades_Dollder_MS.Application.Mappers.R_Calidad_NoConformidadMapper;
using DSW_ApiNoConformidades_Dollder_MS.Application.Requests.Notificacion;
using DSW_ApiNoConformidades_Dollder_MS.Application.Responses.NoConformidad;
using DSW_ApiNoConformidades_Dollder_MS.Infrastructure.Database;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DSW_ApiNoConformidades_Dollder_MS.Application.Handlers.Commands.NoConformidad
{
    public class AgregarNoConformidadHandler : IRequestHandler<AgregarNoConformidadCommand, IdNoConformidadResponse>
    {
        private readonly ApiDbContext _dbContext;
        private readonly ILogger<AgregarNoConformidadHandler> _logger;
        public AgregarNoConformidadHandler(ApiDbContext dbContext, ILogger<AgregarNoConformidadHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }


        public Task<IdNoConformidadResponse> Handle(AgregarNoConformidadCommand request, CancellationToken cancellationToken)
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

        private async Task<IdNoConformidadResponse> HandleAsync(AgregarNoConformidadCommand request)
        {
            var transaccion = _dbContext.BeginTransaction();
            try
            {

                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                ///     Reviso si el reporte ya no esta generado
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                var mismo_Reporte = _dbContext.NoConformidad.Count(n=> n.reporte_Id== request._request.reporte_Id);

                if (mismo_Reporte > 0) //Verifico que el Usuario exista
                {
                    throw new InvalidOperationException("El reporte ya fue registrado");
                }

                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                ///     Obtengo los usuarios de calidad
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                var calidad_usuarios = _dbContext.Calidad
                                                .Select(u => new
                                                {
                                                    id = u.Id,
                                                    correo = u.correo,
                                                }).ToList();

                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                ///     Agrego la no conformidad
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                var noConformidad = NoConformidadMapper.MapRequestNoConformidadEntity(request._request, GenerarNuevaExpedicion());
                _dbContext.NoConformidad.Add(noConformidad);
                await _dbContext.SaveEfContextChanges("APP");


                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                ///     Agrego la relacion Calidad_NoConformidad
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                var titulo = _dbContext.Reporte.Where(r=> r.Id == request._request.reporte_Id).FirstOrDefault();

                foreach (var calidad in calidad_usuarios)
                {
                    _dbContext.R_Calidad_NoConformidad.Add(R_Calidad_NoConformidadMapper.MapR_Calidad_NoConformidadEntity(calidad.id, noConformidad.Id)); R_Calidad_NoConformidadMapper.MapR_Calidad_NoConformidadEntity(calidad.id, noConformidad.Id);
                    await _dbContext.SaveEfContextChanges("APP");

                    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    ///     Genero la notificacion para cada usuario de Garantia de Calidad
                    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    var notificacion = NotificacionMapper.MapRequestNotificacionEntity(new NotificacionRequest(titulo.titulo, request._request.revisado_por, calidad.correo, "Se ha generado una nueva no conformidad para revisar", false, "NoConformidades"));
                    _dbContext.Notificacion.Add(notificacion);
                    await _dbContext.SaveEfContextChanges("APP");
                }


                transaccion.Commit();

                return new IdNoConformidadResponse(noConformidad.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error AgregarOperarioHandler.HandleAsync. {Mensaje}", ex.Message);
                throw;
            }

        }
        public string GenerarNuevaExpedicion()
        {
            // 1. Obtener año actual en formato corto
            string añoActual = DateTime.Now.ToString("yy"); // Ej: "24" para 2024

            // 2. Buscar la última expedición registrada en toda la tabla
            var ultimaExpedicion = _dbContext.NoConformidad
                .OrderByDescending(n => n.numero_expedicion)
                .Select(n => n.numero_expedicion)
                .FirstOrDefault();

            int nuevoNumero = 1;

            // 3. Si existe al menos una expedición
            if (!string.IsNullOrEmpty(ultimaExpedicion))
            {
                // 4. Extraer componentes de la última expedición
                var partes = ultimaExpedicion.Split('-');

                if (partes.Length == 3 && partes[0] == "NC")
                {
                    string ultimoAño = partes[1];
                    string ultimoNumeroStr = partes[2];

                    // 5. Comparar años
                    if (ultimoAño == añoActual)
                    {
                        // Mismo año: incrementar número
                        if (int.TryParse(ultimoNumeroStr, out int ultimoNumero))
                        {
                            nuevoNumero = ultimoNumero + 1;
                        }
                    }
                    else
                    {
                        // Año diferente: resetear a 1
                        nuevoNumero = 1;
                    }
                }
            }

            // 6. Formatear nuevo número con 2 dígitos
            return $"NC-{añoActual}-{nuevoNumero:D3}";
        }
    }
}
