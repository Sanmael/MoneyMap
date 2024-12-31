using Domain.Base.Entities;

namespace Domain.Cards.Entities
{
    public class Card : BaseEntitie
    {        
        public User User { get; set; }
        public DateTime DueDate { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public Categorie Categorie { get; set; }
        public decimal Limit { get; set; }
        public decimal Balance { get; set; }

        public Card()
        {
            
        }

        public bool IsValid()
        {
            return Limit > 0
                && DueDate > DateTime.Now;
        }
    }
}