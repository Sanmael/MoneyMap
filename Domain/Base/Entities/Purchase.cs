
namespace Domain.Base.Entities
{
    public class Purchase : BaseEntitie
    {
        public required User User { get; set; }
        public required string Name { get; set; }
        public string Description { get; set; } = string.Empty;
        public decimal TotalPrice { get; set; }
        public Categorie? Categorie { get; set; } 
    }
}