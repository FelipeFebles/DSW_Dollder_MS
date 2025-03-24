namespace DSW_ApiNoConformidades_Dollder_MS.Core.Entities
{
    public class CalendarioEntity: BaseEntity
    {
        public int? dia { get; set; }
        public int? mes { get; set; }
        public int? anio { get; set; }
        public string? titulo { get; set; }
        public string descripcion { get; set; }
        public string color { get; set; }
        public bool? estado { get; set; }



    }
}
