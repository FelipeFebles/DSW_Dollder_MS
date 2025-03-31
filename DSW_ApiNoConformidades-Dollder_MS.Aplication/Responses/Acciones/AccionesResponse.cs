namespace DSW_ApiNoConformidades_Dollder_MS.Aplication.Responses.Acciones
{
    public class AccionesResponse : BaseResponse
    {
        public string? investigacion { get; set; }     // Acciones correctivas
        public string? area { get; set; }
        public bool? visto_bueno { get; set; }        // Revisado por el gerente
        public string? fecha_compromiso { get; set; }      // fecha_compromiso de la actividad
        public string? nombre_usuario { get; set; }           //Lo usare para ver el nombre del usuario
        public string? apellido_usuario { get; set; }         //Lo usare para ver el Apellido del usuario  
        public string? cargo_usuario { get; set; }    // Cargo del usuario
        
        
        
        public Guid? usuario_Id { get; set; }
        public Guid? correctivas_Id { get; set; }
        public Guid? responsable_Id { get; set; }

    }
}
