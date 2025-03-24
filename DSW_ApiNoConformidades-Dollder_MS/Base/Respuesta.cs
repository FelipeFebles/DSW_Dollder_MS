using System.Diagnostics.CodeAnalysis;

namespace DSW_ApiNoConformidades_Dollder_MS.Base
{
    public class Respuesta
    {
        public Guid OperationId { get; set; }

        public string OperationName { get; set; }

        public Respuesta(Guid operationId, string operationName)
        {
            OperationId = operationId;
            OperationName = operationName;
        }

        public Respuesta(Respuesta responseOperation)
        {
            OperationId = responseOperation.OperationId;
            OperationName = responseOperation.OperationName;
        }
    }
}
