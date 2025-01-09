namespace Domain.Base.Entities
{
    public class BaseEntitie
    {
        public Guid Id {  get; set; } = Guid.NewGuid();
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set;} = DateTime.Now;
        public DateTime? DeletedAt { get; set; } = null;
    }
}