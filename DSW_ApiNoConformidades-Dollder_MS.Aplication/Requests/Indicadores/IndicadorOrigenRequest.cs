namespace DSW_ApiNoConformidades_Dollder_MS.Aplication.Requests.Indicadores
{
    public class IndicadorOrigenRequest: BaseRequest
    {
        public string? origen { get; set; }
        public Guid? indicador_Id { get; set; }
    }
}
