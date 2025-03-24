namespace DSW_ApiNoConformidades_Dollder_MS.Aplication.Requests
{
    public class BaseRequest
    {
        public Guid? Id { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }
    }
}
