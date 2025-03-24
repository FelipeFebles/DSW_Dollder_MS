namespace DSW_ApiNoConformidades_Dollder_MS.Aplication.Requests.Calendario
{
    public class CalendarioRequest : BaseRequest
    {
        public int? dia { get; set; }
        public int? mes { get; set; }
        public int? anio { get; set; }
        public string? titulo { get; set; }
        public string? descripcion { get; set; }
        public string? color { get; set; }
        public bool? estado { get; set; }
    }
}
