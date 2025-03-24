using System.Diagnostics.CodeAnalysis;

namespace DSW_ApiNoConformidades_Dollder_MS.Base
{
    public class Response200<T> : Respuesta
    {
        public T Data { get; set; }

        public Response200(Guid operationId, string operationName, T data)
            : base(operationId, operationName)
        {
            Data = data;
        }

        public Response200(Respuesta responseOperation, T data)
            : base(responseOperation)
        {
            Data = data;
        }
    }
}
