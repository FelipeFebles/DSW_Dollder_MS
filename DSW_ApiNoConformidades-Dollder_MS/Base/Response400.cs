using System.Diagnostics.CodeAnalysis;

namespace DSW_ApiNoConformidades_Dollder_MS.Base
{
    public class Response400 : Respuesta
    {
        public string Message { get; set; }

        public string Exception { get; set; }

        public string InnerException { get; set; }

        public Response400(Guid operationId, string operationName, string message, string exception)
            : base(operationId, operationName)
        {
            Message = message;
            Exception = exception;
        }

        public Response400(Respuesta responseOperation, string message, string exception, string innerException)
            : base(responseOperation)
        {
            Message = message;
            Exception = exception;
            InnerException = innerException;
        }
    }
}
