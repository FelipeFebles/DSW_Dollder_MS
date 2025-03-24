namespace DSW_ApiNoConformidades_Dollder_MS.Application.Responses.NoConformidad
{
    public class IdNoConformidadResponse
    {
        public Guid? id { get; set; }

        public IdNoConformidadResponse(Guid id)
        {
            this.id = id;
        }
    }
}
