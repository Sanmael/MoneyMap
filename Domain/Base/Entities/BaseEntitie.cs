namespace Domain.Base.Entities
{
    public class BaseEntitie
    {
        public long Id {  get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set;} = DateTime.Now;
        public DateTime? DeletedAt { get; set; } = null;
    }
}