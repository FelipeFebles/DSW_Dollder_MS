﻿namespace DSW_ApiNoConformidades_Dollder_MS.Aplication.Responses
{
    public class BaseResponse
    {
        public Guid? Id { get; set; }           
        public DateTime? CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }
        public bool? estado { get; set; }

    }
}