namespace DSW_ApiNoConformidades_Dollder_MS.Aplication.Responses.Indicadores
{
    public class IndicadoresResponse : BaseResponse
    {

        public IndicadorOrigenResponse? origen { get; set; }

        public List<IndicadorCausaResponse>? causa { get; set; } = new List<IndicadorCausaResponse> { };

        public Guid? origen_Id { get; set; }
        public Guid? cierre_Id { get; set; }
    }
}
