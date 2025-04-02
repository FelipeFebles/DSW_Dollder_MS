using DSW_ApiNoConformidades_Dollder_MS.Aplication.Requests.Calendario;
using DSW_ApiNoConformidades_Dollder_MS.Core.Entities;

namespace DSW_ApiNoConformidades_Dollder_MS.Aplication.Mappers.Calendario
{
    public class CalendarioMapper
    {
        public static CalendarioEntity MapCalendarioEntity(string fecha, string descripcion)
        {
            // Divide el string de fecha
            string[] partes = fecha.Split('-');

            // Crea la instancia de CalendarioEntity
            CalendarioEntity entity = new CalendarioEntity
            {
                anio = int.Parse(partes[0]),
                mes = int.Parse(partes[1]),
                dia = int.Parse(partes[2]),
                titulo = "Seguimiento",
                descripcion = descripcion,
                color = "#b0e57c", // Verde pastel
                estado = true
            };

            return entity;
        }

        public static CalendarioEntity MapCalendarioEntity2(string fecha, string descripcion)
        {
            // Divide el string de fecha
            string[] partes = fecha.Split('-');

            // Crea la instancia de CalendarioEntity
            CalendarioEntity entity = new CalendarioEntity
            {
                anio = int.Parse(partes[0]),
                mes = int.Parse(partes[1]),
                dia = int.Parse(partes[2]),
                titulo = "Acciones",
                descripcion = descripcion,
                color = "#5271ff", // azul real
                estado = true
            };

            return entity;
        }

        public static CalendarioEntity MapCalendarioEntityVerificacion(string fecha, string descripcion)
        {
            // Divide el string de fecha
            string[] partes = fecha.Split('-');

            // Crea la instancia de CalendarioEntity
            CalendarioEntity entity = new CalendarioEntity
            {
                anio = int.Parse(partes[0]),
                mes = int.Parse(partes[1]),
                dia = int.Parse(partes[2]),
                titulo = "Verificación de efectividad",
                descripcion = descripcion,
                color = "#cb6ce6", // azul real
                estado = true
            };

            return entity;
        }
        public static CalendarioEntity MapCalendarioEntity(CalendarioRequest request)
        {
            // Crea la instancia de CalendarioEntity
            CalendarioEntity entity = new CalendarioEntity
            {
                dia = request.dia,
                mes = request.mes,
                anio = request.anio,
                titulo = request.titulo,
                descripcion = request.descripcion,
                color = request.color,
                estado = true
            };

            return entity;
        }
    }
}
