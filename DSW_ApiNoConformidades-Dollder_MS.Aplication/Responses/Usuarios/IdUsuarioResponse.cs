
namespace DSW_ApiNoConformidades_Dollder_MS.Application.Responses.Usuarios
{
    public class IdUsuarioResponse
    {
        public Guid? id { get; set; }
        public IdUsuarioResponse() { }
        public IdUsuarioResponse(Guid data)
        {
            id = data;
        }

    }
}
