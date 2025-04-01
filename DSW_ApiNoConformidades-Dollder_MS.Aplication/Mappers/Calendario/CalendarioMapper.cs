using DSW_ApiNoConformidades_Dollder_MS.Aplication.Requests.Calendario;
using DSW_ApiNoConformidades_Dollder_MS.Core.Entities;

namespace DSW_ApiNoConformidades_Dollder_MS.Aplication.Mappers.Calendario
{
    public class CalendarioMapper
    {
        public static CalendarioEntity MapCalendarioEntity(string fecha)
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
                descripcion = "Tiene un seguimiento asignado para una NC.",
                color = "#b0e57c", // Verde pastel
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
