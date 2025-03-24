namespace DSW_ApiNoConformidades_Dollder_MS.Core.Entities;

public class BaseEntity
{
    public Guid Id { get; set; }
    public bool? estado { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? UpdatedBy { get; set; }
}