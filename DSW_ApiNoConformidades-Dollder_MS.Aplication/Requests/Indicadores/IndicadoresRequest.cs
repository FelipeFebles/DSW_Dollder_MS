namespace DSW_ApiNoConformidades_Dollder_MS.Aplication.Requests.Indicadores
{
    public class IndicadoresRequest : BaseRequest
    {
        public IndicadorOrigenRequest? origen { get; set; }

        public List<IndicadorCausaRequest>? causa { get; set; }

        public Guid? origen_Id { get; set; }
        public Guid? cierre_Id { get; set; }
    }
}
