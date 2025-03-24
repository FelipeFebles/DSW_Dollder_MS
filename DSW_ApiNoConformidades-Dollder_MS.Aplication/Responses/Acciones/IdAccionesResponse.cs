namespace DSW_ApiNoConformidades_Dollder_MS.Application.Responses.Acciones
{
    public class IdAccionesResponse
    {
        public Guid? id { get; set; }

        public IdAccionesResponse(Guid id)
        {
            this.id = id;
        }
    }
}
