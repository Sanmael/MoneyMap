﻿namespace Domain.Base.Entities
{
    public class BaseEntitie
    {
        public long Id {  get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set;}
        public DateTime DeletedAt { get; set; }
    }
}