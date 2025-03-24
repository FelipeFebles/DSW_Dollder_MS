namespace DSW_ApiNoConformidades_Dollder_MS.Aplication.Requests.Indicadores
{
    public class IndicadorCausaRequest: BaseRequest
    {
        public string? causa {  get; set; }    
        Guid? indicador_Id { get; set; }
    }
}
